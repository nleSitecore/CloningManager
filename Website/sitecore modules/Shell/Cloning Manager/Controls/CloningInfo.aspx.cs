using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Web;
using Sitecore.Data;
using SharedSource.CloningManager;

namespace Sitecore.SharedSource.CloningManager.Controls
{
    public partial class CloningInfo : System.Web.UI.Page
    {
        private Item currentItem;

        public Item CurrentItem
        {
            get
            {
                if (currentItem == null)
                {
                    string currentID = WebUtil.GetQueryString("id");
                    currentItem =  Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new ID(currentID));
                }
                return currentItem;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            List<Item> clones = Util.GetClones(CurrentItem);
             
            foreach (Item clone in clones)
            {   
                ltInfo.Text += " " + clone.Paths.FullPath + " Version: " +clone.Version + "<br />";
            }
                
            
        }
    }
}