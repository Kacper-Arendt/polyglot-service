namespace Translations.Core.Dtos;

public record LocalizedTextToRead(Guid Id, string Value, Guid TranslationKeyId, Guid LanguageId);