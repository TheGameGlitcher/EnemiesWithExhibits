using EnemiesWithExhibits.Source.StatusEffects.Templates;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace EnemiesWithExhibits.Source.StatusEffects
{
    public sealed class BurstEnemySeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Type = StatusEffectType.Positive;

            config.HasLevel = true;

            return config;
        }
    }

    [EntityLogic(typeof(BurstEnemySeDef))]
    public sealed class BurstEnemySe : EnemiesWithExhibitsStatusEffect
    {

        public BurstEnemySe() : base()
        {
            Value1 = 2;
        }

        public override bool ForceNotShowDownText
        {
            get
            {
                return true;
            }
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<DamageDealingEventArgs>(base.Owner.DamageDealing, new GameEventHandler<DamageDealingEventArgs>(this.OnDamageDealing));
            base.ReactOwnerEvent<UnitEventArgs>(this.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));

            base.React(FirstGain(Level));


        }

        protected override void OnRemoving(Unit unit)
        {
            base.React(Lose(unit));
        }

        public override bool Stack(StatusEffect other)
        {
            bool flag = base.Stack(other);

            if (flag)
                base.React(this.StackGain(other.Level));

            return flag;
        }

        private void OnDamageDealing(DamageDealingEventArgs args)
        {
            if (args.DamageInfo.DamageType == DamageType.Attack)
            {
                args.DamageInfo = args.DamageInfo.MultiplyBy(Value1);
                args.AddModifier(this);

                if (args.Cause != ActionCause.OnlyCalculate)
                    base.NotifyActivating();
            }
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(UnitEventArgs args)
        {
            yield return new RemoveStatusEffectAction(this, true, 0.1f);

            yield break;
        }

        private IEnumerable<BattleAction> FirstGain(int level = 1)
        {
            if (base.Owner.IsAlive)
            {
                yield return PerformAction.Effect(base.Owner, "MarisaBurstStart", 0f, "MarisaBurst", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                yield return PerformAction.Effect(base.Owner, "MarisaBurstLoop", 0.5f, null, 0f, PerformAction.EffectBehavior.Add, 0f);
            }

            yield break;
        }

        private IEnumerable<BattleAction> StackGain(int level = 1)
        {
            if (base.Owner.IsAlive)
            {
                yield return PerformAction.Effect(base.Owner, "MarisaBurstStart", 0f, "MarisaBurst", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                yield return PerformAction.Sfx("MarisaBurst", 0f);
            }

            yield break;
        }

        private IEnumerable<BattleAction> Lose(Unit unit)
        {
            if (unit.IsAlive)
            {
                yield return PerformAction.Effect(unit, "MarisaBurstEnd", 0f, "MarisaBurstLose", 0f, PerformAction.EffectBehavior.PlayOneShot, 0f);
                yield return PerformAction.Effect(unit, "MarisaBurstLoop", 0f, null, 0f, PerformAction.EffectBehavior.Remove, 0f);
            }

            yield break;
        }

    }
}
