using EnemiesWithExhibits.Source.Config;
using EnemiesWithExhibits.Source.ImageLoader;
using EnemiesWithExhibits.Source.Localization;
using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Templates
{
    public class EnemiesWithExhibitsStatusEffectTemplate : StatusEffectTemplate
    {
        public override IdContainer GetId()
        {
            return EnemiesWithExhibitsDefaultConfig.DefaultID(this);
        }

        public override LocalizationOption LoadLocalization()
        {
            return EnemiesWithExhibitsLocalization.StatusEffectsBatchLoc.AddEntity(this);
        }

        public override Sprite LoadSprite()
        {
            return EnemiesWithExhibitsImageLoader.LoadStatusEffectLoader(status: this);
        }

        public override StatusEffectConfig MakeConfig()
        {
            return GetDefaultStatusEffectConfig();
        }

        public static StatusEffectConfig GetDefaultStatusEffectConfig()
        {
            return EnemiesWithExhibitsDefaultConfig.DefaultStatusEffectConfig();
        }
    }
}