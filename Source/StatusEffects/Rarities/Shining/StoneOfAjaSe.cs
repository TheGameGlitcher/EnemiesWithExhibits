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
    public sealed class StoneOfAjaSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "Firepower" };

            return config;
        }
    }

    [EntityLogic(typeof(StoneOfAjaSeDef))]
    public sealed class StoneOfAjaSe : EnemiesWithExhibitsStatusEffect
    {
        public StoneOfAjaSe() : base()
        {
            GunName = GunNameID.GetGunFromId(70040); // ESunnyCard

            Value1 = 2;

            Value2 = 2;
            Value3 = 4;

            Value4 = 5;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<UnitEventArgs>(base.Owner.TurnEnded, new EventSequencedReactor<UnitEventArgs>(this.OnOwnerTurnEnded));

            NotifyActivating();

            React(new ApplyStatusEffectAction<Firepower>(this.Owner, Value1, null, null, null, 0.2f));
        }

        private IEnumerable<BattleAction> OnOwnerTurnEnded(UnitEventArgs args)
        {
            if (base.Owner.TurnCounter == Value2 || base.Owner.TurnCounter == Value3)
            {
                NotifyActivating();

                yield return new DamageAction(this.Owner, this.Owner, new DamageInfo(Value4, DamageType.Reaction, false, true), GunName);
            }

            yield break;
        }
    }
}
