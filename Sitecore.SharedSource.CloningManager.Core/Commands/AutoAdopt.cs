namespace SharedSource.CloningManager.Commands
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore;

    public class AutoAdopt : Command
    {
        public override void Execute(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                Item item = context.Items[0];
                AdoptionManager adoptionManager = new AdoptionManager(item);
                item.Editing.BeginEdit();
                Sitecore.Data.Fields.CheckboxField chkAdopt = item.Fields["AdoptFromOriginal"];
                chkAdopt.Checked = true;
                item.Editing.EndEdit();
                DoAdoption(item);
            }
        }

        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Items.Length == 1)
            {
                Item item = context.Items[0];
                Sitecore.Data.Fields.CheckboxField chkAdopt = item.Fields["AdoptFromOriginal"];
                if ((item != null) &&!item.IsClone)
                    return CommandState.Hidden;
                if (((item != null) && !chkAdopt.Checked && item.IsClone) && (((item.SourceUri != null) && item.Security.CanWrite(Context.User)) && (Database.GetItem(item.SourceUri) != null)))
                {
                    return CommandState.Enabled;
                }
            }
            return CommandState.Disabled;
        }

        protected void DoAdoption(Item item)
        {            
            AdoptionManager adoptManager = new AdoptionManager(item);
            adoptManager.DoAdoption();
        }

    }
}