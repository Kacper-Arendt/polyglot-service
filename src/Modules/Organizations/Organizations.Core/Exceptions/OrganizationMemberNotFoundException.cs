using Shared.Abstractions.Exceptions;

namespace Organizations.Core.Exceptions;

internal class OrganizationMemberNotFoundException(Guid id) : CustomException($"Organization member with ID: {id} does not exist"); 