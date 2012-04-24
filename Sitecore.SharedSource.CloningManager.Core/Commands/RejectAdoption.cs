using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore;
using Sitecore.Data;

namespace SharedSource.CloningManager.Commands
{
    public class RejectAdoption: Command
    {
        public override void Execute(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                Item item = context.Items[0];

                item.Editing.BeginEdit();
                Sitecore.Data.Fields.CheckboxField chkAdopt = item.Fields["AdoptFromOriginal"];
                chkAdopt.Checked = false;
                item.Editing.EndEdit();
            }
        }
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length == 1)
            {
                Item item = context.Items[0];
                Sitecore.Data.Fields.CheckboxField chkAdopt = item.Fields["AdoptFromOriginal"];
                if ((item != null) && !item.IsClone)
                    return CommandState.Hidden;
                if (((item != null) && chkAdopt.Checked && item.IsClone) && (((item.SourceUri != null) && item.Security.CanWrite(Context.User)) && (Database.GetItem(item.SourceUri) != null)))
                {
                    return CommandState.Enabled;
                }
            }
            return CommandState.Disabled;
        }
    }
}
