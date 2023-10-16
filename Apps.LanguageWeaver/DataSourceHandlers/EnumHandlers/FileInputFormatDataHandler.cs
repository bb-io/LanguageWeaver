using Apps.LanguageWeaver.Constants;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers
{
    internal class FileInputFormatDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => FileInputFormats.All.ToDictionary(x => x.Name, x => x.Name);
    }
}
