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

        //Name of the character.
        //This is also the prefix that is used before every .png file in DirResources. 
        public static string playerName = "Null";

        //Whether to us an ingame or custom model.
        //InGame: Will load the character model of the ingame character.
        //Custom: Will load DirResource/EnemiesWithExhibitsModel.png 
        public static bool useInGameModel = true;

        //If InGame is selected, this is the model that will be loaded. 
        //Check LBoL.EntityLib.EnemyUnits.Character or using LBoL.EntityLib.PlayerUnits for a list of all the characters available. 
        public static string modelName = nameof(Reimu);

        //Some in-game model needs to be flipped (most notably elites).
        public static bool modelIsFlipped = false;

        //The character's off-color.
        //Used to separate cards in the card collection and put the off-color cards at the end.
        public static List<ManaColor> offColors = new List<ManaColor>() { ManaColor.Colorless };

        //Whether the Act 1 boss should be enabled.
        //The value can be customized LBoL/BepInEx/config/
        public static ConfigEntry<bool> enableAct1Boss;

        public static CustomConfigEntry<bool> enableAct1BossEntry = new CustomConfigEntry<bool>(
            value: false,
            section: "EnableAct1Boss",
            key: "EnableAct1Boss",
            description: "Toggle the Act 1 boss. Default: Off");

        private static readonly Harmony harmony = PInfo.harmony;

        internal static BepInEx.Logging.ManualLogSource log;

        internal static TemplateSequenceTable sequenceTable = new TemplateSequenceTable();

        internal static IResourceSource embeddedSource = new EmbeddedSource(Assembly.GetExecutingAssembly());

        // add this for audio loading
        internal static DirectorySource directorySource = new DirectorySource(PInfo.GUID, "");


        private void Awake()
        {
            log = Logger;
            ///Load the custom config entry.
            enableAct1Boss = Config.Bind(enableAct1BossEntry.Section, enableAct1BossEntry.Key, enableAct1BossEntry.Value, enableAct1BossEntry.Description);

            // very important. Without this the entry point MonoBehaviour gets destroyed
            DontDestroyOnLoad(gameObject);
            gameObject.hideFlags = HideFlags.HideAndDontSave;

            EntityManager.RegisterSelf();

            harmony.PatchAll();

            //Watermark is now unused as of 1.8.0.
            //The code is temporarily kept if the devs change their minds.
            /*if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(AddWatermark.API.GUID))
                WatermarkWrapper.ActivateWatermark();*/
        }

        private void OnDestroy()
        {
            if (harmony != null)
                harmony.UnpatchSelf();
        }
    }
}
