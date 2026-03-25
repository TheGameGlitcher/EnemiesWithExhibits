using EnemiesWithExhibits.Source.GunName;
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
    public sealed class TacticalConductorSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(TacticalConductorSeDef))]
    public sealed class TacticalConductorSe : EnemiesWithExhibitsStatusEffect
    {
        public TacticalConductorSe() : base()
        {
            Mana = new ManaGroup() { Any = 1 };
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            this.SetMana(base.Battle.EnumerateAllCards());
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToDiscard, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToHand, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsEventArgs>(base.Battle.CardsAddedToExile, new GameEventHandler<CardsEventArgs>(this.OnAddCard));
            base.HandleOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddedToDrawZone, new GameEventHandler<CardsAddingToDrawZoneEventArgs>(this.OnAddCardToDraw));
            base.HandleOwnerEvent<CardTransformEventArgs>(base.Battle.CardTransformed, new GameEventHandler<CardTransformEventArgs>(this.OnCardTransformed));
        }

        protected override void OnRemoved(Unit unit)
        {
            foreach (Card card in base.Battle.EnumerateAllCards())
            {
                if (card.CardType == CardType.Skill)
                    card.AuraCost -= Mana;
            }
        }

        private void OnAddCard(CardsEventArgs args)
        {
            this.SetMana(args.Cards);
        }

        private void OnAddCardToDraw(CardsAddingToDrawZoneEventArgs args)
        {
            this.SetMana(args.Cards);
        }

        private void OnCardTransformed(CardTransformEventArgs args)
        {
            this.SetMana(args.DestinationCard);
        }

        private void SetMana(Card card)
        {
            if (card.CardType == CardType.Skill)
            {
                base.NotifyActivating();
                card.AuraCost += base.Mana;
            }
        }

        private void SetMana(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (card.CardType == CardType.Skill)
                {
                    base.NotifyActivating();

                    card.AuraCost += Mana;
                }
            }
        }
    }
}
