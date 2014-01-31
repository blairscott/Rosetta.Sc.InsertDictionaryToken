using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Pipelines.RenderField;
using System.Text.RegularExpressions;

namespace Rosetta.Sc.InsertDictionaryToken.Pipelines.RenderField
{

    /// <summary>
    /// Code adapted from http://blog.xcentium.com/2013/04/tokenizing-sitecores-dictionary/
    /// </summary>
    public class ReplaceDictionaryToken
    {
        private static string tokenPattern = String.Format(Config.TokenFormat, Config.RegexPattern);       

        public void Process(RenderFieldArgs args)
        {
            // if in page editing mode then ignore this processor
            if (Sitecore.Context.PageMode.IsPageEditorEditing)
            {
                return;
            }

            Assert.ArgumentNotNull(args, "args");

            if (!string.IsNullOrEmpty(args.Result.FirstPart))
            {
                args.Result.FirstPart = ProcessLines(args.Result.FirstPart);
            }

            if (!string.IsNullOrEmpty(args.Result.LastPart))
            {
                args.Result.LastPart = ProcessLines(args.Result.LastPart);
            }
        }

        protected string ProcessLines(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            Regex rgx = new Regex(tokenPattern);

            // search for tokens and return dictionary value
            foreach (Match match in rgx.Matches(text))
            {
                string matchValue = match.Value;
                string definitionValue = Translate.Text(Regex.Replace(matchValue, tokenPattern, "$1"));
                text = text.Replace(matchValue, definitionValue);
            }

            return text;
        }
    }
}
