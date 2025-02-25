using Shared.Abstractions.Exceptions;

namespace Organizations.Core.Exceptions;

internal class OrganizationNotFoundException(Guid id) : CustomException($"Organization with ID: {id} does not exist"); 