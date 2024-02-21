using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos;
using Blackbird.Applications.Sdk.Glossaries.Utils.Parsers;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Apps.LanguageWeaver.Extensions;

public static class GlossaryExtensions
{
    #region Constants

    private const string Source = "source";
    private const string Target = "target";
    private const string Comment = "comment";

    #endregion

    #region Language dictionaries

    private static readonly Dictionary<string, string> TbxToLanguageWeaverLanguages = new()
    {
        {"af", "afr"},
        {"sq", "alb"},
        {"am", "amh"},
        {"ar", "ara"},
        {"ar-arabizi", "arz"},
        {"hy", "arm"},
        {"az", "aze"},
        {"eu", "baq"},
        {"be", "bel"},
        {"bn", "ben"},
        {"bh", "bih"},
        {"bg", "bul"},
        {"my", "bur"},
        {"ca", "cat"},
        {"ceb", "ceb"},
        {"chr", "chr"},
        {"zh-hans", "chi"},
        {"zh-hant", "cht"},
        {"hr", "hrv"},
        {"cs", "cze"},
        {"da", "dan"},
        {"prs-af", "fad"},
        {"nl", "dut"},
        {"en", "eng"},
        {"et", "est"},
        {"fi", "fin"},
        {"fr", "fra"},
        {"fr-ca", "frc"},
        {"gl", "glg"},
        {"lg", "lug"},
        {"ka", "geo"},
        {"de", "ger"},
        {"el", "gre"},
        {"gu", "guj"},
        {"ha", "hau"},
        {"he", "heb"},
        {"hi", "hin"},
        {"hmn", "hmn"},
        {"hu", "hun"},
        {"is", "ice"},
        {"id", "ind"},
        {"iu", "iku"},
        {"ga", "gle"},
        {"it", "ita"},
        {"ja", "jpn"},
        {"jv", "jav"},
        {"kn", "kan"},
        {"km", "khm"},
        {"rw", "kin"},
        {"ko", "kor"},
        {"ku", "kur"},
        {"lv", "lav"},
        {"lif", "lif"},
        {"lt", "lit"},
        {"mk", "mac"},
        {"ms", "may"},
        {"ml", "mal"},
        {"mt", "mlt"},
        {"mr", "mar"},
        {"ne", "nep"},
        {"no", "nor"},
        {"or", "ori"},
        {"os", "oss"},
        {"ps", "pus"},
        {"fa", "per"},
        {"pl", "pol"},
        {"pt", "por"},
        {"pt-br", "ptb"},
        {"pt-pt", "ptp"},
        {"ro", "rum"},
        {"ru", "rus"},
        {"sr", "srp"},
        {"sk", "slo"},
        {"sl", "slv"},
        {"so", "som"},
        {"es", "spa"},
        {"su", "sun"},
        {"sw", "swa"},
        {"sv", "swe"},
        {"syr", "syr"},
        {"tl", "tgl"},
        {"tg", "tgk"},
        {"ta", "tam"},
        {"te", "tel"},
        {"th", "tha"},
        {"tr", "tur"},
        {"uk", "ukr"},
        {"ur", "urd"},
        {"uz", "uzb"},
        {"vi", "vie"},
        {"cy", "wel"},
        {"yi", "yid"}
    };
    
