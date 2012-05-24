using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;

namespace SharedSource.CloningManager.Data
{
    public class CloningItem //: BaseItem
    {
        private Item _item;
        public CloningItem(Item item)
        {
           this._item = item;
        }
        public bool IsAdopt
        {
            get
            { return new AdoptionManager(_item).IsAdopt; }
        }

        public void DoAdoption()
        {
            _item.Editing.BeginEdit();
            Sitecore.Data.Fields.CheckboxField chkAdopt = _item.Fields["AdoptFromOriginal"];
            chkAdopt.Checked = true;
            _item.Editing.EndEdit();
            AdoptionManager aManager = new AdoptionManager(_item);
            aManager.DoAdoption();
        }

        public void RejectAdoption()
        {
            _item.Editing.BeginEdit();
            Sitecore.Data.Fields.CheckboxField chkAdopt = _item.Fields["AdoptFromOriginal"];
            chkAdopt.Checked = false;
            _item.Editing.EndEdit();
        }
    }
}
