using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Security.AccessControl;
using System.Collections.Specialized;

namespace SharedSource.CloningManager.Security
{
    public class AdoptionAccessRight : AccessRight
    {
        public AdoptionAccessRight(string name) : base(name) { }
        public override void Initialize(NameValueCollection config)
        {
            base.Initialize(config);
            this.BucketFieldName = config["AdoptionFieldName"];
        }
        
        public string BucketFieldName { get; private set; }

        public static AccessRight AdoptionChange
        {
            get
            {
                return FromName("adoption:change");
            }
        }


    }
}
