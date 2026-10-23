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
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class LunarSoilSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Purify;

            return config;
        }
    }

    [EntityLogic(typeof(LunarSoilSeDef))]
    public sealed class LunarSoilSe : EnemiesWithExhibitsStatusEffect
    {
        public LunarSoilSe() : base()
        {
            Value1 = 1;
            Value2 = 2;

            GunName = GunNameID.GetGunFromId(4522); // Junko3C
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.PlayerTurnStarted));
        }

        private IEnumerable<BattleAction> PlayerTurnStarted(UnitEventArgs args)
        {
            if (base.Battle.Player.TurnCounter == Value1)
            {
                this.NotifyActivating();

                yield return PerformAction.Gun(base.Owner, base.Battle.Player, GunName);

                yield return ConvertManaAction.Purify(base.Battle.BattleMana, Value2);
            }
        }
    }
}
