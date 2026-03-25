using EnemiesWithExhibits.Source.GunName;
using EnemiesWithExhibits.Source.Misc;
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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Rare
{
    public sealed class JeweledPagodaSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(JeweledPagodaSeDef))]
    public sealed class JeweledPagodaSe : EnemiesWithExhibitsStatusEffect
    {
        public JeweledPagodaSe() : base()
        {
            Value1 = 0;
            Value2 = 1;
            Value3 = 2;
            Value4 = 3;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            switch (base.Battle.EnemyGroup.EnemyType)
            {
                case EnemyType.Normal:
                    Value1 += Value2;
                    break;

                case EnemyType.Elite:
                    Value1 += Value3;
                    break;

                case EnemyType.Boss:
                    Value1 += Value4;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.NotifyActivating();

            React(new ApplyStatusEffectAction<Firepower>(this.Owner, Value1, null, null, null, 0.2f));
        }
    }
}
