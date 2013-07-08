using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedSource.CloningManager.Configuration
{
    public sealed class Settings
    {
        public static bool DisableAdoptionChangeRight
        {
            get
            {
                return Sitecore.Configuration.Settings.GetBoolSetting("DisableAdoptionChangeRight", false);
            }
        }

    }
}
