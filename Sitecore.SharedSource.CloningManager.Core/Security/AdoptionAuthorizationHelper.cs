using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Security.AccessControl;
using Sitecore.Data.Items;
using Sitecore.Security.Accounts;

namespace SharedSource.CloningManager.Security
{
    public class AdoptionAuthorizationHelper : ItemAuthorizationHelper 
    {
        protected virtual AccessResult GetItemAccess(Item item, Account account, AdoptionAccessRight right)
        {
            return new AccessResult(AccessPermission.Allow, new AccessExplanation("This cloned item can be adopted", new object[0]));
        }


        protected override AccessResult GetItemAccess(Item item, Account account, AccessRight accessRight, PropagationType propagationType)
        {
              AccessResult o = base.GetItemAccess(item, account, accessRight, propagationType);
        if (o!= null && (o.Permission == AccessPermission.Allow))
        {
            if (accessRight.Name != "adoption:change")
            {
                return o;
            }
            AdoptionAccessRight right = accessRight as AdoptionAccessRight;
            if (right != null)
            {
                o = this.GetItemAccess(item, account, right);
            }
        }
        return o;
    }


       
    }
}
