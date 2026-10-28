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

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Common
{
    public sealed class PeonySeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.HasLevel = true;

            config.HasCount = true;

            return config;
        }
    }

    [EntityLogic(typeof(PeonySeDef))]
    public sealed class PeonySe : EnemiesWithExhibitsStatusEffect
    {
        public PeonySe() : base()
        {
            Value1 = 5;

            Value2 = 3;

            Value3 = 2;

            Mana = ManaGroup.Single(ManaColor.White);
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<ManaEventArgs>(base.Battle.ManaConsumed, new GameEventHandler<ManaEventArgs>(this.OnManaConsumed));

            Level = 2;
            Count = 0;
        }

        private void OnManaConsumed(ManaEventArgs args)
        {
            if (Level > 0)
            {
                Count += args.Value.White;
                Count += args.Value.Philosophy;

                ValueTuple<int, int> valueTuple = Count.DivRem(base.Value1);

                int div = valueTuple.Item1;
                int item = valueTuple.Item2;

                Count = item;

                if (div != 0)
                {
                    div = Math.Min(div, Level);

                    base.NotifyActivating();

                    React(new HealAction(base.Owner, base.Owner, base.Value2 * div, HealType.Normal, 0.2f));

                    Level -= div;

                    if (Level <= 0)
                        Count = 0;
                }
            }
        }
    }
}
