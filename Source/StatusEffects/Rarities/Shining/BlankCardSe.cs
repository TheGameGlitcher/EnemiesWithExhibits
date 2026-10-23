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
using LBoL.EntityLib.Cards.Enemy;
using LBoL.EntityLib.Cards.Neutral.Black;
using LBoL.Presentation.UI.ExtraWidgets;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class BlankCardSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(BlankCardSeDef))]
    public sealed class BlankCardSe : EnemiesWithExhibitsStatusEffect
    {
        private static readonly Type[] _statusCards =
        {
            typeof(AyaNews),
            typeof(HatateNews),
            typeof(AyaNewsReimuSp),
            typeof(AyaNewsMarisaSp),
            typeof(AyaNewsSakuyaSp),
            typeof(AyaNewsCirnoSp),
            typeof(AyaNewsKoishiSp),
            typeof(AyaNewsAliceSp),
            typeof(Riguang),
            typeof(Yueguang),
            typeof(Xingguang),
            typeof(Xuanguang),
            typeof(CirnoFreeze),
            typeof(KoishiDiscard),
            typeof(BlackResidue),
            typeof(Wushuai),
            typeof(Lunatic),
            typeof(Chunguang),
            typeof(Nightmare),
            typeof(NightMana1),
            typeof(NightMana2),
            typeof(NightMana3),
            typeof(NightMana4),
            typeof(Shadow)
        };

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            this.NotifyActivating();

            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddingToHand, new EventSequencedReactor<CardsEventArgs>(this.OnCardAddingToHand));
            base.ReactOwnerEvent<CardsAddingToDrawZoneEventArgs>(base.Battle.CardsAddingToDrawZone, new EventSequencedReactor<CardsAddingToDrawZoneEventArgs>(this.OnCardAddingToDrawZone));
            base.ReactOwnerEvent<CardsEventArgs>(base.Battle.CardsAddingToDiscard, new EventSequencedReactor<CardsEventArgs>(this.OnCardAddingToDiscard));
        }

        private IEnumerable<BattleAction> OnCardAddingToHand(CardsEventArgs args)
        {
            if (args.ActionSource == this.Owner)
            {
                this.NotifyActivating();

                for (int i = 0; i < args.Cards.Length; i++)
                    args.Cards[i] = Library.CreateCard(_statusCards[base.Battle.GameRun.BattleRng.NextInt(0, _statusCards.Length - 1)], false);
            }

            yield break;
        }

        private IEnumerable<BattleAction> OnCardAddingToDrawZone(CardsAddingToDrawZoneEventArgs args)
        {
            if (args.ActionSource == this.Owner)
            {
                this.NotifyActivating();

                for (int i = 0; i < args.Cards.Length; i++)
                    args.Cards[i] = Library.CreateCard(_statusCards[base.Battle.GameRun.BattleRng.NextInt(0, _statusCards.Length - 1)], false);
            }

            yield break;
        }

        private IEnumerable<BattleAction> OnCardAddingToDiscard(CardsEventArgs args)
        {
            if (args.ActionSource == this.Owner)
            {
                this.NotifyActivating();

                for (int i = 0; i < args.Cards.Length; i++)
                    args.Cards[i] = Library.CreateCard(_statusCards[base.Battle.GameRun.BattleRng.NextInt(0, _statusCards.Length - 1)], false);
            }

            yield break;
        }
    }
}
