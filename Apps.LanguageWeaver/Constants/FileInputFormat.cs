using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Constants
{
    public record FileInputFormat (string Name, List<string> Extension, string MimeType);

    public static class FileInputFormats
    {
        public static List<FileInputFormat> All => new List<FileInputFormat>() 
        {
            new("PLAIN",    new List<string>{".txt"},                                                   "text/plain"),
            new("XLINE",    new List<string>{".xline"},                                                 "text/x-line"),
            new("HTML",     new List<string>{".htm", ".html", ".xhtml"},                                "text/html"),
            new("XML",      new List<string>{".xml"},                                                   "text/xml"),
            new("SDLXML",   new List<string>{".sdlxml"},                                                "text/sdlxml"),
            new("TMX",      new List<string>{".tmx"},                                                   "text/x-tmx"),
            new("XLIFF",    new List<string>{".xliff"},                                                 "application/x-xlif"),
            new("BCM",      new List<string>{".bcm"},                                                   "application/x-json-bcm"),
            new("PDF",      new List<string>{".pdf"},                                                   "application/pdf"),
            new("RTF",      new List<string>{".rtf"},                                                   "application/rtf"),
            new("DOCX",     new List<string>{".docx", ".dotx", ".docm", ".dotm"},                       "application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
            new("XLSX",     new List<string>{".xlsx", ".xltx", ".xlsm", ".xltm", ".xlam", ".xlsb"},     "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
            new("PPTX",     new List<string>{".pptx", ".potx", ".ppsx", ".pptm", ".potm", ".ppsm"},     "application/vnd.openxmlformats-officedocument.presentationml.presentation"),
            new("DOC",      new List<string>{".doc", ".dot"},                                           "application/msword"),
            new("XLS",      new List<string>{".xls", ".xlt", ".xla"},                                   "application/vnd.ms-excel"),
            new("PPT",      new List<string>{".ppt", ".pot,", ".pps"},                                  "application/vnd.ms-powerpoint"),
            new("ODT",      new List<string>{".odt"},                                                   "application/vnd.oasis.opendocument.text"),
            new("ODS",      new List<string>{".ods"},                                                   "application/vnd.oasis.opendocument.spreadsheet"),
            new("ODP",      new List<string>{".odp"},                                                   "application/vnd.oasis.opendocument.presentation"),
            new("GIF",      new List<string>{".gif"},                                                   "image/gif"),
            new("JPG",      new List<string>{".jpg", ".jpeg" },                                         "image/jpeg"),
            new("PNG",      new List<string>{".png"},                                                   "image/png"),
            new("EML",      new List<string>{".eml"},                                                   "message/rfc822"),
            new("MSG",      new List<string>{".msg"},                                                   "application/vnd"),
        };
    }
}
