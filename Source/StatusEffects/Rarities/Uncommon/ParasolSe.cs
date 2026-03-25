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

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Uncommon
{
    public sealed class ParasolSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "TempFirepower" };

            return config;
        }
    }

    [EntityLogic(typeof(ParasolSeDef))]
    public sealed class ParasolSe : EnemiesWithExhibitsStatusEffect
    {
        public ParasolSe() : base()
        {
            Value1 = 2;
            Value2 = 12;
            Value3 = 5;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(GameEventArgs args)
        {
            if (this.Owner.TurnCounter == Value1)
            {
                base.NotifyActivating();

                yield return new CastBlockShieldAction(this.Owner, Value2, 0);
                yield return new ApplyStatusEffectAction<TempFirepower>(this.Owner, Value3, null, null, null, 0.2f);
            }

            yield break;
        }
    }
}
