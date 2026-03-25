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
    public sealed class TakeCopterSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.HasCount = true;

            config.RelativeEffects = new List<string>() { "Graze" };

            return config;
        }
    }

    [EntityLogic(typeof(TakeCopterSeDef))]
    public sealed class TakeCopterSe : EnemiesWithExhibitsStatusEffect
    {
        public TakeCopterSe() : base()
        {
            Value1 = 1;
            Value2 = 3;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(this.Owner.TurnStarted, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnStarted));
        }

        private IEnumerable<BattleAction> OnOwnerTurnStarted(UnitEventArgs args)
        {
            if (Count == Value2 - 1)
            {
                yield return new ApplyStatusEffectAction<Graze>(this.Owner, Value1, null, null, null, 0.2f);

                Count = 0;
            }

            else
                Count++;

            yield break;
        }
    }
}
