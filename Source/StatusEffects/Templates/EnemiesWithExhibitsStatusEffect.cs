using EnemiesWithExhibits.Source.GunName;
using LBoL.Base;
using LBoL.Core.StatusEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnemiesWithExhibits.Source.StatusEffects.Templates
{
    public class EnemiesWithExhibitsStatusEffect : StatusEffect
    {
        public string GunName { get; protected set; } = GunNameID.GetGunFromId(900); // Simple1
        public string BurstGunName { get; protected set; } = GunNameID.GetGunFromId(901); // Simple2
        public ManaGroup Mana { get; protected set; } = ManaGroup.Empty;
        public bool HasTriggered { get; protected set; } = false;
        public int Counter { get; protected set; } = 0;
        public int Value1 { get; protected set; } = 0;
        public int Value2 { get; protected set; } = 0;
        public int Value3 { get; protected set; } = 0;
        public int Value4 { get; protected set; } = 0;
        public int Value5 { get; protected set; } = 0;
        public int Value6 { get; protected set; } = 0;
    }
}
