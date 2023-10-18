using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers
{
    internal class PdfConverterDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {"STANDARD", "Standard"},
            {"ABBYY", "ABBYY"},
            {"SMART_SELECTION", "Smart selection"}
        };
    }
}