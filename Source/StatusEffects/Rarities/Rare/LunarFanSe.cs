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
    public sealed class LunarFanSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Block;

            return config;
        }
    }

    [EntityLogic(typeof(LunarFanSeDef))]
    public sealed class LunarFanSe : EnemiesWithExhibitsStatusEffect
    {

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.HandleOwnerEvent<BlockShieldEventArgs>(base.Owner.BlockShieldLosing, new GameEventHandler<BlockShieldEventArgs>(this.OnBlockShieldLosing));
        }

        private void OnBlockShieldLosing(BlockShieldEventArgs args)
        {
            if (args.Cause == ActionCause.TurnStart && args.Block > 0f)
            {
                args.Block = (float)(args.Block / 2f).FloorToInt();

                args.AddModifier(this);

                NotifyActivating();
            }
        }
    }
}
