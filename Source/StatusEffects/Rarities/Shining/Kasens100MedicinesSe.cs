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
    public sealed class Kasens100MedicinesSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "TempFirepower" };

            return config;
        }
    }

    [EntityLogic(typeof(Kasens100MedicinesSeDef))]
    public sealed class Kasens100MedicinesSe : EnemiesWithExhibitsStatusEffect
    {
        public Kasens100MedicinesSe() : base()
        {
            Value1 = 2;
            Value2 = 3;

            Value3 = 3;
            Value4 = 3;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(UnitEventArgs args)
        {
            if (this.Owner.TurnCounter == Value1)
            {
                this.NotifyActivating();

                yield return new HealAction(this.Owner, this.Owner, Value2);
            }

            else if (this.Owner.TurnCounter == Value3)
            {
                this.NotifyActivating();

                yield return new ApplyStatusEffectAction<TempFirepower>(this.Owner, Value4, null, null, null, 0.2f);
            }
        }
    }
}
