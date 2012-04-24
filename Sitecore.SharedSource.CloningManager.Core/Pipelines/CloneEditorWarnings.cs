using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Pipelines.GetContentEditorWarnings;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Data.Clones;
using Sitecore.Diagnostics;

namespace SharedSource.CloningManager.Pipelines
{
    public class CloneEditorWarnings
    {
        public void Process(GetContentEditorWarningsArgs args)
        {            
            Item item = args.Item;
            Assert.ArgumentNotNull(item, "item");
            if (Util.HasClones(item))
                args.Add(Translate.Text("this item has cloned items"), GetCloneList(item).ToString() + Translate.Text("switch to cloning manager"));
            else if (item.IsItemClone)
            {
                Item originalItem = item.Source;
                if (originalItem != null)
                    args.Add(Translate.Text("this item is a clone"), Translate.Text("clone item help text", originalItem.ID.ToString(), originalItem.Language.Name, originalItem.Version));
                //Test(args, item);
            }
        }

        private StringBuilder GetCloneList(Item item)
        {
            List<Item> clones = Util.GetClones(item);
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.Append("<ul>");
            foreach (Item clone in clones)
            {
                strbuilder.Append("<li>");
                strbuilder.Append(String.Format("<a onclick=\"javascript:return scForm.postEvent(this,event,'item:load(id={0},la={1},vs={2})')\" href=\"#\" style=\"font-style:italic;\">Click here to see Clone: {3}.</a>",clone.ID.ToString(), clone.Name, clone.Version, clone.Name));                
                strbuilder.Append("</li>");
            }
            strbuilder.Append("</ul>");
            return strbuilder;
        }

        private void Test(GetContentEditorWarningsArgs args, Item item)
        {
              List<Sitecore.Data.Clones.Notification> list = new List<Sitecore.Data.Clones.Notification>(item.Database.NotificationProvider.GetNotifications(item));
             list.AddRange(this.GetAdditionalNotifications(item));
            int num = 5;
            for (int i = 0; i < ((num > list.Count) ? list.Count : num); i++)
            {
                list[i].RegisterWarnings(args, item);
            }

            
        }

        protected virtual List<Notification> GetAdditionalNotifications(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            List<Notification> list = new List<Notification>();
            if (item.IsClone && (item.Database.NotificationProvider != null))
            {
                foreach (Item item2 in item.Versions.GetVersions(true))
                {
                    if ((item2.Uri != item.Uri) && (item2.Database.NotificationProvider != null))
                    {
                        foreach (Notification notification in item2.Database.NotificationProvider.GetNotifications(item2))
                        {
                            if (notification.Uri == item2.Uri)
                            {
                                ItemVersionNotification notification2 = new ItemVersionNotification
                                {
                                    VersionUri = item2.Uri
                                };
                                list.Add(notification2);
                                break;
                            }
                        }
                    }
                }
            }
            return list;
        }

 

 

    }
}
