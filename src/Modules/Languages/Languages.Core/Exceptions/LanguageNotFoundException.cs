using Shared.Abstractions.Exceptions;

namespace Languages.Core.Exceptions;

internal class LanguageNotFoundException(Guid id) : CustomException($"Language with ID: {id} does not exist"); 
