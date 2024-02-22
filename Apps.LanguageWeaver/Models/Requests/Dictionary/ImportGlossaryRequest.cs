using Apps.LanguageWeaver.DataSourceHandlers;
using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageWeaver.Models.Requests.Dictionary;

public class ImportGlossaryRequest
{
    public FileReference Glossary { get; set; }
    
    [Display("Source language", Description = "Source language for a new dictionary, which must be specified " +
                                              "when the 'Dictionary ID' parameter is not set")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? Source { get; set; }

    [Display("Target language", Description = "Target language for a new dictionary, which must be specified " +
                                              "when the 'Dictionary ID' parameter is not set")]
    [DataSource(typeof(LanguageDataHandler))]
    public string? Target { get; set; }
    
    [Display("Dictionary ID", Description = "An existing dictionary to import terms into")]
    [DataSource(typeof(DictionaryDataHandler))]
    public string? DictionaryId { get; set; }
    
    [Display("Overwrite duplicate terms", Description = "Value specifying whether imported elements with the " +
                                                        "same source term should overwrite existing values when " +
                                                        "importing into an existing dictionary")]
    public bool? OverwriteDuplicates { get; set; }
    
    [Display("New dictionary title", Description = "Title for a new dictionary when the 'Dictionary ID' parameter " +
                                                   "is not set, indicating the creation of a new dictionary")]
    public string? DictionaryTitle { get; set; }
    
    [Display("New dictionary description", Description = "Description for a new dictionary when the 'Dictionary " +
                                                         "ID' parameter is not set, indicating the creation of a " +
                                                         "new dictionary")]
    public string? DictionaryDescription { get; set; }
}