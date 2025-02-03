using Shared.Abstractions.Exceptions;

namespace Translations.Core.Exceptions;

internal class LocalizedTextNotFoundException(Guid id) : CustomException($"Localized text with ID: {id} does not exist"); 
