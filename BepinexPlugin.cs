using BepInEx;
using BepInEx.Configuration;
using EnemiesWithExhibits.Source.Config;
using HarmonyLib;
using LBoL.Base;
using LBoL.EntityLib.PlayerUnits;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace EnemiesWithExhibits
{
    [BepInPlugin(PInfo.GUID, PInfo.Name, PInfo.version)]
    [BepInDependency(LBoLEntitySideloader.PluginInfo.GUID, BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency(AddWatermark.API.GUID, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("LBoL.exe")]
    public class BepinexPlugin : BaseUnityPlugin
    {
        //The Unique mod ID of the mod.
        //If defined, this is also the ID used by the Act 1 boss.
        //WARNING: It is mandatory to rename it to avoid issues.
        public static string modUniqueID = "EnemiesWithExhibitsMod";

        public static ConfigEntry<int> normalEnemyExhibits;
        public static CustomConfigEntry<int> normalEnemyExhibitsEntry = new CustomConfigEntry<int>(
            value: 1,
            section: "Jadebox Values",
            key: "NormalEnemyExhibits",
            description: "The number of exhibits given to basic enemies at the start of combat.");

        public static ConfigEntry<int> eliteEnemyExhibits;
        public static CustomConfigEntry<int> eliteEnemyExhibitsEntry = new CustomConfigEntry<int>(
            value: 2,
            section: "Jadebox Values",
            key: "EliteEnemyExhibits",
            description: "The number of exhibits given to elite enemies at the start of combat.");

        public static ConfigEntry<int> bossEnemyExhibits;
        public static CustomConfigEntry<int> bossEnemyExhibitsEntry = new CustomConfigEntry<int>(
            value: 3,
            section: "Jadebox Values",
            key: "BossEnemyExhibits",
            description: "The number of exhibits given to boss enemies at the start of combat.");

        public static ConfigEntry<bool> enableActScaling;
        public static CustomConfigEntry<bool> enableActScalingEntry = new CustomConfigEntry<bool>(
            value: false,
            section: "Act Scaling",
            key: "EnableActScaling",
            description: "If true, the number of exhibits given to enemies will scale based on the act number with the formula Exhibits + ActScalingValue * Act.");

        public static ConfigEntry<int> actScalingValue;
        public static CustomConfigEntry<int> actScalingValueEntry = new CustomConfigEntry<int>(
            value: 1,
            section: "Act Scaling",
            key: "ActScalingValue",
            description: "The number of extra exhibits to give each enemy per act. Requires EnableActScaling to be set to true.");

        public static ConfigEntry<bool> enableSectionScaling;
        public static CustomConfigEntry<bool> enableSectionScalingEntry = new CustomConfigEntry<bool>(
            value: false,
            section: "Section Scaling",
            key: "EnableSectionScaling",
            description: "If true, the number of exhibits given to enemies will scale based on the level segment with the formula Exhibits + SectionScalingValue * Level Section.");

        public static ConfigEntry<int> sectionScalingValue;
        public static CustomConfigEntry<int> sectionScalingValueEntry = new CustomConfigEntry<int>(
            value: 1,
            section: "Section Scaling",
            key: "SectionScalingValue",
            description: "The number of extra exhibits to give each enemy per act section. Requires EnableLevelScaling to be set to true.");

        private static readonly Harmony harmony = PInfo.harmony;

        internal static BepInEx.Logging.ManualLogSource log;

        internal static TemplateSequenceTable sequenceTable = new TemplateSequenceTable();

        internal static IResourceSource embeddedSource = new EmbeddedSource(Assembly.GetExecutingAssembly());

        // add this for audio loading
        internal static DirectorySource directorySource = new DirectorySource(PInfo.GUID, "");


        private void Awake()
        {
            log = Logger;

            // very important. Without this the entry point MonoBehaviour gets destroyed
            DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = HideFlags.HideAndDontSave;

            EntityManager.RegisterSelf();

            SetupConfig();

            harmony.PatchAll();
        }

        private void SetupConfig()
        {
            normalEnemyExhibits = Config.Bind<int>(normalEnemyExhibitsEntry.Section, normalEnemyExhibitsEntry.Key, normalEnemyExhibitsEntry.Value, normalEnemyExhibitsEntry.Description);

            eliteEnemyExhibits = Config.Bind<int>(eliteEnemyExhibitsEntry.Section, eliteEnemyExhibitsEntry.Key, eliteEnemyExhibitsEntry.Value, eliteEnemyExhibitsEntry.Description);

            bossEnemyExhibits = Config.Bind<int>(bossEnemyExhibitsEntry.Section, bossEnemyExhibitsEntry.Key, bossEnemyExhibitsEntry.Value, bossEnemyExhibitsEntry.Description);

            enableActScaling = Config.Bind<bool>(enableActScalingEntry.Section, enableActScalingEntry.Key, enableActScalingEntry.Value, enableActScalingEntry.Description);

            actScalingValue = Config.Bind<int>(actScalingValueEntry.Section, actScalingValueEntry.Key, actScalingValueEntry.Value, actScalingValueEntry.Description);

            enableSectionScaling = Config.Bind<bool>(enableSectionScalingEntry.Section, enableSectionScalingEntry.Key, enableSectionScalingEntry.Value, enableSectionScalingEntry.Description);

            sectionScalingValue = Config.Bind<int>(sectionScalingValueEntry.Section, sectionScalingValueEntry.Key, sectionScalingValueEntry.Value, sectionScalingValueEntry.Description);
        }

        private void OnDestroy()
        {
            if (harmony != null)
                harmony.UnpatchSelf();
        }
    }
}
