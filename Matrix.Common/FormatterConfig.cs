﻿using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace Matrix.Common
{
    public static class FormatterConfig
    {
        public static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.None;
        }
    }
}
