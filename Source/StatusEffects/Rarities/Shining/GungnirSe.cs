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
    public sealed class GungnirSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Accuracy;

            return config;
        }
    }

    [EntityLogic(typeof(GungnirSeDef))]
    public sealed class GungnirSe : EnemiesWithExhibitsStatusEffect
    {

        protected override void OnAdded(Unit unit)
        {
            base.ReactOwnerEvent<DamageDealingEventArgs>(base.Owner.DamageDealing, new EventSequencedReactor<DamageDealingEventArgs>(this.OnDamageDealing));
        }

        private IEnumerable<BattleAction> OnDamageDealing(DamageDealingEventArgs args)
        {

            if (args.Cause == ActionCause.EnemyAction && args.DamageInfo.IsAccuracy == false)
            {
                this.NotifyActivating();

                DamageInfo damageInfo = args.DamageInfo;

                damageInfo.IsAccuracy = true;

                args.DamageInfo = damageInfo;

                args.AddModifier(this);
            }

            yield break;
        }
    }
}
