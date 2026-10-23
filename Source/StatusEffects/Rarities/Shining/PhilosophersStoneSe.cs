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
using LBoL.EntityLib.StatusEffects.Neutral.MultiColor;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class PhilosophersStoneSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Philosophy;

            return config;
        }
    }

    [EntityLogic(typeof(PhilosophersStoneSeDef))]
    public sealed class PhilosophersStoneSe : EnemiesWithExhibitsStatusEffect
    {
        public PhilosophersStoneSe() : base()
        {
            Value1 = 1;

            Mana = new ManaGroup() { Philosophy = 1 };
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<ManaEventArgs>(base.Battle.ManaGaining, new GameEventHandler<ManaEventArgs>(this.OnManaGaining));
        }

        private void OnManaGaining(ManaEventArgs args)
        {
            if (args.Value.HasColor(ManaColor.Philosophy))
            {
                this.NotifyActivating();

                args.Value = new ManaGroup() { White = args.Value.White,
                                               Blue = args.Value.Blue, 
                                               Black = args.Value.Black,
                                               Red = args.Value.Red,
                                               Green = args.Value.Green,
                                               Colorless = args.Value.Colorless,
                                               Philosophy = Math.Max(args.Value.Philosophy - 1, 0),
                                               Any = args.Value.Any };

                args.AddModifier(this);
            }
        }
    }
}
