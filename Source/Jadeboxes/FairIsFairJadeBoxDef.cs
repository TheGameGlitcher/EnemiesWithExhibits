using EnemiesWithExhibits.Source.Jadeboxes.Templates;
using EnemiesWithExhibits.Source.Misc;
using EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining;
using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Battle;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.Randoms;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoLEntitySideloader.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using YamlDotNet.Core.Tokens;

namespace EnemiesWithExhibits.Source.Jadeboxes
{
    public sealed class FairIsFairJadeBoxDef : EnemiesWithExhibitsJadeBoxTemplate
    {
        public override JadeBoxConfig MakeConfig()
        {
            JadeBoxConfig config = DefaultConfig();

            config.Value1 = BepinexPlugin.normalEnemyExhibits.Value;

            config.Value2 = BepinexPlugin.eliteEnemyExhibits.Value;

            config.Value3 = BepinexPlugin.bossEnemyExhibits.Value;

            return config;
        }

        [EntityLogic(typeof(FairIsFairJadeBoxDef))]
        public sealed class FairIsFairJadeBox : EnemiesWithExhibitsJadeBox
        {
            public FairIsFairJadeBox() : base()
            {
                Value4 = BepinexPlugin.actScalingValue.Value;

                Value5 = BepinexPlugin.sectionScalingValue.Value;
            }

            public string ActScalingDescription => LocalizeProperty("ActScalingDescription", decorated: true, required: false);

            public string SectionScalingDescription => LocalizeProperty("SectionScalingDescription", decorated: true, required: false);

            public string DualScalingDescription => LocalizeProperty("DualScalingDescription", decorated: true, required: false);

            protected override string GetBaseDescription()
            {

                if (BepinexPlugin.enableActScaling.Value && BepinexPlugin.enableSectionScaling.Value)
                    return DualScalingDescription;

                if (BepinexPlugin.enableActScaling.Value)
                    return ActScalingDescription;

                if (BepinexPlugin.enableSectionScaling.Value)
                    return SectionScalingDescription;

                return base.GetBaseDescription();
            }

            protected override void OnEnterBattle()
            {
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStated));
            }

            private IEnumerable<BattleAction> OnBattleStated(GameEventArgs args)
            {
                int actNum = base.GameRun.CurrentStage.Level;

                int sectionNum = base.GameRun.CurrentStation.Act;

                switch (base.Battle.EnemyGroup.EnemyType)
                {
                    case EnemyType.Normal:
                        foreach (BattleAction action in ApplyExhibits(Value1 + (BepinexPlugin.enableActScaling.Value ? Value4 * actNum : 0) + (BepinexPlugin.enableSectionScaling.Value ? Value5 * sectionNum : 0)))
                            yield return action;
                        break;

                    case EnemyType.Elite:
                        foreach (BattleAction action in ApplyExhibits(Value2 + (BepinexPlugin.enableActScaling.Value ? Value4 * actNum : 0) + (BepinexPlugin.enableSectionScaling.Value ? Value5 * sectionNum : 0)))
                            yield return action;
                        break;

                    case EnemyType.Boss:
                        foreach (BattleAction action in ApplyExhibits(Value3 + (BepinexPlugin.enableActScaling.Value ? Value4 * actNum : 0) + (BepinexPlugin.enableSectionScaling.Value ? Value5 * sectionNum : 0)))
                            yield return action;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("The encounter type was neither Normal, Elite, or Boss.");
                }

                yield break;
            }

            private IEnumerable<BattleAction> ApplyExhibits(int times)
            {
                foreach (EnemyUnit enemy in base.Battle.AllAliveEnemies)
                {
                    for (int i = 0; i < times; i++)
                    {
                        Type exhibit = EnemiesWithExhibitsManager.GetEnemyExhibit(base.Battle.GameRun);

                        while (enemy.HasStatusEffect(exhibit))
                            exhibit = EnemiesWithExhibitsManager.GetEnemyExhibit(base.Battle.GameRun);

                        yield return new ApplyStatusEffectAction(exhibit, enemy, 1, null, null, null, 0.2f);
                    }
                }
            }
        }
    }
}
