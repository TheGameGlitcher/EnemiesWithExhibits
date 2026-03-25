using EnemiesWithExhibits.Source.GunName;
using EnemiesWithExhibits.Source.StatusEffects.Templates;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Uncommon
{
    public sealed class TNTSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(TNTSeDef))]
    public sealed class TNTSe : EnemiesWithExhibitsStatusEffect
    {
        public TNTSe() : base()
        {
            Value1 = 4;
            Value2 = 25;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(GameEventArgs args)
        {
            if (this.Owner.TurnCounter == Value1 - 1)
                this.Highlight = true;

            if (this.Owner.TurnCounter == Value1)
            {
                base.NotifyActivating();

                yield return new DamageAction(base.Owner, base.Battle.Player, DamageInfo.Reaction((float)base.Value2, false), GunNameID.GetGunFromId(60002), GunType.Single);

                base.Highlight = false;
            }

            yield break;
        }
    }
}
