using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;

namespace SharedSource.CloningManager
{
    public class AdoptionManager
    {
        Item _item;
        public AdoptionManager(Item item)
        {
            this._item = item;
        }

        public bool IsAdopt
        {
            get{
                return((Sitecore.Data.Fields.CheckboxField) _item.Fields["AdoptFromOriginal"]).Checked;                
            }
            set
            {
                _item.Editing.BeginEdit();
                Sitecore.Data.Fields.CheckboxField chkAdopt = _item.Fields["AdoptFromOriginal"];
                chkAdopt.Checked = value;
                _item.Editing.EndEdit();
            }
        }
        public void DoAdoption()
        {
            Item originalItem = _item.Source;
            if (originalItem != null)
            {
                Sitecore.Data.ItemUri uri = new Sitecore.Data.ItemUri(_item.SourceUri);
                Sitecore.Data.ItemUri uriOrg = new Sitecore.Data.ItemUri(originalItem.Versions.GetLatestVersion());
                if (uri.Version != uriOrg.Version && (_item.Database.NotificationProvider != null))
                {
                    foreach (Sitecore.Data.Clones.Notification notification in _item.Database.NotificationProvider.GetNotifications(_item))
                    {
                        notification.Accept(_item);
                    }
                }
            }
        }
    }
}
