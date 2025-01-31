using Shared.Abstractions.Exceptions;
using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Exceptions;

internal class ProjectNotFoundException(ProjectId id) : CustomException($"Project with ID: {id} does not exist"); 