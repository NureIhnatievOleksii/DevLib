﻿namespace DevLib.Application.CQRS.Dtos.Queries;

public record GetBookByIdQueryDto
(
    Guid BookId,
    string BookName,
    string Author,
    string FilePath
);