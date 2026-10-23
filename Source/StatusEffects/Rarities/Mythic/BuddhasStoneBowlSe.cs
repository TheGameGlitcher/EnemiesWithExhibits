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

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Mythic
{
    public sealed class BuddhasStoneBowlSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(BuddhasStoneBowlSeDef))]
    public sealed class BuddhasStoneBowlSe : EnemiesWithExhibitsStatusEffect
    {
        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<BlockShieldEventArgs>(base.Owner.BlockShieldCasted, new EventSequencedReactor<BlockShieldEventArgs>(this.OnBlockShieldCasted));
        }

        private IEnumerable<BattleAction> OnBlockShieldCasted(BlockShieldEventArgs args)
        {
            if (args.Cause == ActionCause.EnemyAction)
            {
                this.NotifyActivating();

                yield return new CastBlockShieldAction(this.Owner, args.Block.RoundToInt(), args.Shield.RoundToInt());
            }

            yield break;
        }
    }
}
