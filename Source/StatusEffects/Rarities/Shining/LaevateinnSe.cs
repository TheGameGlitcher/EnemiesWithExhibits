using EnemiesWithExhibits.Source.GunName;
using EnemiesWithExhibits.Source.StatusEffects.Templates;
using LBoL.Base;
using LBoL.Base.Extensions;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Intentions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class LaevateinnSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Vulnerable" };

            return config;
        }
    }

    [EntityLogic(typeof(LaevateinnSeDef))]
    public sealed class LaevateinnSe : EnemiesWithExhibitsStatusEffect
    {
        public LaevateinnSe() : base()
        {
            Value1 = 50;
            Value2 = 25;
        }

        protected override void OnAdded(Unit unit)
        {
            base.GameRun.PlayerVulnerableExtraPercentage += base.Value1;
            base.GameRun.EnemyVulnerableExtraPercentage += base.Value2;
        }

        protected override void OnRemoved(Unit unit)
        {
            base.GameRun.PlayerVulnerableExtraPercentage -= base.Value1;
            base.GameRun.EnemyVulnerableExtraPercentage -= base.Value2;
        }
    }
}
