using EnemiesWithExhibits.Source.StatusEffects.Templates;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Common
{
    public sealed class TengusCameraSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "LockedOn" };

            return config;
        }
    }

    [EntityLogic(typeof(TengusCameraSeDef))]
    public sealed class TengusCameraSe : EnemiesWithExhibitsStatusEffect
    {
        public TengusCameraSe() : base()
        {
            Value1 = 2;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            this.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarting, new LBoL.Core.Battle.EventSequencedReactor<UnitEventArgs>(this.OnPlayerTurnStarting));
        }

        private IEnumerable<BattleAction> OnPlayerTurnStarting(UnitEventArgs args)
        {
            if (base.Battle.Player.TurnCounter == 1)
            {
                this.NotifyActivating();

                yield return new ApplyStatusEffectAction<LockedOn>(base.Battle.Player, Value1, null, null, null, 0.2f);
            }
        }
    }
}
