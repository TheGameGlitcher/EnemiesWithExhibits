using EnemiesWithExhibits.Source.StatusEffects.Rarities.Common;
using EnemiesWithExhibits.Source.StatusEffects.Rarities.Mythic;
using EnemiesWithExhibits.Source.StatusEffects.Rarities.Rare;
using EnemiesWithExhibits.Source.StatusEffects.Rarities.Shining;
using EnemiesWithExhibits.Source.StatusEffects.Rarities.Uncommon;
using LBoL.Base;
using LBoL.Core;
using LBoL.Core.StatusEffects;
using LBoL.Core.Units;
using LBoL.EntityLib.Exhibits.Common;
using LBoL.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EnemiesWithExhibits.Source.Misc
{
    public static class EnemiesWithExhibitsManager
    {
        private static readonly Type[] _shiningExhibits =
        {
            typeof(BloodthirstyYinYangOrbSe),
            typeof(HakureiPurificationRodSe),
            typeof(MiniHakkeroSe),
            typeof(LaevateinnSe),
            typeof(GungnirSe),
            typeof(OniSakeDishSe),
            typeof(StoneOfAjaSe),
            typeof(Kasens100MedicinesSe),
            typeof(HarvestFruitBasketSe),
            typeof(LunarSoilSe),
            typeof(BlankCardSe),
            typeof(KaguyasTreasureBoxSe)
        };

        private static readonly Type[] _commonExhibits =
        {
            typeof(TengusPompomsSe),
            typeof(TengusClogSe),
            typeof(TengusFanSe),
            typeof(TengusCameraSe),
            typeof(GrapeSe),
            typeof(KarakasaSe),
            typeof(CrowTengusWingSe),
            typeof(SecretCloakSe),
            typeof(PeonySe),
            typeof(IceCubeSe),
            typeof(PeonyStoneSe),
            typeof(LeafSe),
            typeof(HakureiAmuletSe),
            typeof(DeliciousCookieSe),
            typeof(TranquilizerWatchSe),
            typeof(TakeCopterSe),
            typeof(BeerSe),
            typeof(LightsaberSe),
            typeof(LighterSe)
        };

        private static readonly Type[] _uncommonExhibits =
        {
            typeof(TNTSe),
            typeof(NimbleFabricSe),
            typeof(ParasolSe),
            typeof(AppleSe),
            typeof(MaskOfHopeSe),
            typeof(HyottokoMaskSe),
            typeof(FoxMaskSe),
            typeof(HannyaMaskSe),
            typeof(HeavyBassDrumSe)
        };

        private static readonly Type[] _rareExhibits =
        {
            typeof(JeweledPagodaSe),
            typeof(ParasolOfGapsSe),
            typeof(PeachSe),
            typeof(SpacesuitSe),
            typeof(UltramarineOrbElixirSe),
            typeof(SoulTorchSe),
            typeof(BrokenAmuletSe),
            typeof(GensokyoChronicleSe),
            typeof(TacticalConductorSe),
            typeof(LunarFanSe),
            typeof(LunarVeilSe),
            typeof(SwordOfHisouSe),
            typeof(RoukankenSe),
            typeof(TelevisionSe),
            typeof(WashingMachineSe)
        };

        private static readonly Type[] _mythicExhibits =
        {
            typeof(SwallowsCowrieShellSe),
            typeof(RobeOfFireRatSe),
            typeof(BuddhasStoneBowlSe)
        };

        public static readonly Type[] EnemyExhibits = _shiningExhibits.Concat(_commonExhibits).Concat(_uncommonExhibits).Concat(_rareExhibits).Concat(_mythicExhibits).ToArray();

        public static Type GetEnemyExhibit(GameRunController gameRun)
        {
            int exhibit;

            exhibit = gameRun.BattleRng.NextInt(0, EnemyExhibits.Length - 1);

            return EnemyExhibits[exhibit];
        }
    }
}
