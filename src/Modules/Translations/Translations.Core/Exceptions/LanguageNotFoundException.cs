using Shared.Abstractions.Exceptions;

namespace Translations.Core.Exceptions;

internal class LanguageNotFoundException(Guid id) : CustomException($"Language with ID: {id} does not exist"); 
