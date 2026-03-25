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
    public sealed class HarvestFruitBasketSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(HarvestFruitBasketSeDef))]
    public sealed class HarvestFruitBasketSe : EnemiesWithExhibitsStatusEffect
    {
        public HarvestFruitBasketSe() : base()
        {
            Value1 = 5;
            Value2 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            Value3 = Value1 + Value2 * base.GameRun.BaseDeck.Count();

            this.NotifyActivating();

            base.Battle.GameRun.SetEnemyHpAndMaxHp(unit.Hp + Value3, unit.MaxHp + Value3, (EnemyUnit)unit);
        }
    }
}
