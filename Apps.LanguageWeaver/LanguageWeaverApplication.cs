using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.LanguageWeaver;

public class LanguageWeaverApplication : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.MachineTranslationAndMtqe];
        set { }
    }
    
    public string Name
    {
        get => "Language Weaver";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}