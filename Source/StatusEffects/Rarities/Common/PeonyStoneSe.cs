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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class PeonyStoneSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.HasCount = true;

            return config;
        }
    }

    [EntityLogic(typeof(PeonyStoneSeDef))]
    public sealed class PeonyStoneSe : EnemiesWithExhibitsStatusEffect
    {
        public PeonyStoneSe() : base()
        {
            GunName = GunNameID.GetGunFromId(60000); // ExhMudan

            Value1 = 5;

            Value2 = 6;

            Mana = ManaGroup.Single(ManaColor.Red);
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<ManaEventArgs>(base.Battle.ManaConsumed, new EventSequencedReactor<ManaEventArgs>(this.OnManaConsumed));

            Count = 0;
        }

        private IEnumerable<BattleAction> OnManaConsumed(ManaEventArgs args)
        {
            Count += args.Value.Red;
            Count += args.Value.Philosophy;

            ValueTuple<int, int> valueTuple = Count.DivRem(base.Value1);

            int item = valueTuple.Item1;
            int item2 = valueTuple.Item2;

            Count = item2;

            if (item != 0)
            {
                base.NotifyActivating();
                yield return new DamageAction(base.Owner, base.Battle.Player, DamageInfo.Reaction((float)(base.Value2 * item), false), GunName, GunType.Single);
            }

            yield break;
        }
    }
}
