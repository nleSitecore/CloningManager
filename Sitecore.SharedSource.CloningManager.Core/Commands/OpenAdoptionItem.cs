namespace SharedSource.CloningManager.Commands
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Shell.Framework.Commands.UserManager;
    using Sitecore.Web.UI.Sheer;
    using System.Collections.Specialized;
    using Sitecore.Web.UI.WebControls;
    using Sitecore;
    using Sitecore.Text;
    using Sitecore.Globalization;
    using System;

    public class OpenAdoptionItem : OpenItemSecurityEditor
    {
        public override void Execute(CommandContext context)
        {
            /*TODO
              * Prüfung auf Cloned und Adoption Item
              * Prüfen auf Berechtigung Adoption:Change
              * JA: Disable field editing! Info Anzeige dass Item Adopted ist und nicht geändert werden darf
              * 
              */

            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length == 1)
            {
                NameValueCollection parameters = new NameValueCollection();
                parameters["items"] = base.SerializeItems(context.Items);
                parameters["domainname"] = context.Parameters["domainname"];
                parameters["accountname"] = context.Parameters["accountname"];
                parameters["accounttype"] = context.Parameters["accounttype"];
                parameters["fieldid"] = context.Parameters["fieldid"];
                ClientPipelineArgs args = new ClientPipelineArgs(parameters);
                if (ContinuationManager.Current != null)
                {
                    ContinuationManager.Current.Start(this, "Run", args);
                }
                else
                {
                    Context.ClientPage.Start(this, "Run", args);
                }
            }

        }

        protected void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Item[] itemArray = base.DeserializeItems(args.Parameters["items"]);
            Item item = itemArray[0];
            if (itemArray.Length == 0)
            {
                SheerResponse.Eval(String.Format("alert('{0}');", Translate.Text("The item may have been deleted by another user or you do not have permission to access the item.", false )));
                Context.ClientPage.SendMessage(this, "item:refresh");
            }
            else if (SheerResponse.CheckModified())
            {
                if (args.IsPostBack)
                {
                    if (AjaxScriptManager.Current != null)
                    {
                        AjaxScriptManager.Current.Dispatch("itemsecurity:changed");
                    }
                    else
                    {
                        Context.ClientPage.SendMessage(this, "itemsecurity:changed");
                        Context.ClientPage.SendMessage(this, String.Format("item:refresh(id={0})", item.ID.ToString()));
                    }
                }
                else if (item.Appearance.ReadOnly)
                {
                    SheerResponse.Alert("You cannot edit the '{0}' item because it is protected.", new string[] { item.DisplayName });
                }
                else if (!item.Access.CanWrite())
                {
                    SheerResponse.Alert("You cannot edit this item because you do not have write access to it.", new string[0]);
                }
                else if (!item.Access.CanAdmin())
                {
                    SheerResponse.Alert("You cannot set security for the '{0}' item because you do not have administrative access.", new string[] { item.DisplayName });
                }
                else
                {
                    UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.Security.ItemSecurityEditor.aspx");
                    item.Uri.AddToUrlString(urlString);
                    urlString["do"] = StringUtil.GetString(new string[] { args.Parameters["domainname"] });
                    urlString["ac"] = StringUtil.GetString(new string[] { args.Parameters["accountname"] });
                    urlString["at"] = StringUtil.GetString(new string[] { args.Parameters["accounttype"] });
                    urlString["fld"] = StringUtil.GetString(new string[] { args.Parameters["fieldid"] });
                    SheerResponse.ShowModalDialog(urlString.ToString(), true);
                    args.WaitForPostBack();
                }
            }

        }
    }
}