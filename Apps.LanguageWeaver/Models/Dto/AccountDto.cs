using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Dto;

public class AccountDto
{
    [Display("Account ID")]
    public string AccountId { get; set; }
    
    [Display("Client ID")]
    public string ClientId { get; set; }
}