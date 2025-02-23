namespace Projects.Shared;

public interface IProjectsModuleApi
{
    Task<bool> GetAsync(Guid id);
}