using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver;

public class LanguageWeaverApplication : IApplication
{
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