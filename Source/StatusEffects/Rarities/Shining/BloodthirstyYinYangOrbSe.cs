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
    public sealed class BloodthirstyYinYangOrbSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Firepower" };

            return config;
        }
    }

    [EntityLogic(typeof(BloodthirstyYinYangOrbSeDef))]
    public sealed class BloodthirstyYinYangOrbSe : EnemiesWithExhibitsStatusEffect
    {
        public BloodthirstyYinYangOrbSe() : base()
        {
            Value1 = 1;
            Value2 = 3;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<DamageEventArgs>(base.Owner.DamageDealt, new EventSequencedReactor<DamageEventArgs>(this.OnOwnerDamageDealt));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerDamageDealt(DamageEventArgs args)
        {
            if (!HasTriggered && args.Cause == ActionCause.EnemyAction && args.DamageInfo.Damage > 0f)
            {
                this.NotifyActivating();

                yield return new ApplyStatusEffectAction<Firepower>(this.Owner, Value1, null, null, null, 0.2f);
                yield return new HealAction(this.Owner, this.Owner, Value2);

                HasTriggered = true;
            }

            yield break;
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            HasTriggered = false;

            yield break;
        }
    }
}
