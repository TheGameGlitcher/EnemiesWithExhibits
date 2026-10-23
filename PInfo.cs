using HarmonyLib;

namespace EnemiesWithExhibits
{
    public static class PInfo
    {
        //Rename the variable below to prevent conflicts between mod.
        public const string GUID = "thegameglitcher.lbol.jadebox.EnemiesWithExhibits";
        public const string Name = "EnemiesWithExhibitsMod";
        public const string version = "1.0.2";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}
