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
    public sealed class NimbleFabricSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Graze" };

            return config;
        }
    }

    [EntityLogic(typeof(NimbleFabricSeDef))]
    public sealed class NimbleFabricSe : EnemiesWithExhibitsStatusEffect
    {
        public NimbleFabricSe() : base()
        {
            Value1 = 2;
            Value2 = 2;
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

                yield return new ApplyStatusEffectAction<Graze>(this.Owner, Value2, null, null, null, 0.2f);

                this.Highlight = false;
            }

            yield break;
        }
    }
}
