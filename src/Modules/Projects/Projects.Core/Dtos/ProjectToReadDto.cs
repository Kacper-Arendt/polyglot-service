namespace Projects.Core.Dtos;

public record ProjectToReadDto(
    Guid Id,
    string Name,
    string Description,
    Guid BaseLanguage,
    Guid Owner
);