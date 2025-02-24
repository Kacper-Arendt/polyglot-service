using Shared.Abstractions.Exceptions;

namespace Translations.Core.Exceptions;

internal class ProjectNotFoundException(Guid id) : CustomException($"Project with ID: {id} does not exist"); 
