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
    public sealed class MiniHakkeroSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "ChargingEnemySe" };

            return config;
        }
    }

    [EntityLogic(typeof(MiniHakkeroSeDef))]
    public sealed class MiniHakkeroSe : EnemiesWithExhibitsStatusEffect
    {
        public MiniHakkeroSe() : base()
        {
            Value1 = 1;

            Mana = ManaGroup.Single(ManaColor.Red);
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new GameEventHandler<CardUsingEventArgs>(this.OnCardUsed));
        }

        private void OnCardUsed(CardUsingEventArgs args)
        {
            if (args.Card.CardType == CardType.Attack && (args.ConsumingMana.Red > 0 || args.ConsumingMana.Philosophy > 0))
            {
                base.NotifyActivating();

                React(new ApplyStatusEffectAction<ChargingEnemySe>(base.Owner, base.Value1, null, null, null, 0f, true));
            }
        }
    }
}
