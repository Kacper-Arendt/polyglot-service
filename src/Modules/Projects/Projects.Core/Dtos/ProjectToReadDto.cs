using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Dtos;

public record ProjectToReadDto(
    ProjectId Id,
    string Name,
    string Description,
    LanguageId BaseLanguage,
    OwnerId Owner
);