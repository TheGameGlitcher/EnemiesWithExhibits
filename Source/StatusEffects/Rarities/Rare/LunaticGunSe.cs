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
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Rare
{
    public sealed class LunaticGunSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            return config;
        }
    }

    [EntityLogic(typeof(LunaticGunSeDef))]
    public sealed class LunaticGunSe : EnemiesWithExhibitsStatusEffect
    {
        public LunaticGunSe() : base()
        {
            GunName = GunNameID.GetGunFromId(60004); // ExhYuekuang

            Value1 = 8;
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<CardEventArgs>(base.Battle.CardDiscarded, new EventSequencedReactor<CardEventArgs>(this.OnCardDiscarded));
        }

        private IEnumerable<BattleAction> OnCardDiscarded(CardEventArgs args)
        {
            this.NotifyActivating();

            yield return new DamageAction(base.Owner, base.Battle.Player, DamageInfo.Reaction((float)base.Value1, false), GunName, GunType.Single);

            yield break;
        }
    }
}
