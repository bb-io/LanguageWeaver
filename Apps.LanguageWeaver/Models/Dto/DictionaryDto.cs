using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Dto;

public class DictionaryDto
{
    [Display("Dictionary ID")]
    public string DictionaryId { get; set; }
  
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Display("Source language")]
    public string Source { get; set; }
    
    [Display("Target language")]
    public string Target { get; set; }
    
    [Display("Created by")]
    public string CreatedBy { get; set; }
    
    [Display("Creation date")]
    public DateTime CreationDate { get; set; }
}