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
using LBoL.EntityLib.StatusEffects.Enemy;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.ExtraFunc.GunHelpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining
{
    public sealed class OldLunarSakeSeDef : EnemiesWithExhibitsStatusEffectTemplate
    {
        public override StatusEffectConfig MakeConfig()
        {
            StatusEffectConfig config = GetDefaultStatusEffectConfig();

            config.Keywords = Keyword.Exile | Keyword.Purify;

            return config;
        }
    }

    [EntityLogic(typeof(OldLunarSakeSeDef))]
    public sealed class OldLunarSakeSe : EnemiesWithExhibitsStatusEffect
    {
        public OldLunarSakeSe() : base()
        {
            Value1 = 1;

            GunName = GunNameID.GetGunFromId(4522); // Junko3C
        }

        protected override void OnAdded(Unit unit)
        {
            if (unit is PlayerUnit)
                Debug.LogError(this.DebugName + " should not apply to player unit.");

            base.ReactOwnerEvent<CardUsingEventArgs>(base.Battle.CardUsed, new EventSequencedReactor<CardUsingEventArgs>(this.OnCardUsed));
        }

        private IEnumerable<BattleAction> OnCardUsed(CardUsingEventArgs args)
        {
            if (base.Battle.Player.IsInTurn && args.Card.IsExile)
            {
                base.NotifyActivating();

                yield return PerformAction.Gun(base.Owner, base.Battle.Player, GunName);

                yield return ConvertManaAction.Purify(base.Battle.BattleMana, Value1);
            }

            yield break;
        }
    }
}
