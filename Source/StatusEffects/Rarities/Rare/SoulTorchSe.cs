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
    public sealed class SoulTorchSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(SoulTorchSeDef))]
    public sealed class SoulTorchSe : EnemiesWithExhibitsStatusEffect
    {
        public SoulTorchSe() : base()
        {
            Counter = 1;

            Value1 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<DamageEventArgs>(this.Owner.DamageTaking, new GameEventHandler<DamageEventArgs>(this.OnOwnerDamageTaking));

            this.Highlight = true;
        }

        private void OnOwnerDamageTaking(DamageEventArgs args)
        {
            if (Counter > 0 && args.DamageInfo.Damage.RoundToInt() > 0)
            {
                base.NotifyActivating();

                args.DamageInfo = args.DamageInfo.ReduceActualDamageBy(args.DamageInfo.Damage.RoundToInt());

                args.AddModifier(this);

                this.Highlight = false;

                Counter--;
            }
        }
    }
}
