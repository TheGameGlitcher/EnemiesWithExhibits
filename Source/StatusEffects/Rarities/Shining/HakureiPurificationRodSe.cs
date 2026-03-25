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
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class HakureiPurificationRodSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Shield;

            return config;
        }
    }

    [EntityLogic(typeof(HakureiPurificationRodSeDef))]
    public sealed class HakureiPurificationRodSe : EnemiesWithExhibitsStatusEffect
    {
        public HakureiPurificationRodSe() : base()
        {
            Value1 = 3;
            Value2 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnEnding, new EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnEnding));
            React(new CastBlockShieldAction(this.Owner, 0, Value2));
        }

        private IEnumerable<BattleAction> OnPlayerTurnEnding(UnitEventArgs args)
        {
            if (base.Battle.BattleMana != ManaGroup.Empty)
            {
                this.NotifyActivating();

                yield return new CastBlockShieldAction(this.Owner, 0, Value1);
            }

            yield break;
        }
    }
}
