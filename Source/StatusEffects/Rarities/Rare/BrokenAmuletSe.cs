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
    public sealed class BrokenAmuletSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Firepower", "Spirit" };

            return config;
        }
    }

    [EntityLogic(typeof(BrokenAmuletSeDef))]
    public sealed class BrokenAmuletSe : EnemiesWithExhibitsStatusEffect
    {
        public BrokenAmuletSe() : base()
        {
            Value1 = 1;
            Value2 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            int misfortunes = base.GameRun.BaseDeck.Count((Card card) => card.CardType == CardType.Misfortune);

            if (misfortunes > 0)
            {
                React(new ApplyStatusEffectAction<Firepower>(this.Owner, Value1 * misfortunes, null, null, null, 0.2f));
                React(new ApplyStatusEffectAction<Spirit>(this.Owner, Value2 * misfortunes, null, null, null, 0.2f));
            }
        }
    }
}
