using AutoMapper;
using MediatR;
using DevLib.Application.Interfaces.Repositories;
using DevLib.Application.CQRS.Dtos.Queries;

namespace DevLib.Application.CQRS.Queries.User
{
    

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoQueryDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;


        public GetUserInfoQueryHandler(ICommentRepository commentRepository,IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<GetUserInfoQueryDto> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithPostsAsync(query.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found");

            var comments = await _commentRepository.GetUserCommentAsync(query.UserId,cancellationToken);

            var posts = user.Posts.Select(p => new UserPostDto(p.PostId, p.Text)).ToList();



            return new GetUserInfoQueryDto(user.UserName, user.Email, user.Photo, posts, comments);
        }

    }

}
