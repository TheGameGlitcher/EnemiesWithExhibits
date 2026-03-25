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
using LBoL.EntityLib.Mixins;
using LBoL.EntityLib.StatusEffects.Basic;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class RoukankenSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.RelativeEffects = new List<string>() { "LockedOn" };

            return config;
        }
    }

    [EntityLogic(typeof(RoukankenSeDef))]
    public sealed class RoukankenSe : EnemiesWithExhibitsStatusEffect, INotifyActivating
    {
        public RoukankenSe() : base()
        {
            Value1 = 1;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<StatisticalDamageEventArgs>(base.Owner.StatisticalTotalDamageDealt, new EventSequencedReactor<StatisticalDamageEventArgs>(this.OnStatisticalDamageDealt));
        }

        IEnumerable<BattleAction> OnStatisticalDamageDealt(StatisticalDamageEventArgs args)
        {
            if (this.Battle.BattleShouldEnd)
                yield break;

            foreach (KeyValuePair<Unit, IReadOnlyList<DamageEventArgs>> keyValuePair in args.ArgsTable)
            {
                Unit unit;
                IReadOnlyList<DamageEventArgs> readOnlyList;
                keyValuePair.Deconstruct(out unit, out readOnlyList);
                Unit unit2 = unit;
                IReadOnlyList<DamageEventArgs> source = readOnlyList;

                if (unit2.IsAlive)
                {
                    int num = source.Count(delegate (DamageEventArgs damageAgs)
                    {
                        DamageInfo damageInfo = damageAgs.DamageInfo;
                        return damageInfo.DamageType == DamageType.Attack && damageInfo.Amount > 0f;
                    });
                    if (num > 0)
                    {
                        this.NotifyActivating();
                        EnemyUnit enemyUnit = this.Owner as EnemyUnit;
                        if (enemyUnit != null && enemyUnit != this.Battle.LastAliveEnemy)
                        {
                            yield return new ApplyStatusEffectAction<EnemyLockedOn>(enemyUnit, new int?(this.Value1 * num), null, null, null, 0f, true);
                        }
                        else
                        {
                            yield return new ApplyStatusEffectAction<LockedOn>(unit2, new int?(this.Value1 * num), null, null, null, 0f, true);
                        }

                        this.OnTriggered();
                    }
                }
            }

            IEnumerator<KeyValuePair<Unit, IReadOnlyList<DamageEventArgs>>> enumerator = null;
        }

        void OnTriggered()
        {
            int triggerCount = Counter + 1;
            Counter = triggerCount;
        }
    }
}
