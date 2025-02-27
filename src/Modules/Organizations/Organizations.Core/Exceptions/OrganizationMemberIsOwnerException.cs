using Shared.Abstractions.Exceptions;

namespace Organizations.Core.Exceptions;

internal class OrganizationMemberIsOwnerException(Guid id) : CustomException($"Organization member with ID: {id} is the owner of the organization and cannot be removed");