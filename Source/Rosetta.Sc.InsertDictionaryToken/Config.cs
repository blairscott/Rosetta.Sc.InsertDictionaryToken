using Sitecore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosetta.Sc.InsertDictionaryToken
{
    public class Config
    {
        public static string TokenFormat
        {
            get
            {
                return Settings.GetSetting("Rosetta.Sc.InsertDictionaryToken.TokenFormat", "%%{0}%%");
            }
        }

        public static string RegexPattern
        {
            get
            {
                return Settings.GetSetting("Rosetta.Sc.InsertDictionaryToken.RegexPatter", "([a-zA-Z0-9 ]+)");
            }
        }
    }
}