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

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Mythic
{
    public sealed class RobeOfFireRatSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Shield;

            config.RelativeEffects = new List<string>() { "Firepower" };

            return config;
        }
    }

    [EntityLogic(typeof(RobeOfFireRatSeDef))]
    public sealed class RobeOfFireRatSe : EnemiesWithExhibitsStatusEffect
    {
        public RobeOfFireRatSe() : base()
        {
            Value1 = 20;
            Value2 = 3;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            this.NotifyActivating();

            React(new CastBlockShieldAction(unit, 0, Value1));
            React(new ApplyStatusEffectAction<Firepower>(unit, Value2, null, null, null, 0.2f));
        }
    }
}
