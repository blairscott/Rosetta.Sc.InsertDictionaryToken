using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Web.UI.Pages;
using Sitecore.Diagnostics;
using Sitecore;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Data.Items;
using Sitecore.Shell.Controls.RichTextEditor.InsertLink;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Rosetta.Sc.InsertDictionaryToken.Controls
{
    public class InsertDictionaryToken : DialogForm
    {
        /// <summary>
        /// The internal link tree view.
        /// 
        /// </summary>
        protected TreeviewEx InternalLinkTreeview;

        /// <summary>
        /// The internal link data context.
        /// 
        /// </summary>
        protected DataContext InternalLinkDataContext;

        //setup page
        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            if (Context.ClientPage.IsEvent)
            {
                return;
            }

            this.Mode = WebUtil.GetQueryString("mo");
            this.InternalLinkDataContext.GetFromQueryString();
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull((object)args, "args");

            string text;

            Item selectionItem = this.InternalLinkTreeview.GetSelectionItem();
            if (selectionItem == null)
            {
                SheerResponse.Alert("Select an item.", new string[0]);
                return;
            }
            else if (selectionItem.TemplateID != Sitecore.TemplateIDs.DictionaryEntry)
            {
                SheerResponse.Alert("Please select a dictionary entry", new string[0]);
                return;
            }
            else
            {
                text = String.Format(Config.TokenFormat, selectionItem.Fields["Key"].Value);
            }

            if (this.Mode == "webedit")
            {
                SheerResponse.SetDialogValue(StringUtil.EscapeJavascriptString(text));
                base.OnOK(sender, args);
            }
            else
            {
                SheerResponse.Eval("scClose(" + StringUtil.EscapeJavascriptString(text) + ")");
            }
        }

        //cancelled
        protected override void OnCancel(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            if (this.Mode == "webedit")
            {
                base.OnCancel(sender, args);
            }
            else
            {
                SheerResponse.Eval("scCancel()");
            }
        }



        // Properties
        protected string Mode
        {
            get
            {
                string str = StringUtil.GetString(base.ServerProperties["Mode"]);
                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }
                return "shell";
            }
            set
            {
                Assert.ArgumentNotNull(value, "value");
                base.ServerProperties["Mode"] = value;
            }
        }
    }
}