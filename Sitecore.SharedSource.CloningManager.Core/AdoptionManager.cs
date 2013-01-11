using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Security.Accounts;
using Sitecore.Security.AccessControl;
using SharedSource.CloningManager.Security;
using Sitecore.Configuration;

namespace SharedSource.CloningManager
{
    public class AdoptionManager
    {
        Item _item;
        Item _originalItem;
        public AdoptionManager(Item item)
        {
            this._item = item;
            this._originalItem = _item.Source;
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

        public List<Field> GetChangedFields()
        {
            List<Field> allFields = _item.Fields.ToList();
            var fields = from f in allFields where f.InheritsValueFromOtherItem == false select f;            
            return fields.ToList();
        }

        public List<Field> GetChangedFieldsWithoutStandardFields()
        {
            List<Field> allFields = GetChangedFields();
            var fields = from f in allFields where !f.Name.StartsWith("__") && f.Name != "AdoptFromOriginal" select f;
            return fields.ToList();
        }
        public void DoAdoption()
        {

            if (_originalItem != null)
            {
                Sitecore.Data.ItemUri uri = new Sitecore.Data.ItemUri(_item.SourceUri);
                Sitecore.Data.ItemUri uriOrg = new Sitecore.Data.ItemUri(_originalItem.Versions.GetLatestVersion());
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    if (uri.Version != uriOrg.Version && (_item.Database.NotificationProvider != null))
                    {
                        _item.Editing.BeginEdit();
                        Sitecore.Data.Fields.DateField validFromClone = _item.Fields["__Valid From"];
                        Sitecore.Data.Fields.DateField validFromOriginal = _originalItem.Fields["__Valid From"];
                        validFromClone = validFromOriginal;


                        _item.Editing.EndEdit();
                        foreach (Sitecore.Data.Clones.Notification notification in _item.Database.NotificationProvider.GetNotifications(_item))
                        {
                            notification.Accept(_item);
                        }
                    }
                    //ToDO if (!SharedSource.CloningManager.Configuration.Settings.DisableAdoptionChangeRight)
                    SetSecurity();
                }
            }
        }

        public void UndoAdoption()
        {
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                _item.Editing.BeginEdit();
                Sitecore.Data.Fields.CheckboxField chkAdopt = _item.Fields["AdoptFromOriginal"];
                chkAdopt.Checked = false;
                _item.Editing.EndEdit();
                //ToDO if (!SharedSource.CloningManager.Configuration.Settings.DisableAdoptionChangeRight)
                RemoveSecurity();
            }
        }

        private void RemoveSecurity()
        {           
                _item.Editing.BeginEdit();
                Role everybody = Role.FromName("sitecore\\Everyone");
                AccessRuleCollection accessRules = _item.Security.GetAccessRules();
                accessRules.Helper.RemoveExactMatches(everybody, AdoptionAccessRight.AdoptionChange, PropagationType.Any);
                _item.Security.SetAccessRules(accessRules);
                _item.Editing.EndEdit();           
        }

        private void SetSecurity()
        {           
                _item.Editing.BeginEdit();
                Role everybody = Role.FromName("sitecore\\Everyone");
                AccessRuleCollection accessRules = _item.Security.GetAccessRules();
                accessRules.Helper.AddAccessPermission(everybody, AdoptionAccessRight.AdoptionChange, PropagationType.Any, AccessPermission.Deny);
                _item.Security.SetAccessRules(accessRules);
                _item.Editing.EndEdit();           
        }
    }
}
