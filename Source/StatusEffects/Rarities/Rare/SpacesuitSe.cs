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
    public sealed class SpacesuitSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(SpacesuitSeDef))]
    public sealed class SpacesuitSe : EnemiesWithExhibitsStatusEffect
    {
        public SpacesuitSe() : base()
        {
            Value1 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<DamageEventArgs>(this.Owner.DamageTaking, new GameEventHandler<DamageEventArgs>(this.OnOwnerDamageTaking));
        }

        private void OnOwnerDamageTaking(DamageEventArgs args)
        {
            if (args.DamageInfo.Damage.RoundToInt() > 0)
            {
                base.NotifyActivating();

                args.DamageInfo = args.DamageInfo.ReduceActualDamageBy(base.Value1);

                args.AddModifier(this);
            }
        }
    }
}
