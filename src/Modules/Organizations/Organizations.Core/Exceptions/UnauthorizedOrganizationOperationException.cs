using Shared.Abstractions.Exceptions;

namespace Organizations.Core.Exceptions;

internal class UnauthorizedOrganizationOperationException(Guid id) : CustomException($"Unauthorized operation on organization with editor ID: {id}");