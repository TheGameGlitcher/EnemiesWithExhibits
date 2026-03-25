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
    public sealed class LunarVeilSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Invincible" };

            return config;
        }
    }

    [EntityLogic(typeof(LunarVeilSeDef))]
    public sealed class LunarVeilSe : EnemiesWithExhibitsStatusEffect
    {
        public LunarVeilSe() : base()
        {
            Value1 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            React(new ApplyStatusEffectAction<Invincible>(this.Owner, null, Value1, null, null, 0.2f));
        }
    }
}
