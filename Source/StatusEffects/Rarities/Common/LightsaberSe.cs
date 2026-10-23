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
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Common
{
    public sealed class LightsaberSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "LockedOn" };

            return config;
        }
    }

    [EntityLogic(typeof(LightsaberSeDef))]
    public sealed class LightsaberSe : EnemiesWithExhibitsStatusEffect
    {
        public LightsaberSe() : base()
        {
            Counter = 1;

            Value1 = 3;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<StatisticalDamageEventArgs>(this.Owner.StatisticalTotalDamageDealt, new EventSequencedReactor<StatisticalDamageEventArgs>(this.OnStatisticalDamageDealt));
        }

        private IEnumerable<BattleAction> OnStatisticalDamageDealt(StatisticalDamageEventArgs args)
        {
            if (Counter > 0 && args.DamageSource == this.Owner)
            {
                this.NotifyActivating();

                yield return new ApplyStatusEffectAction<EnemyLockedOn>(this.Owner, Value1, null, null, null, 0.2f);

                Counter--;
            }

            yield break;
        }
    }
}
