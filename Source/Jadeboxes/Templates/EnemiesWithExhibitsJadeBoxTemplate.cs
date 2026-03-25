using EnemiesWithExhibits.Source.Config;
using EnemiesWithExhibits.Source.Localization;
using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnemiesWithExhibits.Source.Jadeboxes.Templates
{
    public class EnemiesWithExhibitsJadeBoxTemplate : JadeBoxTemplate
    {
        public override IdContainer GetId()
        {
            return EnemiesWithExhibitsDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return EnemiesWithExhibitsLocalization.JadeBoxBatchLoc.AddEntity(this);
        }

        public override JadeBoxConfig MakeConfig()
        {
            return GetDefaultJadeBoxConfig();
        }

        public JadeBoxConfig GetDefaultJadeBoxConfig()
        {
            return EnemiesWithExhibitsDefaultConfig.DefaultJadeBoxConfig();
        }

    }
}
