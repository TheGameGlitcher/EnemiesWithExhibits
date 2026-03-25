using EnemiesWithExhibits.Source.StatusEffects.Templates;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Common
{
    public sealed class TengusFanSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Graze" };

            return config;
        }
    }

    [EntityLogic(typeof(TengusFanSeDef))]
    public sealed class TengusFanSe : EnemiesWithExhibitsStatusEffect
    {
        public TengusFanSe() : base()
        {
            Value1 = 2;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            this.NotifyActivating();

            React(new ApplyStatusEffectAction<Graze>(unit, Value1, null, null, null, 0.2f));
        }
    }
}
