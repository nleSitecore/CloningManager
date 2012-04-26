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

    }
}