    private static readonly Dictionary<string, string> LanguageWeaverToTbxLanguages = new()
    {
        {"afr", "af"},
        {"alb", "sq"},
        {"amh", "am"},
        {"ara", "ar"},
        {"arz", "ar-arabizi"},
        {"arm", "hy"},
        {"aze", "az"},
        {"baq", "eu"},
        {"bel", "be"},
        {"ben", "bn"},
        {"bih", "bh"},
        {"bul", "bg"},
        {"bur", "my"},
        {"cat", "ca"},
        {"ceb", "ceb"},
        {"chr", "chr"},
        {"chi", "zh-hans"},
        {"cht", "zh-hant"},
        {"hrv", "hr"},
        {"cze", "cs"},
        {"dan", "da"},
        {"fad", "prs-af"},
        {"dut", "nl"},
        {"eng", "en"},
        {"est", "et"},
        {"fin", "fi"},
        {"fra", "fr"},
        {"frc", "fr-ca"},
        {"glg", "gl"},
        {"lug", "lg"},
        {"geo", "ka"},
        {"ger", "de"},
        {"gre", "el"},
        {"guj", "gu"},
        {"hau", "ha"},
        {"heb", "he"},
        {"hin", "hi"},
        {"hmn", "hmn"},
        {"hun", "hu"},
        {"ice", "is"},
        {"ind", "id"},
        {"iku", "iu"},
        {"gle", "ga"},
        {"ita", "it"},
        {"jpn", "ja"},
        {"jav", "jv"},
        {"kan", "kn"},
        {"khm", "km"},
        {"kin", "rw"},
        {"kor", "ko"},
        {"kur", "ku"},
        {"lav", "lv"},
        {"lif", "lif"},
        {"lit", "lt"},
        {"mac", "mk"},
        {"may", "ms"},
        {"mal", "ml"},
        {"mt", "mlt"},
        {"mar", "mr"},
        {"nep", "ne"},
        {"nor", "no"},
        {"ori", "or"},
        {"oss", "os"},
        {"pus", "ps"},
        {"per", "fa"},
        {"pol", "pl"},
        {"por", "pt"},
        {"ptb", "pt-br"},
        {"ptp", "pt-pt"},
        {"rum", "ro"},
        {"rus", "ru"},
        {"srp", "sr"},
        {"slo", "sk"},
        {"slv", "sl"},
        {"som", "so"},
        {"spa", "es"},
        {"sun", "su"},
        {"swa", "sw"},
        {"swe", "sv"},
        {"syr", "syr"},
        {"tgl", "tl"},
        {"tgk", "tg"},
        {"tam", "ta"},
        {"tel", "te"},
        {"tha", "th"},
        {"tur", "tr"},
        {"ukr", "uk"},
        {"urd", "ur"},
        {"uzb", "uz"},
        {"vie", "vi"},
        {"wel", "cy"},
        {"yid", "yi"}
    };

    #endregion

    #region Import

    public static MemoryStream ToLanguageWeaverExcelGlossary(this Glossary glossary, DictionaryDto dictionaryDto, 
        IEnumerable<DictionaryTermDto> dictionaryTerms, bool overwriteDuplicates)
    {
        var glossaryLanguageCodes = GetTargetGlossaryLanguageCodes(glossary, dictionaryDto);
        
        var tbxSourceLanguage = glossaryLanguageCodes.Source;
        var tbxTargetLanguage = glossaryLanguageCodes.Target;

        var rows = new List<List<string>> { new() { Source, Target, Comment } };

        foreach (var entry in glossary.ConceptEntries)
        {
            var sourceTerm = GetTerm(entry, tbxSourceLanguage);
            
            if (!overwriteDuplicates)
                if (dictionaryTerms.Any(
                        term => term.Source.Equals(sourceTerm?.Term, StringComparison.OrdinalIgnoreCase)))
                    continue;
            
            var targetTerm = GetTerm(entry, tbxTargetLanguage);

            if (sourceTerm == null || targetTerm == null)
                continue;

            var comment = string.Empty;

            if (sourceTerm.Notes != null)
                comment = string.Join("; ", sourceTerm.Notes);
            
            rows.Add(new() { sourceTerm.Term, targetTerm.Term, comment });
        }
        
        var excelStream = CreateExcelFile(rows, glossary.Title ?? Guid.NewGuid().ToString());
        return excelStream;
    }

