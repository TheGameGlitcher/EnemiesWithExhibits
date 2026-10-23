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
    public sealed class UltramarineOrbElixirSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Firepower" };

            return config;
        }
    }

    [EntityLogic(typeof(UltramarineOrbElixirSeDef))]
    public sealed class UltramarineOrbElixirSe : EnemiesWithExhibitsStatusEffect
    {
        public UltramarineOrbElixirSe() : base()
        {
            Counter = 1;

            Value1 = 2;
            Value2 = 50;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<DieEventArgs>(this.Owner.Dying, new GameEventHandler<DieEventArgs>(this.OnOwnerDying));

            this.Highlight = true;
        }

        private void OnOwnerDying(DieEventArgs args)
        {
            if (Counter > 0)
            {
                base.NotifyActivating();

                int health = ((double)(args.Unit.MaxHp * base.Value2) / 100.0).RoundToInt();

                base.Battle.GameRun.SetEnemyHpAndMaxHp(health, health, (EnemyUnit)this.Owner);

                args.CancelBy(this);

                React(new ApplyStatusEffectAction<Firepower>(this.Owner, Value1, null, null, null, 0.2f));
            }

            Counter--;

            this.Highlight = false;
        }
    }
}
