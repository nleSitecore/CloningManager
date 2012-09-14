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
using Sitecore.Globalization;
using SharedSource.CloningManager.Data;

namespace Sitecore.SharedSource.CloningManager.Controls
{
    public partial class CloningInfo : System.Web.UI.Page
    {
        private Item _currentItem;
        private Language _language;
        public Item CurrentItem
        {
            get
            {
                if (_currentItem == null)
                {
                    string currentID = WebUtil.GetQueryString("id");
                    string languageName = WebUtil.GetQueryString("la");
                    _language = Sitecore.Data.Managers.LanguageManager.GetLanguage(languageName);                    
                    _currentItem =  Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new ID(currentID));
                }
                return _currentItem;                                                                                                                        
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Item> clones = Util.GetClones(CurrentItem);
            if (!IsPostBack)
            {
            
                ListView1.DataSource = clones;
                ListView1.DataBind();
            }
        }
        
        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Item item =(Item) ((ListViewDataItem)e.Item).DataItem;
            Label lbVersion = (Label)e.Item.FindControl("Label1");            
            HyperLink hylPath = (HyperLink)e.Item.FindControl("HyperLink1");
            CheckBox chkAdopt = (CheckBox)e.Item.FindControl("CheckBox1");
            lbVersion.Text = item.SourceUri.Version.ToString();

            if (item.SourceUri.Version == CurrentItem.Version)
            {
                lbVersion.Text = "Current";                
            }
            
            string st = string.Format("http://{0}/sitecore/shell/Applications/Content Manager/default.aspx?ro={1}&fo={2}&mo={3}&la={4}&v={5}", WebUtil.GetHostName(), item.ID.ToString(), item.ID.ToString(), "preview", Sitecore.Context.Language.Name, item.Version.ToString());            
            hylPath.Attributes.Add("onclick","window.open('" + st+"')");
            hylPath.NavigateUrl = "#";
            AdoptionManager adoptManager = new AdoptionManager(item);
            if (adoptManager.IsAdopt)
                chkAdopt.Checked = true;                        
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                CheckBox chk = ListView1.Items[i].FindControl("CheckBox1") as CheckBox;
                Literal litItemId = ListView1.Items[i].FindControl("ItemID") as Literal;
                Item listItem = Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new ID(new Guid(litItemId.Text)), _language, Data.Version.Latest);
                if (listItem != null)
                {
                    CloningItem cloneItem = new CloningItem(listItem);
                    if (chk.Checked) cloneItem.DoAdoption();
                    else cloneItem.RejectAdoption();
                }
            }
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnDone.Visible = true;

        }
      
    }
}