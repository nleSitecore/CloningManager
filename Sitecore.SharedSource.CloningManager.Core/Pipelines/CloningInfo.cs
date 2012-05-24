using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Pipelines.GetChromeData;
using Sitecore.Diagnostics;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace SharedSource.CloningManager.Pipelines.ChromeData
{
    public class CloningInfo : GetChromeDataProcessor
    {
        public const string ChromeType = "field";
        public const string FieldKey = "field";

        public override void Process(GetChromeDataArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.IsNotNull(args.ChromeData, "Chrome Data");
            if ("field".Equals(args.ChromeType, StringComparison.OrdinalIgnoreCase))
            {
                Field argument = args.CustomData["field"] as Field;
                Item currentItem = args.Item;
                if (currentItem != null)
                {
                    if (currentItem.IsClone)
                    {
                        args.ChromeData.DisplayName = string.Format("<b>This field is a Clone!</b> {0}", argument.DisplayName);
                        args.ChromeData.ExpandedDisplayName = string.Format("If you change this field, changes from the original Item will be ignored! {0}", argument.ToolTip);
                    }
                }
            }
        }
    }
}
