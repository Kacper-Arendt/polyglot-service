using Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Users.Core.Exceptions;

internal class UserCreationFailedException(List<IdentityError> errors) : CustomException(string.Join(", ", errors.Select(x => x.Description)))
{
};