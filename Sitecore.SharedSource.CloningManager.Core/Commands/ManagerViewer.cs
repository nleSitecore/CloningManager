using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using Sitecore.Text;
using Sitecore;
namespace SharedSource.CloningManager.Commands
{
    public class ManagerViewer : Command
    {
        public override void Execute(CommandContext context)
        {
            Error.AssertObject(context, "context");
            if ((context.Items.Length == 1) && (context.Items[0] != null))
            {
                Item item = context.Items[0];
               UrlString str = new UrlString("/sitecore modules/Shell/Cloning Manager/Controls/CloningInfo.aspx");
                str.Append("id", item.ID.ToString());
                str.Append("la", item.Language.ToString());
                str.Append("vs", item.Version.ToString());                
                Sitecore.Context.ClientPage.ClientResponse.ShowModalDialog(str.ToString());
            }
        }

        public override CommandState QueryState(CommandContext context)
        {
            Error.AssertObject(context, "context");
            if (context.Items.Length == 1)
            {
                Item item = context.Items[0];
                if (!Util.HasClones(item))
                    return CommandState.Hidden;
            }
            return base.QueryState(context);
        }
    }
}
