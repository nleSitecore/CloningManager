using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore;

namespace SharedSource.CloningManager
{
    public static class Util
    {
        public static bool HasClones(Item item)
        {
            Sitecore.Links.ItemLink[] links = Globals.LinkDatabase.GetReferrers(item);

            foreach (Sitecore.Links.ItemLink link in links)
            {                
                Item referedItem = link.GetSourceItem();
                if (referedItem.IsClone)
                {
                    if (link.GetTargetItem().ID == item.ID)
                        return true;                 
                }
            }
            return false;
        }

        public static List<Item> GetClones(Item item)
        {
            List<Item> cloneItems = new List<Item>();
            Sitecore.Links.ItemLink[] links = Globals.LinkDatabase.GetReferrers(item);

            foreach (Sitecore.Links.ItemLink link in links)
            {
                Item referedItem = link.GetSourceItem();
                if (referedItem.IsClone)
                {                    
                    if (item.ID == link.GetTargetItem().ID)
                    {
                        if (!cloneItems.Exists(element => element.ID == link.GetSourceItem().ID))
                            cloneItems.Add(link.GetSourceItem());
                    }
                }
            }
            return cloneItems;
        }
    }
}
