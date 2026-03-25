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
using System.Text;
using UnityEngine.EventSystems;
using YamlDotNet.Core.Tokens;

namespace EnemiesWithExhibits.Source.Jadeboxes
{
    public sealed class FairIsFairJadeBoxDef : EnemiesWithExhibitsJadeBoxTemplate
    {
        public override JadeBoxConfig MakeConfig()
        {
            JadeBoxConfig config = DefaultConfig();

            config.Value1 = 1;

            config.Value2 = 2;

            config.Value3 = 3;

            return config;
        }

        [EntityLogic(typeof(FairIsFairJadeBoxDef))]
        public sealed class FairIsFairJadeBox : EnemiesWithExhibitsJadeBox
        {
            public FairIsFairJadeBox() : base()
            {
                Value4 = 1;
            }

            protected override void OnEnterBattle()
            {
                ReactBattleEvent(Battle.BattleStarted, new EventSequencedReactor<GameEventArgs>(OnBattleStated));
            }

            private IEnumerable<BattleAction> OnBattleStated(GameEventArgs args)
            {
                switch (base.Battle.EnemyGroup.EnemyType)
                {
                    case EnemyType.Normal:
                        foreach (BattleAction action in ApplyExhibits(Value1))
                            yield return action;
                        break;

                    case EnemyType.Elite:
                        foreach (BattleAction action in ApplyExhibits(Value2))
                            yield return action;
                        break;

                    case EnemyType.Boss:
                        foreach (BattleAction action in ApplyExhibits(Value3))
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
