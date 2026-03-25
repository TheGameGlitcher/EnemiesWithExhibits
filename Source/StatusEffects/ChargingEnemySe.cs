using EnemiesWithExhibits.Source.StatusEffects.Templates;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace EnemiesWithExhibits.Source.StatusEffects
{
    public sealed class ChargingEnemySeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Type = StatusEffectType.Positive;

            config.StackActionTriggerLevel = 8;

            config.HasLevel = true;

            config.RelativeEffects = new List<string>() { "BurstEnemySe" };

            config.SFX = "Charging";

            return config;
        }
    }

    [EntityLogic(typeof(ChargingEnemySeDef))]
    public sealed class ChargingEnemySe : EnemiesWithExhibitsStatusEffect
    {
        public override string UnitEffectName
        {
            get
            {
                return "MarisaChargingLoop";
            }
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");
        }

        public override IEnumerable<BattleAction> StackAction(Unit targetOwner, int targetLevel)
        {
            yield return new ApplyStatusEffectAction<BurstEnemySe>(targetOwner, targetLevel, null, null, null, 0f, true);

            yield break;
        }

    }
}
