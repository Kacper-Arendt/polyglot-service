using Shared.Abstractions.Exceptions;

namespace Translations.Core.Exceptions;

internal class TranslationKeyNotFoundException(Guid id) : CustomException($"Translation key text with ID: {id} does not exist"); 
