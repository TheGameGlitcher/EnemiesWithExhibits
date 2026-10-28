using EnemiesWithExhibits.Source.StatusEffects.Templates;
using JetBrains.Annotations;
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
using LBoL.EntityLib.StatusEffects.Koishi;
using LBoL.EntityLib.StatusEffects.Neutral.Green;
using LBoL.EntityLib.StatusEffects.Sakuya;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Mythic
{
    public sealed class BorderSensorSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(BorderSensorSeDef))]
    public sealed class BorderSensorSe : EnemiesWithExhibitsStatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<DamageEventArgs>(base.Owner.DamageReceiving, new GameEventHandler<DamageEventArgs>(this.OnDamageRecieving));
        }

        private void OnDamageRecieving(DamageEventArgs args)
        {
            var damageInfo = args.DamageInfo;

            var burst = base.Battle.Player.GetStatusEffect<Burst>();
            var passion = base.Battle.Player.GetStatusEffect<MoodPassion>();
            var evilMaid = base.Battle.Player.GetStatusEffect<EvilMaidDoubleAttack>();
            var butterflyPill = base.Battle.Player.GetStatusEffect<YonglinBurstSe>();

            var vulnerable = this.Owner.GetStatusEffect<Vulnerable>();
            var lockOn = this.Owner.GetStatusEffect<LockedOn>();

            if (vulnerable != null)
                damageInfo.Damage = (float)Math.Round(damageInfo.Amount / (1f + (float)vulnerable.Value / 100f));

            if (lockOn != null)
                damageInfo = damageInfo.ReduceBy(lockOn.Level);

            if (burst != null)
                damageInfo.Damage = damageInfo.Amount / burst.DamageRate;

            if (passion != null)
                damageInfo.Damage = damageInfo.Amount / passion.AttackRatio;

            if (evilMaid != null)
                damageInfo.Damage = damageInfo.Amount / evilMaid.DamageRate;

            if (butterflyPill != null)
                damageInfo.Damage = damageInfo.Amount / butterflyPill.DamageRate;

            if (base.Battle.Player.TotalFirepower > 0)
                damageInfo = damageInfo.ReduceBy(base.Battle.Player.TotalFirepower);

            if (damageInfo.Amount != args.DamageInfo.Amount)
            {
                args.DamageInfo = damageInfo;
                args.AddModifier(this);

                if (args.Cause != ActionCause.OnlyCalculate)
                    this.NotifyActivating();
            }
        }
    }
}
