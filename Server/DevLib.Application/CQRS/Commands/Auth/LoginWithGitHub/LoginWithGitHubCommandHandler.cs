using DevLib.Application.CQRS.Dtos.Commands;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.Interfaces.Services;
using DevLib.Application.Services;
using DevLib.Domain.UserAggregate;
using MediatR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DevLib.Application.CQRS.Commands.Auth
{
    public class LoginWithGitHubHandler : IRequestHandler<LoginWithGitHubCommand, AuthResponseDto>
    {
        private readonly HttpClient _httpClient;
        private readonly IJwtService _jwtService;
        private readonly IAuthRepository _authRepository;
        private readonly string _clientId = "Ov23liVP1fdpjqeZ6yPh";
        private readonly string _clientSecret = "7b3f86da428ca938d69f5e9ec477a511758c54f6";
        public LoginWithGitHubHandler(IJwtService jwtService, IAuthRepository authRepository, HttpClient httpClient)
        {
            _jwtService = jwtService;
            _authRepository = authRepository;
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDto> Handle(LoginWithGitHubCommand request, CancellationToken cancellationToken)
        {
            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("code", request.Code)
            });

            var response = await _httpClient.PostAsync("https://github.com/login/oauth/access_token", requestBody);
            if (!response.IsSuccessStatusCode)
            {
                return CreateLoginResult(false, "Failed to obtain access token.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var json = ParseAccessTokenResponse(responseBody);
            var accessToken = json["access_token"]?.ToString();

            if (string.IsNullOrEmpty(accessToken))
            {
                return CreateLoginResult(false, "Invalid access token.");
            }

            var userInfo = await GetGitHubUserInfo(accessToken);
            var email = userInfo["email"]?.ToString();

            var user = await _authRepository.FindByEmailAsync(email, cancellationToken);

            var token = await _jwtService.GenerateJwtTokenAsync(user);

            return CreateLoginResult(true, token: token);
        }

        private JObject ParseAccessTokenResponse(string responseBody)
        {
            var keyValuePairs = responseBody.Split('&');
            var jsonDict = new Dictionary<string, string>();

            foreach (var pair in keyValuePairs)
            {
                var keyValue = pair.Split('=');
                if (keyValue.Length == 2)
                {
                    var key = Uri.UnescapeDataString(keyValue[0]);
                    var value = Uri.UnescapeDataString(keyValue[1]);
                    jsonDict[key] = value;
                }
            }

            return JObject.FromObject(jsonDict);
        }

        private async Task<JObject> GetGitHubUserInfo(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user/emails");
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("User-Agent", "DevLib");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        private AuthResponseDto CreateLoginResult(bool success, string errorMessage = null, string token = null)
        {
            return new AuthResponseDto { IsSuccess = success, ErrorMessage = errorMessage, Token = token };
        }
    }
}