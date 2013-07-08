using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;

namespace SharedSource.CloningManager.Security
{
    public class AdoptionAuthorizationProvider : SqlServerAuthorizationProvider
    {
        private ItemAuthorizationHelper _itemHelper = new AdoptionAuthorizationHelper();

        // Methods
        protected override void AddAccessResultToCache(ISecurable entity, Account account, AccessRight accessRight, AccessResult accessResult, PropagationType propagationType)
        {
            if (accessRight.Name != "adoption:change")
            {
                base.AddAccessResultToCache(entity, account, accessRight, accessResult, propagationType);
            }
        }

        // Properties
        protected override ItemAuthorizationHelper ItemHelper
        {
            get
            {
                return this._itemHelper;
            }
            set
            {
                this._itemHelper = value;
            }
        }
    }
}
