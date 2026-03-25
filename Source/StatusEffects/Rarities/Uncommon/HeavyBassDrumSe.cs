using EnemiesWithExhibits.Source.GunName;
using EnemiesWithExhibits.Source.StatusEffects.Templates;
using JetBrains.Annotations;
using LBoL.Base;
using LBoL.Base.Extensions;
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
    public sealed class HeavyBassDrumSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "TempFirepower" };

            return config;
        }
    }

    [EntityLogic(typeof(HeavyBassDrumSeDef))]
    public sealed class HeavyBassDrumSe : EnemiesWithExhibitsStatusEffect
    {
        public HeavyBassDrumSe() : base()
        {
            Value1 = 1;
        }

        [UsedImplicitly]
        public ManaGroup LastColor { get; set; }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            IReadOnlyList<ManaColor> colors = args.Card.Config.Colors;

            ManaGroup newColor = colors.Empty<ManaColor>() ? ManaGroup.Single(ManaColor.Colorless) : ManaGroup.FromComponents(colors);

            if (!ManaGroup.Intersect(LastColor, newColor).IsEmpty)
            {
                base.NotifyActivating();

                yield return new ApplyStatusEffectAction<TempFirepower>(this.Owner, Value1, null, null, null, 0.2f);
            }

            LastColor = newColor;

            yield break;
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(UnitEventArgs args)
        {
            LastColor = ManaGroup.Empty;

            yield break;
        }
    }
}
