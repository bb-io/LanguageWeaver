using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers
{
    internal class TranslationModeDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {"quality", "Quality"},
            {"speed", "Speed"}
        };
    }
}