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

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class KaguyasTreasureBoxSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(KaguyasTreasureBoxSeDef))]
    public sealed class KaguyasTreasureBoxSe : EnemiesWithExhibitsStatusEffect
    {
        public KaguyasTreasureBoxSe() : base()
        {
            Value1 = 3;
            Value2 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Battle.Player.TurnStarting, new EventSequencedReactor<UnitEventArgs>(this.PlayerTurnStarting));

            for (int i = 0; i < Value1; i++)
            {
                Type exhibit = EnemiesWithExhibitsManager.GetEnemyExhibit(base.Battle.GameRun);

                while (this.Owner.HasStatusEffect(exhibit))
                    exhibit = EnemiesWithExhibitsManager.GetEnemyExhibit(base.Battle.GameRun);

                React(new ApplyStatusEffectAction(exhibit, this.Owner, 1, null, null, null, 0.2f));
            }
        }

        private IEnumerable<BattleAction> PlayerTurnStarting(UnitEventArgs args)
        {
            if (base.Battle.Player.TurnCounter == Value2)
            {
                EnemyUnit owner = (EnemyUnit)this.Owner;

                this.NotifyActivating();

                owner.ClearIntentions();
                owner._turnMoves.Clear();

                owner._turnMoves.Add(new SimpleEnemyMove(Intention.Stun()));

                owner.NotifyIntentionsChanged();
            }

            yield break;
        }
    }
}
