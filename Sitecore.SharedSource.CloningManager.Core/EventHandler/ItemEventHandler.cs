using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Pipelines.Save;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using Sitecore.Events;

namespace SharedSource.CloningManager.EventHandler
{
    public class ItemEventHandler
    {
        public void OnOriginalItemSaved(object sender, EventArgs args)
        {
            if (args != null)
            {
                Item item = Event.ExtractParameter(args, 0) as Item;
                Assert.IsNotNull(item, "No item in parameters");
                if (Util.HasClones(item))
                {
                    foreach (Item clone in Util.GetClones(item))
                    {
                        AdoptionManager adoptionManager = new AdoptionManager(clone);
                        if (adoptionManager.IsAdopt)
                        {
                            foreach (Sitecore.Data.Clones.Notification notification in item.Database.NotificationProvider.GetNotifications(clone))
                            {
                                notification.Accept(clone);
                                clone.Database.NotificationProvider.RemoveNotification(clone.ID);
                            }
                        }
                    }
                }
            }
        }
    }
}
