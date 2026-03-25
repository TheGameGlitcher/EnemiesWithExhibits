using EnemiesWithExhibits.Source.GunName;
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
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class TranquilizerWatchSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Weak", "Vulnerable" };

            return config;
        }
    }

    [EntityLogic(typeof(TranquilizerWatchSeDef))]
    public sealed class TranquilizerWatchSe : EnemiesWithExhibitsStatusEffect
    {
        public TranquilizerWatchSe() : base()
        {
            Value1 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            int result = base.Battle.GameRun.BattleRng.NextInt(1, 2);

            switch (result)
            {
                case 1:
                    React(new ApplyStatusEffectAction<Weak>(base.Battle.Player, null, Value1, null, null, 0.2f));
                    break;

                case 2:
                    React(new ApplyStatusEffectAction<Vulnerable>(base.Battle.Player, null, Value1, null, null, 0.2f));
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
