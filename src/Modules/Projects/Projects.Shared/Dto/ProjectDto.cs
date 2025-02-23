namespace Projects.Shared.Dto;

public record ProjectDto(Guid Id, string Name, string Description, Guid? BaseLanguage, Guid Owner);
