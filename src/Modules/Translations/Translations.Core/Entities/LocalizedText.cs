namespace Translations.Core.Entities;

public class LocalizedText
{
    public Guid Id { get; private set; }
    public string Value { get; private set; }
    public Guid TranslationKeyId { get; private set; }
    public Guid LanguageId { get; private set; }

    private LocalizedText(string value, Guid translationKeyId, Guid languageId)
    {
        Id = Guid.NewGuid();
        Value = value;
        TranslationKeyId = translationKeyId;
        LanguageId = languageId;
    }
    
    public static LocalizedText Create(string value, Guid translationKeyId, Guid languageId)
    {
        return new LocalizedText(value, translationKeyId, languageId);
    }
    
    public void Update(string value)
    {
        Value = value;
    }
}