namespace Apps.LanguageWeaver.Dto
{
    public class TranslationTextResultDto
    {
        public string SourceLanguageId { get; set; }
        public string TargetLanguageId { get; set; }
        public string Model { get; set; }
        public List<string> Translation { get; set; }
    }
}
