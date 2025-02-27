using Shared.Abstractions.Exceptions;

namespace Organizations.Core.Exceptions;

internal class OrganizationMemberExistsException(Guid id) : CustomException($"Organization member with ID: {id} already exists");