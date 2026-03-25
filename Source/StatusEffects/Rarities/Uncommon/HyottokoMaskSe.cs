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
    public sealed class HyottokoMaskSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.HasCount = true;

            config.Keywords = Keyword.Block;

            return config;
        }
    }

    [EntityLogic(typeof(HyottokoMaskSeDef))]
    public sealed class HyottokoMaskSe : EnemiesWithExhibitsStatusEffect
    {
        public HyottokoMaskSe() : base()
        {
            Value1 = 3;
            Value2 = 5;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (base.Battle.Player.IsInTurn && args.Card.CardType == CardType.Attack)
            {
                Count++;

                if (Count == Value1 - 1)
                    this.Highlight = true;

                if (Count >= Value1)
                {
                    base.NotifyActivating();

                    yield return new CastBlockShieldAction(this.Owner, Value2, 0);

                    this.Highlight = false;

                    Count = 0;
                }
            }

            yield break;
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(UnitEventArgs args)
        {
            Count = 0;

            this.Highlight = false;

            yield break;
        }
    }
}