    private static (string Source, string Target) GetTargetGlossaryLanguageCodes(Glossary glossary, 
        DictionaryDto dictionaryDto)
    {
        static string GetLanguage(string[] glossaryLanguages, string languageWeaverLanguage)
        {
            var tbxLanguage = LanguageWeaverToTbxLanguages[languageWeaverLanguage];
            var language = glossaryLanguages.FirstOrDefault(code => 
                code.Equals(tbxLanguage, StringComparison.OrdinalIgnoreCase));

            language ??= glossaryLanguages.FirstOrDefault(code =>
                code.StartsWith(tbxLanguage.Split('-')[0], StringComparison.OrdinalIgnoreCase));

            if (language == null)
                throw new Exception(
                    $"Couldn't find an appropriate language code for the input language {languageWeaverLanguage}");
            
            return language;
        }
        
        var glossaryLanguageCodes = glossary.ConceptEntries
            .SelectMany(entry => entry.LanguageSections)
            .Select(section => section.LanguageCode)
            .Distinct()
            .ToArray();

        var sourceLanguage = GetLanguage(glossaryLanguageCodes, dictionaryDto.Source);
        var targetLanguage = GetLanguage(glossaryLanguageCodes, dictionaryDto.Target);

        return new(sourceLanguage, targetLanguage);
    }
    
    private static GlossaryTermSection? GetTerm(GlossaryConceptEntry entry, string tbxLanguage)
        => entry.LanguageSections.FirstOrDefault(section => section.LanguageCode == tbxLanguage)?.Terms
            .FirstOrDefault();
    
    private static MemoryStream CreateExcelFile(List<List<string>> rows, string glossaryTitle)
    {
        static void InsertCell(Row row, string cellText, int index)
        {
            row.InsertAt(new Cell
            {
                DataType = CellValues.InlineString,
                InlineString = new InlineString { Text = new Text(cellText) }
            }, index);
        }
        
        var excelStream = new MemoryStream();
        var spreadsheetDocument = SpreadsheetDocument.Create(excelStream, SpreadsheetDocumentType.Workbook);

        var workbookPart = spreadsheetDocument.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();
        
        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet();

        var headers = rows[0];
        var values = rows.Skip(1).ToList();
        
        var data = new SheetData();
        var rowId = 0;
        var row = new Row();
        
        for (var i = 0; i < headers.Count; i++)
        {
            InsertCell(row, headers[i], i);
        }
        
        data.InsertAt(row, rowId++);

        foreach (var value in values)
        {
            row = new Row();
            
            for (int i = 0; i < value.Count; i++)
            {
                InsertCell(row, value[i], i);
            }
            
            data.InsertAt(row, rowId++);
        }

        worksheetPart.Worksheet.Append(data);
        
        var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

        sheets.Append(new Sheet
        {
            Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = glossaryTitle
        });
        
        workbookPart.Workbook.Save();
        
        spreadsheetDocument.Dispose();

        excelStream.Position = 0;
        return excelStream;
    }

    #endregion
    
    #region Export

    public static Glossary ToGlossary(this Stream excelStream, DictionaryDto dictionaryDto, string glossaryTitle, 
        string glossarySourceDescription)
    {
        var parsedExcel = excelStream.ParseXlsxFile();
        
        var tbxSourceLanguage = LanguageWeaverToTbxLanguages[dictionaryDto.Source];
        var tbxTargetLanguage = LanguageWeaverToTbxLanguages[dictionaryDto.Target];

        var entries = new List<GlossaryConceptEntry>();
        var length = parsedExcel.Values.First().Count;

        for (var i = 0; i < length; i++)
        {
            var sourceTerm = parsedExcel[Source][i];
            var targetTerm = parsedExcel[Target][i];
            var comment = parsedExcel[Comment][i];

            var languageSections = new List<GlossaryLanguageSection>
            {
                new(tbxSourceLanguage,
                    new List<GlossaryTermSection> { new(sourceTerm) { Notes = comment.Split("; ").ToList() } }),
                new(tbxTargetLanguage, new List<GlossaryTermSection> { new(targetTerm) })
            };
            
            entries.Add(new(Guid.NewGuid().ToString(), languageSections));
        }

        var glossary = new Glossary(entries) { Title = glossaryTitle, SourceDescription = glossarySourceDescription };
        return glossary;
    }
    
    #endregion
}