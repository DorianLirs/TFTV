﻿using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Statuses;
using Base.UI;
using HarmonyLib;
using PhoenixPoint.Common.ContextHelp;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Statuses;
using PhoenixPoint.Tactical.Levels;
using PhoenixPoint.Tactical.View.ViewControllers;
using PhoenixPoint.Tactical.View.ViewModules;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PhoenixPoint.Tactical.View.ViewModules.UIModuleCharacterStatus;
using static PhoenixPoint.Tactical.View.ViewModules.UIModuleCharacterStatus.CharacterData;

namespace TFTV
{
    internal class TFTVHumanEnemies
    {
        private static readonly DefRepository Repo = TFTVMain.Repo;
        private static readonly DefCache DefCache = TFTVMain.Main.DefCache;
        private static readonly GameTagDef HumanEnemyTier1GameTag = DefCache.GetDef<GameTagDef>("HumanEnemyTier_1_GameTagDef");
        private static readonly GameTagDef HumanEnemyTier2GameTag = DefCache.GetDef<GameTagDef>("HumanEnemyTier_2_GameTagDef");
        private static readonly GameTagDef HumanEnemyTier3GameTag = DefCache.GetDef<GameTagDef>("HumanEnemyTier_3_GameTagDef");
        private static readonly GameTagDef HumanEnemyTier4GameTag = DefCache.GetDef<GameTagDef>("HumanEnemyTier_4_GameTagDef");
        private static readonly GameTagDef humanEnemyTagDef = DefCache.GetDef<GameTagDef>("HumanEnemy_GameTagDef");

        private static readonly GameTagDef heavy = DefCache.GetDef<GameTagDef>("Heavy_ClassTagDef");

        private static readonly AbilityDef regeneration = DefCache.GetDef<AbilityDef>("Regeneration_Torso_Passive_AbilityDef");
        private static readonly HealthChangeStatusDef regenerationStatus = DefCache.GetDef<HealthChangeStatusDef>("Regeneration_Torso_Constant_StatusDef");
        private static readonly AddAttackBoostStatusDef quickAimStatus = DefCache.GetDef<AddAttackBoostStatusDef>("E_Status [QuickAim_AbilityDef]");
        private static readonly PassiveModifierAbilityDef ambush = DefCache.GetDef<PassiveModifierAbilityDef>("HumanEnemiesTacticsAmbush_AbilityDef");
        private static readonly StatusDef frenzy = DefCache.GetDef<StatusDef>("Frenzy_StatusDef");
        private static readonly HitPenaltyStatusDef mFDStatus = DefCache.GetDef<HitPenaltyStatusDef>("E_PureDamageBonusStatus [MarkedForDeath_AbilityDef]");

        //Assault abilites
        private static readonly AbilityDef killAndRun = DefCache.GetDef<AbilityDef>("KillAndRun_AbilityDef");
        private static readonly AbilityDef quickAim = DefCache.GetDef<AbilityDef>("BC_QuickAim_AbilityDef");
        private static readonly AbilityDef onslaught = DefCache.GetDef<AbilityDef>("DeterminedAdvance_AbilityDef");
        private static readonly AbilityDef readyForAction = DefCache.GetDef<AbilityDef>("ReadyForAction_AbilityDef");
        private static readonly AbilityDef rapidClearance = DefCache.GetDef<AbilityDef>("RapidClearance_AbilityDef");

        //Berseker abilities
        private static readonly AbilityDef dash = DefCache.GetDef<AbilityDef>("Dash_AbilityDef");
        private static readonly AbilityDef cqc = DefCache.GetDef<AbilityDef>("CloseQuarters_AbilityDef");
        private static readonly AbilityDef bloodlust = DefCache.GetDef<AbilityDef>("BloodLust_AbilityDef");
        private static readonly AbilityDef ignorePain = DefCache.GetDef<AbilityDef>("IgnorePain_AbilityDef");
        private static readonly AbilityDef adrenalineRush = DefCache.GetDef<AbilityDef>("AdrenalineRush_AbilityDef");

        //Heavy
        private static readonly AbilityDef returnFire = DefCache.GetDef<AbilityDef>("ReturnFire_AbilityDef");
        private static readonly AbilityDef hunkerDown = DefCache.GetDef<AbilityDef>("HunkerDown_AbilityDef");
        private static readonly AbilityDef skirmisher = DefCache.GetDef<AbilityDef>("Skirmisher_AbilityDef");
        private static readonly AbilityDef shredResistant = DefCache.GetDef<AbilityDef>("ShredResistant_DamageMultiplierAbilityDef");
        private static readonly AbilityDef rageBurst = DefCache.GetDef<AbilityDef>("RageBurst_RageBurstInConeAbilityDef");

        //infiltrator
        private static readonly AbilityDef surpriseAttack = DefCache.GetDef<AbilityDef>("SurpriseAttack_AbilityDef");
        private static readonly AbilityDef decoy = DefCache.GetDef<AbilityDef>("Decoy_AbilityDef");
        private static readonly AbilityDef weakspot = DefCache.GetDef<AbilityDef>("WeakSpot_AbilityDef");
        private static readonly AbilityDef vanish = DefCache.GetDef<AbilityDef>("Vanish_AbilityDef");
        private static readonly AbilityDef sneakAttack = DefCache.GetDef<AbilityDef>("SneakAttack_AbilityDef");

        //priest

        private static readonly AbilityDef mindControl = DefCache.GetDef<AbilityDef>("Priest_MindControl_AbilityDef");
        private static readonly AbilityDef inducePanic = DefCache.GetDef<AbilityDef>("InducePanic_AbilityDef");
        private static readonly AbilityDef mindSense = DefCache.GetDef<AbilityDef>("MindSense_AbilityDef");
        private static readonly AbilityDef psychicWard = DefCache.GetDef<AbilityDef>("PsychicWard_AbilityDef");
        private static readonly AbilityDef mindCrush = DefCache.GetDef<AbilityDef>("MindCrush_AbilityDef");

        //sniper
        private static readonly AbilityDef extremeFocus = DefCache.GetDef<AbilityDef>("ExtremeFocus_AbilityDef");
        private static readonly AbilityDef armorBreak = DefCache.GetDef<AbilityDef>("ArmourBreak_AbilityDef");
        private static readonly AbilityDef masterMarksman = DefCache.GetDef<AbilityDef>("MasterMarksman_AbilityDef");
        private static readonly AbilityDef inspire = DefCache.GetDef<AbilityDef>("Inspire_AbilityDef");
        private static readonly AbilityDef markedForDeath = DefCache.GetDef<AbilityDef>("MarkedForDeath_AbilityDef");

        //Technician
        private static readonly AbilityDef fastUse = DefCache.GetDef<AbilityDef>("FastUse_AbilityDef");
        private static readonly AbilityDef electricReinforcement = DefCache.GetDef<AbilityDef>("ElectricReinforcement_AbilityDef");
        private static readonly AbilityDef stability = DefCache.GetDef<AbilityDef>("Stability_AbilityDef");
        private static readonly AbilityDef fieldMedic = DefCache.GetDef<AbilityDef>("FieldMedic_AbilityDef");
        private static readonly AbilityDef amplifyPain = DefCache.GetDef<AbilityDef>("AmplifyPain_AbilityDef");




        public static int difficultyLevel = 0;
        // public static Dictionary <string, string> FileNameSquadPic = new Dictionary<string, string>();

        public static Dictionary<string, int> HumanEnemiesAndTactics = new Dictionary<string, int>();

        public static int RollCount = 0;
        public static List<ContextHelpHintDef> TacticsHint = new List<ContextHelpHintDef>();
        // public static List <TacticalFaction> HumanEnemyTacticalFactions = new List<TacticalFaction>();

        /* [HarmonyPatch(typeof(GeoMission), "Launch")]
         public static class GeoMission_Launch_InfestationStory_Patch
         {
             public static void Postfix(GeoMission __instance)
             {
                 try
                 {

                     List<string> enemyFactionNames = new List<string>();
                     List<int> enemyParticpants = new List<int>();
                     foreach (MutualParticipantsRelations relations in __instance.MissionDef.ParticipantsRelations)
                     {
                         if (relations.HasParticipant(TacMissionParticipant.Player) && relations.MutualRelation == PhoenixPoint.Common.Core.FactionRelation.Enemy)
                         {
                             enemyParticpants.Add((int)relations.SecondParticipant);
                         }
                     }

                     foreach (TacMissionTypeParticipantData participantData in __instance.MissionDef.ParticipantsData)
                     {
                         if (enemyParticpants.Contains((int)participantData.ParticipantKind))
                         {
                             if (participantData.FactionDef.ShortName.Equals("ban")
                                || participantData.FactionDef.ShortName.Equals("nj") || participantData.FactionDef.ShortName.Equals("anu")
                                || participantData.FactionDef.ShortName.Equals("syn") || participantData.FactionDef.ShortName.Equals("Purists")
                                || participantData.FactionDef.ShortName.Equals("FallenOnes"))
                                 TFTVLogger.Always("On GeoMission launch, the short name of the faction is " + participantData.FactionDef.ShortName);
                             enemyFactionNames.Add(participantData.FactionDef.ShortName);
                         }
                     }

                     foreach (string name in enemyFactionNames)
                     {
                         RollTactic(name);
                     }



                 }
                 catch (Exception e)
                 {
                     TFTVLogger.Error(e);
                 }
             }
         }*/

        public static void RollTactic(string nameOfFaction)
        {
            try
            {
                UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                int roll = UnityEngine.Random.Range(1, 10);

                TFTVLogger.Always("The tactics roll is " + roll);


                if (!HumanEnemiesAndTactics.ContainsKey(nameOfFaction))
                {
                    HumanEnemiesAndTactics.Add(nameOfFaction, roll);
                    RollCount++;
                }

                TFTVLogger.Always("Tactics have been rolled " + RollCount + " times, and there are currently " + HumanEnemiesAndTactics.Count + " tactics in play.");

            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }
        public static string GenerateGangName()
        {
            try
            {
                UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                int adjectivesNumber = UnityEngine.Random.Range(0, TFTVHumanEnemiesNames.adjectives.Count());
                UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                int nounsNumber = UnityEngine.Random.Range(0, TFTVHumanEnemiesNames.nouns.Count());
                string name = TFTVHumanEnemiesNames.adjectives[adjectivesNumber] + " " + TFTVHumanEnemiesNames.nouns[nounsNumber];
                TFTVLogger.Always("The gang names is" + name);
                return name;
            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }

        public static void GenerateHumanEnemyUnit(TacticalFaction enemyHumanFaction, string nameOfLeader, int roll)
        {
            try
            {
                string nameOfGang = GenerateGangName();
                string unitType = "";

                string tactic = "";
                string description = "";
                if (roll == 1)
                {
                    description = "Enemies who can see the character lose 1 WP";
                    tactic = "Fearsome";
                }
                else if (roll == 2)
                {
                    description = "Allies first attack with firearms costs 1 AP less";
                    tactic = "Starting volley";
                }
                else if (roll == 3)
                {
                    description = "All lowest tier friendlies gain 10 regeneration";
                    tactic = "Experimental drugs";
                }
                else if (roll == 4)
                {
                    description = "Character and allies within 12 tiles get +100% stealth";
                    tactic = "Active camo";
                }
                else if (roll == 5)
                {
                    description = "Allies within 20 tiles have return fire ability";
                    tactic = "Fire discipline";
                }
                else if (roll == 6)
                {
                    description = "When any high ranking character dies, allies gain frenzy status";
                    tactic = "Blood frenzy";
                }
                else if (roll == 7)
                {
                    description = "Enemy that attacks character becomes Marked for Death";
                    tactic = "Retribution";
                }
                else if (roll == 8)
                {
                    description = "Each ally does +10% damage while leader is alive if there are no enemies in sight within 10 tiles at the start of its turn";
                    tactic = "Ambush";
                }
                else if (roll == 9)
                {
                    description = "While leader is alive each ally gains +15% accuracy per ally within 12 tiles, up to +60%";
                    tactic = "Assisted targeting";
                }

                string nameOfTactic = tactic;
                string descriptionOfTactic = description;
                // string factionTag = "";

                if (enemyHumanFaction.TacticalFactionDef.ShortName.Equals("ban"))
                {
                    unitType = "a gang";
                    //  factionTag= "NEU_Bandits_TacticalFactionDef";
                    //  FileNameSquadPic = "ban_squad.png";
                }
                else if (enemyHumanFaction.TacticalFactionDef.ShortName.Equals("nj") || enemyHumanFaction.Faction.FactionDef.ShortName.Equals("anu") || enemyHumanFaction.Faction.FactionDef.ShortName.Equals("syn"))
                {
                    string factionName = "";
                    if (enemyHumanFaction.TacticalFactionDef.ShortName.Equals("nj"))
                    {
                        factionName = "New Jericho";
                        //  factionTag = "NewJericho_TacticalFactionDef";
                        // FileNameSquadPic = "nj_squad.jpg";
                    }
                    else if (enemyHumanFaction.TacticalFactionDef.ShortName.Equals("anu"))
                    {
                        factionName = "Disciples of Anu";
                        //  factionTag = "Anu_TacticalFactionDef";
                        //  FileNameSquadPic = "anu_squad.jpg";
                    }
                    else
                    {
                        factionName = "Synedrion";
                        //  factionTag = "Synedrion_TacticalFactionDef";                        
                        //  FileNameSquadPic = "syn_squad.jpg";
                    }

                    unitType = "a " + factionName + " squad";
                }
                else if (enemyHumanFaction.TacticalFactionDef.ShortName.Equals("FallenOnes"))
                {
                    unitType = "a pack of Forsaken";
                    // factionTag = "AN_FallenOnes_TacticalFactionDef";
                    //  FileNameSquadPic = "fo_squad.png";
                }
                else if (enemyHumanFaction.TacticalFactionDef.ShortName.Equals("Purists"))
                {
                    unitType = "an array of the Pure";
                    // factionTag = "NJ_Purists_TacticalFactionDef";

                    //  FileNameSquadPic = "pu_squad.jpg";
                }

                string descriptionHint = "You are facing " + unitType + ", called the " + nameOfGang +
                    ". Their leader is " + nameOfLeader + ", using the tactic " + nameOfTactic + ": " + descriptionOfTactic;


                TFTVTutorialAndStory.CreateNewTacticalHintForHumanEnemies(nameOfGang, HintTrigger.ActorSeen, "HumanEnemyFaction_" + enemyHumanFaction.TacticalFactionDef.ShortName + "_GameTagDef", nameOfGang, descriptionHint);
                ContextHelpHintDef humanEnemySightedHint = Repo.GetAllDefs<ContextHelpHintDef>().FirstOrDefault(ged => ged.name.Equals(nameOfGang));
                //humanEnemySightedHint.Title = new LocalizedTextBind(nameOfGang, true);
                //humanEnemySightedHint.Text = new LocalizedTextBind(descriptionHint, true);
                TacticsHint.Add(humanEnemySightedHint);

                //  LocalizedTextBind title = new LocalizedTextBind(nameOfGang, true);
                //  LocalizedTextBind text = new LocalizedTextBind(descriptionHint, true);
                //  Sprite sprite = Helper.CreateSpriteFromImageFile(FileNameSquadPic);
                //  leaderSightedHint.AnyCondition = true;
                // UIModuleContextHelp uIModuleContextHelp = (UIModuleContextHelp)UnityEngine.Object.FindObjectOfType(typeof(UIModuleContextHelp));
                // uIModuleContextHelp.Show(title, text, sprite, leaderSightedHint.VideoDef, false, leaderSightedHint, true);
                //  TFTVLogger.Always("The hint should have appeared");
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void AssignHumanEnemiesTags(TacticalLevelController controller)
        {
            try
            {
                TacticalFaction phoenix = controller.GetFactionByCommandName("PX");


                foreach (TacticalFaction faction in GetHumanEnemyFactions(controller))
                {
                    List<TacticalActor> listOfHumansEnemies = new List<TacticalActor>();

                    foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                    {
                        if (tacticalActorBase.BaseDef.name == "Soldier_ActorDef" && tacticalActorBase.InPlay)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;
                            listOfHumansEnemies.Add(tacticalActor);
                        }
                    }

                    if (listOfHumansEnemies.Count == 0)
                    {
                        return;
                    }

                    TFTVLogger.Always("There are " + listOfHumansEnemies.Count() + " human enemies");
                    List<TacticalActor> orderedListOfHumanEnemies = listOfHumansEnemies.OrderByDescending(e => e.LevelProgression.Level).ToList();
                    for (int i = 0; i < listOfHumansEnemies.Count; i++)
                    {
                        TFTVLogger.Always("TacticalActor is " + orderedListOfHumanEnemies[i].DisplayName + " and its level is " + listOfHumansEnemies[i].LevelProgression.Level);
                    }

                    if (listOfHumansEnemies[0].LevelProgression.Level == listOfHumansEnemies[listOfHumansEnemies.Count - 1].LevelProgression.Level)
                    {
                        TFTVLogger.Always("All enemies are of the same level");
                        orderedListOfHumanEnemies = listOfHumansEnemies.OrderByDescending(e => e.CharacterStats.Willpower.IntValue).ToList();
                    }

                    for (int i = 0; i < orderedListOfHumanEnemies.Count; i++)
                    {
                        TFTVLogger.Always("The character is " + orderedListOfHumanEnemies[i].name + " and their WP are " + orderedListOfHumanEnemies[i].CharacterStats.Willpower.IntValue);
                    }

                    TacticalActor leader = orderedListOfHumanEnemies[0];


                    orderedListOfHumanEnemies.Remove(leader);

                    int champs = Mathf.FloorToInt(orderedListOfHumanEnemies.Count / (5 - (difficultyLevel / 2)));
                    TFTVLogger.Always("There is space for " + champs + " champs");
                    int gangers = Mathf.FloorToInt((orderedListOfHumanEnemies.Count - champs) / (4 - (difficultyLevel / 2)));
                    TFTVLogger.Always("There is space for " + gangers + " gangers");
                    int juves = orderedListOfHumanEnemies.Count - champs - gangers;
                    TFTVLogger.Always("There is space for " + juves + " juves");

                    TacticalActorBase leaderBase = leader;
                    string nameOfFaction = faction.Faction.FactionDef.ShortName;

                    TFTVLogger.Always("The short name of the faction is " + nameOfFaction);
                    GameTagDef gameTagDef = DefCache.GetDef<GameTagDef>("HumanEnemyFaction_" + nameOfFaction + "_GameTagDef");
                    TFTVLogger.Always("gameTagDef found");
                    List<string> factionNames = TFTVHumanEnemiesNames.names.GetValueSafe(nameOfFaction);

                    if (!leaderBase.GameTags.Contains(HumanEnemyTier1GameTag))
                    {
                        leaderBase.GameTags.Add(HumanEnemyTier1GameTag);
                        TFTVLogger.Always("Tier1GameTag assigned");
                        leaderBase.GameTags.Add(gameTagDef);
                        TFTVLogger.Always("GameTagDef assigned");
                        leaderBase.GameTags.Add(humanEnemyTagDef, GameTagAddMode.ReplaceExistingExclusive);
                        TFTVLogger.Always("humanEnemyTagDef assigned");

                        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                        leader.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                        factionNames.Remove(leader.name);
                        TFTVLogger.Always("Leader now has GameTag and their name is " + leader.name);
                        AdjustStatsAndSkills(leader);
                    }

                    RollTactic(nameOfFaction);
                    GenerateHumanEnemyUnit(faction, leader.name, HumanEnemiesAndTactics[nameOfFaction]);


                    for (int i = 0; i < champs; i++)
                    {
                        TacticalActorBase champ = orderedListOfHumanEnemies[i];
                        if (!champ.GameTags.Contains(gameTagDef))
                        {

                            champ.GameTags.Add(HumanEnemyTier2GameTag);
                            champ.GameTags.Add(gameTagDef);
                            champ.GameTags.Add(humanEnemyTagDef, GameTagAddMode.ReplaceExistingExclusive);
                            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                            champ.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                            TacticalActor tacticalActor = champ as TacticalActor;
                            AdjustStatsAndSkills(tacticalActor);
                            factionNames.Remove(champ.name);
                        }
                        TFTVLogger.Always("This " + champ.name + " is now a champ");
                    }

                    for (int i = champs; i < champs + gangers; i++)
                    {
                        TacticalActorBase ganger = orderedListOfHumanEnemies[i];
                        if (!ganger.GameTags.Contains(gameTagDef))
                        {

                            ganger.GameTags.Add(HumanEnemyTier3GameTag);
                            ganger.GameTags.Add(gameTagDef);
                            ganger.GameTags.Add(humanEnemyTagDef, GameTagAddMode.ReplaceExistingExclusive);
                            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                            ganger.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                            TacticalActor tacticalActor = ganger as TacticalActor;
                            AdjustStatsAndSkills(tacticalActor);
                            factionNames.Remove(ganger.name);

                        }
                        TFTVLogger.Always("This " + ganger.name + " is now a ganger");

                    }

                    for (int i = champs + gangers; i < champs + gangers + juves; i++)
                    {
                        TacticalActorBase juve = orderedListOfHumanEnemies[i];
                        if (!juve.GameTags.Contains(gameTagDef))
                        {
                            juve.GameTags.Add(HumanEnemyTier4GameTag);
                            juve.GameTags.Add(gameTagDef);
                            juve.GameTags.Add(humanEnemyTagDef, GameTagAddMode.ReplaceExistingExclusive);
                            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                            juve.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                            TacticalActor tacticalActor = juve as TacticalActor;
                            AdjustStatsAndSkills(tacticalActor);
                            factionNames.Remove(juve.name);
                        }
                        TFTVLogger.Always("This " + juve.name + " is now a juve");


                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static List<TacticalFaction> GetHumanEnemyFactions(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = new List<TacticalFaction>();

                TacticalFaction phoenix = controller.GetFactionByCommandName("PX");

                foreach (TacticalFaction faction in controller.Factions)
                {
                    if (faction.GetRelationTo(phoenix) == PhoenixPoint.Common.Core.FactionRelation.Enemy)
                    {
                        if (faction.Faction.FactionDef.ShortName.Equals("ban")
                                || faction.Faction.FactionDef.ShortName.Equals("nj") || faction.Faction.FactionDef.ShortName.Equals("anu")
                                || faction.Faction.FactionDef.ShortName.Equals("syn") || faction.Faction.FactionDef.ShortName.Equals("Purists")
                                || faction.Faction.FactionDef.ShortName.Equals("FallenOnes"))
                        {
                            TFTVLogger.Always("The short name of the faction is " + faction.Faction.FactionDef.ShortName);

                            enemyHumanFactions.Add(faction);
                        }
                    }
                }

                return enemyHumanFactions;

            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }

        [HarmonyPatch(typeof(TacticalActorBase), "get_DisplayName")]
        public static class TacticalActorBase_GetDisplayName_HumanEnemiesGenerator_Patch
        {
            public static void Postfix(TacticalActorBase __instance, ref string __result)
            {
                try
                {
                    if (GetFactionTierAndClassTags(__instance.GameTags.ToList())[0] != null)
                    {
                        __result = __instance.name + GetRankName(GetFactionTierAndClassTags(__instance.GameTags.ToList()));
                    }

                }

                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }

            }
        }
        public static GameTagDef[] GetFactionTierAndClassTags(List<GameTagDef> data)
        {
            try
            {
                GameTagDef[] factionTierAndClass = new GameTagDef[3];
                GameTagDef factionGameTagDef = null;
                GameTagDef tierGameTagDef = null;
                GameTagDef classTagDef = null;
                foreach (GameTagDef tag in data)
                {
                    //  TFTVLogger.Always("Here is tag with name " + tag.name);
                    if (tag.name.Contains("HumanEnemyFaction"))
                    {
                        factionGameTagDef = tag;
                    }
                    // TFTVLogger.Always("The character has the tag " + tag.name);

                    //  TFTVLogger.Always("Here is tag with name " + tag.name);
                    else if (tag.name.Contains("HumanEnemyTier"))
                    {
                        tierGameTagDef = tag;
                        // TFTVLogger.Always("The character has the tag " + tag.name);
                    }
                    else if (tag.name.Contains("Class"))
                    {
                        classTagDef = tag;
                        //  TFTVLogger.Always("The character has the tag " + tag.name);
                    }
                }
                factionTierAndClass[0] = factionGameTagDef;
                factionTierAndClass[1] = tierGameTagDef;
                factionTierAndClass[2] = classTagDef;

                return factionTierAndClass;
            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }
        public static string GetRankName(GameTagDef[] gameTags)
        {
            try
            {

                string factionName = gameTags[0].name.Split('_')[1];
                // TFTVLogger.Always("Faction name is " + factionName);
                List<string> nameRank = TFTVHumanEnemiesNames.ranks.GetValueSafe(factionName);
                string rank = gameTags[1].name.Split('_')[1];
                int rankOrder = int.Parse(rank);
                //  TFTVLogger.Always("Rank order is " + rankOrder);
                string rankName = " " + "(" + nameRank[rankOrder - 1] + ")";

                return rankName;
            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }

        [HarmonyPatch(typeof(UIModuleCharacterStatus), "SetData")]
        public static class UIModuleCharacterStatus_SetData_AdjustLevel_Patch
        {
            public static void Postfix(CharacterData data, UIModuleCharacterStatus __instance, ListControl ____abilitiesList)
            {
                try
                {

                    GameTagDef[] factionAndTier = GetFactionTierAndClassTags(data.Tags.ToList());
                    if (factionAndTier[0] != null)
                    {
                        ____abilitiesList.AddRow<CharacterStatusAbilityRowController>
                                (__instance.AbilitiesListAbilityPrototype).SetData(ApplyTextChanges(factionAndTier[0], factionAndTier[1]));
                        if (factionAndTier[1] == HumanEnemyTier1GameTag)
                        {
                            if (data.Level < 6)
                            {
                                __instance.CharacterLevel.text = "6";
                            }

                            string factionName = factionAndTier[0].name.Split('_')[1];
                            int roll = HumanEnemiesAndTactics[factionName];
                            TFTVLogger.Always("factionName is " + factionName + " and the roll is " + roll);
                            ____abilitiesList.AddRow<CharacterStatusAbilityRowController>
                               (__instance.AbilitiesListAbilityPrototype).SetData(AddTacticsDescription(roll));

                        }
                        else if (factionAndTier[1] == HumanEnemyTier2GameTag)
                        {
                            if (data.Level < 5)
                            {
                                __instance.CharacterLevel.text = "5";
                            }
                            else if (data.Level > 6)
                            {
                                __instance.CharacterLevel.text = "6";

                            }

                        }
                        else if (factionAndTier[1] == HumanEnemyTier3GameTag)
                        {
                            if (data.Level < 3)
                            {
                                __instance.CharacterLevel.text = "3";
                            }
                            else if (data.Level > 4)
                            {
                                __instance.CharacterLevel.text = "4";

                            }

                        }
                        else if (factionAndTier[1] == HumanEnemyTier4GameTag)
                        {
                            if (data.Level > 2)
                            {
                                __instance.CharacterLevel.text = "2";
                            }
                        }
                    }
                }

                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }

            }
        }
        public static AbilityData ApplyTextChanges(GameTagDef factionTag, GameTagDef tierTag)
        {

            try
            {
                string factionName = factionTag.name.Split('_')[1];
                // TFTVLogger.Always("Faction name is " + factionName);
                List<string> nameRank = TFTVHumanEnemiesNames.ranks.GetValueSafe(factionName);
                string rank = tierTag.name.Split('_')[1];
                int rankOrder = int.Parse(rank);
                // TFTVLogger.Always("Rank order is " + rankOrder);

                string name = nameRank[rankOrder - 1];
                string description = TFTVHumanEnemiesNames.tierDescriptions[rankOrder - 1];
                AbilityData abilityData = new AbilityData
                {
                    Name = new LocalizedTextBind(name, true),
                    LocalizedDescription = description,
                    Icon = Helper.CreateSpriteFromImageFile("UI_StatusesIcons_CanBeRecruitedIntoPhoenix-2.png")
                };
                return abilityData;

            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }
        public static AbilityData AddTacticsDescription(int roll)
        {
            try
            {
                string name = "Tactic";
                string tactic = "";
                string description = "";
                if (roll == 1)
                {
                    description = "Enemies who can see the character lose 1 WP at the start of their turn";
                    tactic = " - Fearsome";
                }
                else if (roll == 2)
                {
                    description = "Allies first attack with firearms costs 1 AP less";
                    tactic = " - Starting volley";
                }
                else if (roll == 3)
                {
                    description = "All lowest tier friendlies gain 10 regeneration";
                    tactic = " - Experimental drugs";
                }

                else if (roll == 4)
                {
                    description = "Character and allies within 12 tiles get +100% stealth";
                    tactic = " - Active camo";
                }
                else if (roll == 5)
                {
                    description = "Allies within 20 tiles have return fire ability";
                    tactic = " - Fire discipline";
                }
                else if (roll == 6)
                {
                    description = "When any high ranking character dies (level 4 and above), allies gain frenzy status";
                    tactic = " - Blood frenzy";
                }
                else if (roll == 7)
                {
                    description = "Enemy that attacks character becomes Marked for Death";
                    tactic = " - Retribution";
                }
                else if (roll == 8)
                {
                    description = "Each ally does +10% damage while leader is alive if there are no enemies in sight within 10 tiles at the start of its turn";
                    tactic = " - Ambush";
                }
                else if (roll == 9)
                {
                    description = "While leader is alive each ally gains +15% accuracy per ally within 12 tiles, up to +60%";
                    tactic = "Assisted targeting";
                }


                AbilityData abilityData = new AbilityData
                {
                    Name = new LocalizedTextBind(name + tactic, true),
                    LocalizedDescription = description,
                    Icon = Helper.CreateSpriteFromImageFile("UI_AbilitiesIcon_PersonalTrack_SpecOp-1.png")
                };

                return abilityData;
            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();

        }

        public static int GetAdjustedLevel(TacticalActor tacticalActor)
        {
            try
            {
                int startingLevel = tacticalActor.LevelProgression.Level;
                TFTVLogger.Always("Starting level is " + startingLevel);
                GameTagDef tierTagDef = GetFactionTierAndClassTags(tacticalActor.GameTags.ToList())[1];

                string rank = tierTagDef.name.Split('_')[1];
                int rankOrder = int.Parse(rank);
                TFTVLogger.Always("The rank is " + rankOrder);

                if (rankOrder == 1)
                {
                    if (startingLevel == 7)
                    {
                        return 7;
                    }
                    else
                    {
                        return 6;

                    }

                }
                else if (rankOrder == 2)
                {
                    if (startingLevel <= 5)
                    {
                        return 5;
                    }
                    else
                    {
                        return 6;

                    }
                }
                else if (rankOrder == 3)
                {
                    if (startingLevel <= 3)
                    {
                        return 3;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else if (rankOrder == 4)
                {
                    if (startingLevel >= 2)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }

            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }

        public static void AdjustStatsAndSkills(TacticalActor tacticalActor)
        {
            try
            {

                List<AbilityDef> assaultAbilities = new List<AbilityDef> { quickAim, killAndRun, onslaught, readyForAction, rapidClearance };
                List<AbilityDef> berserkerAbilities = new List<AbilityDef> { dash, cqc, bloodlust, ignorePain, adrenalineRush };
                List<AbilityDef> heavyAbilities = new List<AbilityDef> { returnFire, hunkerDown, skirmisher, shredResistant, rageBurst };
                List<AbilityDef> infiltratorAbilities = new List<AbilityDef> { surpriseAttack, decoy, weakspot, vanish, sneakAttack };
                List<AbilityDef> priestAbilities = new List<AbilityDef> { mindControl, inducePanic, mindSense, psychicWard, mindCrush };
                List<AbilityDef> sniperAbilities = new List<AbilityDef> { extremeFocus, armorBreak, masterMarksman, inspire, markedForDeath };
                List<AbilityDef> technicianAbilities = new List<AbilityDef> { fastUse, electricReinforcement, stability, fieldMedic, amplifyPain };

                List<AbilityDef> allAbilities = new List<AbilityDef>();

                allAbilities.AddRange(assaultAbilities);
                allAbilities.AddRange(berserkerAbilities);
                allAbilities.AddRange(heavyAbilities);
                allAbilities.AddRange(infiltratorAbilities);
                allAbilities.AddRange(priestAbilities);
                allAbilities.AddRange(sniperAbilities);
                allAbilities.AddRange(technicianAbilities);

                List<AbilityDef> discardedAbilities = allAbilities;
                discardedAbilities.Remove(quickAim);
                discardedAbilities.Remove(dash);
                discardedAbilities.Remove(cqc);
                discardedAbilities.Remove(bloodlust);
                discardedAbilities.Remove(ignorePain);
                discardedAbilities.Remove(returnFire);
                discardedAbilities.Remove(skirmisher);
                discardedAbilities.Remove(shredResistant);
                discardedAbilities.Remove(surpriseAttack);
                discardedAbilities.Remove(weakspot);
                discardedAbilities.Remove(sneakAttack);
                discardedAbilities.Remove(mindControl);
                discardedAbilities.Remove(mindSense);
                discardedAbilities.Remove(mindCrush);
                discardedAbilities.Remove(psychicWard);
                discardedAbilities.Remove(extremeFocus);
                discardedAbilities.Remove(masterMarksman);
                discardedAbilities.Remove(inspire);
                discardedAbilities.Remove(fastUse);
                discardedAbilities.Remove(electricReinforcement);
                discardedAbilities.Remove(stability);
                discardedAbilities.Remove(fieldMedic);

                foreach (AbilityDef ability in discardedAbilities)
                {
                    if (tacticalActor.GetAbilityWithDef<Ability>(ability) != null)
                    {
                        tacticalActor.RemoveAbility(ability);
                    }
                }

                int level = GetAdjustedLevel(tacticalActor);
                GameTagDef classTagDef = GetFactionTierAndClassTags(tacticalActor.GameTags.ToList())[2];

                tacticalActor.CharacterStats.Willpower.SetMax(5 + Mathf.FloorToInt(difficultyLevel / 2) + level + GetStatBuffForTier(tacticalActor) / 2);
                tacticalActor.CharacterStats.Willpower.Set(5 + Mathf.FloorToInt(difficultyLevel / 2) + level + GetStatBuffForTier(tacticalActor) / 2);
                tacticalActor.CharacterStats.Speed.SetMax(12 + Mathf.CeilToInt(difficultyLevel / 3) + level + GetStatBuffForTier(tacticalActor) / 3);
                tacticalActor.CharacterStats.Speed.Set(12 + Mathf.CeilToInt(difficultyLevel / 3) + level + GetStatBuffForTier(tacticalActor) / 3);
                tacticalActor.CharacterStats.Endurance.SetMax(12 + difficultyLevel + level * 2);
                tacticalActor.CharacterStats.Endurance.Set(12 + difficultyLevel + level * 2);
                tacticalActor.CharacterStats.Health.SetMax(130 + difficultyLevel * 10 + level * 2 * 10);
                tacticalActor.CharacterStats.Health.SetToMax();
                tacticalActor.UpdateStats();


                if (classTagDef.name.Contains("Assault"))
                {
                    if (level >= 2)
                    {

                        tacticalActor.AddAbility(quickAim, tacticalActor);
                        tacticalActor.UpdateStats();
                    }
                    else
                    {
                        if (tacticalActor.GetAbilityWithDef<Ability>(quickAim) != null)
                        {
                            tacticalActor.RemoveAbility(quickAim);
                        }
                    }
                }
                if (classTagDef.name.Contains("Berserker"))
                {
                    if (level >= 6)
                    {
                        tacticalActor.AddAbility(dash, tacticalActor);
                        tacticalActor.AddAbility(cqc, tacticalActor);
                        tacticalActor.AddAbility(bloodlust, tacticalActor);
                        tacticalActor.AddAbility(ignorePain, tacticalActor);
                    }
                    else if (level == 5)
                    {
                        tacticalActor.AddAbility(dash, tacticalActor);
                        tacticalActor.AddAbility(cqc, tacticalActor);
                        tacticalActor.AddAbility(bloodlust, tacticalActor);
                    }
                    else if (level >= 3)
                    {
                        tacticalActor.AddAbility(dash, tacticalActor);
                        tacticalActor.AddAbility(cqc, tacticalActor);
                    }
                    else if (level == 2)
                    {
                        tacticalActor.AddAbility(dash, tacticalActor);
                    }
                }
                if (classTagDef.name.Contains("Heavy"))
                {
                    if (level >= 6)
                    {
                        tacticalActor.AddAbility(returnFire, tacticalActor);
                        tacticalActor.AddAbility(shredResistant, tacticalActor);
                        tacticalActor.AddAbility(skirmisher, tacticalActor);
                    }
                    else if (level == 5)
                    {
                        tacticalActor.AddAbility(returnFire, tacticalActor);
                        tacticalActor.AddAbility(skirmisher, tacticalActor);

                    }
                    else if (level >= 2)
                    {
                        tacticalActor.AddAbility(returnFire, tacticalActor);
                    }
                }
                if (classTagDef.name.Contains("Infiltrator"))
                {
                    if (level == 7)
                    {
                        tacticalActor.AddAbility(sneakAttack, tacticalActor);
                        tacticalActor.AddAbility(weakspot, tacticalActor);
                        tacticalActor.AddAbility(surpriseAttack, tacticalActor);
                    }
                    else if (level >= 5)
                    {
                        tacticalActor.AddAbility(weakspot, tacticalActor);
                        tacticalActor.AddAbility(surpriseAttack, tacticalActor);

                    }
                    else if (level >= 2)
                    {
                        tacticalActor.AddAbility(surpriseAttack, tacticalActor);
                    }
                }
                if (classTagDef.name.Contains("Priest"))
                {
                    if (level == 7)
                    {
                        tacticalActor.AddAbility(mindCrush, tacticalActor);
                        tacticalActor.AddAbility(psychicWard, tacticalActor);
                        tacticalActor.AddAbility(mindSense, tacticalActor);
                        tacticalActor.AddAbility(mindControl, tacticalActor);

                    }
                    else if (level == 6)
                    {
                        tacticalActor.AddAbility(psychicWard, tacticalActor);
                        tacticalActor.AddAbility(mindSense, tacticalActor);
                        tacticalActor.AddAbility(mindControl, tacticalActor);

                    }
                    else if (level == 5)
                    {
                        tacticalActor.AddAbility(mindSense, tacticalActor);
                        tacticalActor.AddAbility(mindControl, tacticalActor);
                    }
                    else if (level >= 2)
                    {
                        tacticalActor.AddAbility(mindControl, tacticalActor);
                    }
                }
                if (classTagDef.name.Contains("Priest"))
                {
                    if (level == 7)
                    {
                        tacticalActor.AddAbility(mindCrush, tacticalActor);
                        tacticalActor.AddAbility(psychicWard, tacticalActor);
                        tacticalActor.AddAbility(mindSense, tacticalActor);
                        tacticalActor.AddAbility(mindControl, tacticalActor);

                    }
                    else if (level == 6)
                    {
                        tacticalActor.AddAbility(psychicWard, tacticalActor);
                        tacticalActor.AddAbility(mindSense, tacticalActor);
                        tacticalActor.AddAbility(mindControl, tacticalActor);

                    }
                    else if (level == 5)
                    {
                        tacticalActor.AddAbility(mindSense, tacticalActor);
                        tacticalActor.AddAbility(mindControl, tacticalActor);
                    }
                    else if (level >= 2)
                    {
                        tacticalActor.AddAbility(mindControl, tacticalActor);
                    }
                }
                if (classTagDef.name.Contains("Sniper"))
                {
                    if (level >= 6)
                    {
                        tacticalActor.AddAbility(inspire, tacticalActor);
                        tacticalActor.AddAbility(masterMarksman, tacticalActor);
                        tacticalActor.AddAbility(extremeFocus, tacticalActor);
                    }
                    else if (level == 5)
                    {
                        tacticalActor.AddAbility(masterMarksman, tacticalActor);
                        tacticalActor.AddAbility(extremeFocus, tacticalActor);

                    }
                    else if (level >= 2)
                    {
                        tacticalActor.AddAbility(extremeFocus, tacticalActor);
                    }
                }
                if (classTagDef.name.Contains("Technician"))
                {
                    if (level >= 6)
                    {
                        tacticalActor.AddAbility(electricReinforcement, tacticalActor);
                        tacticalActor.AddAbility(fastUse, tacticalActor);
                        tacticalActor.AddAbility(stability, tacticalActor);
                        tacticalActor.AddAbility(fieldMedic, tacticalActor);
                    }
                    else if (level == 5)
                    {
                        tacticalActor.AddAbility(electricReinforcement, tacticalActor);
                        tacticalActor.AddAbility(fastUse, tacticalActor);
                        tacticalActor.AddAbility(stability, tacticalActor);
                    }
                    else if (level >= 3)
                    {
                        tacticalActor.AddAbility(electricReinforcement, tacticalActor);
                        tacticalActor.AddAbility(fastUse, tacticalActor);
                    }
                    else if (level == 2)
                    {
                        tacticalActor.AddAbility(fastUse, tacticalActor);
                    }
                }

            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static int GetStatBuffForTier(TacticalActor tacticalActor)
        {
            try
            {

                GameTagDef tierTagDef = GetFactionTierAndClassTags(tacticalActor.GameTags.ToList())[1];
                string rank = tierTagDef.name.Split('_')[1];
                int rankOrder = int.Parse(rank);

                int buff = (4 - rankOrder) * (difficultyLevel);

                return buff;
            }

            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
            throw new InvalidOperationException();
        }

        [HarmonyPatch(typeof(TacticalActor), "OnAnotherActorDeath")]
        public static class TacticalActor_OnAnotherActorDeath_HumanEnemies_Patch
        {
            public static void Postfix(TacticalActor __instance, DeathReport death)
            {

                try
                {

                    if (death.Actor.HasGameTag(HumanEnemyTier4GameTag))
                    {
                        TacticalFaction tacticalFaction = death.Actor.TacticalFaction;
                        int willPointWorth = death.Actor.TacticalActorBaseDef.WillPointWorth;
                        if (death.Actor.TacticalFaction == __instance.TacticalFaction)
                        {
                            __instance.CharacterStats.WillPoints.Add(willPointWorth);
                        }
                    }
                    else if (death.Actor.HasGameTag(HumanEnemyTier2GameTag))
                    {
                        TacticalFaction tacticalFaction = death.Actor.TacticalFaction;
                        if (death.Actor.TacticalFaction == __instance.TacticalFaction)
                        {
                            __instance.CharacterStats.WillPoints.Subtract(1);
                        }
                    }
                    else if (death.Actor.HasGameTag(HumanEnemyTier1GameTag))
                    {
                        TacticalFaction tacticalFaction = death.Actor.TacticalFaction;
                        int willPointWorth = death.Actor.TacticalActorBaseDef.WillPointWorth;
                        if (death.Actor.TacticalFaction == __instance.TacticalFaction)
                        {
                            __instance.CharacterStats.WillPoints.Subtract(willPointWorth);
                        }
                    }
                }
                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }
            }
        }

        [HarmonyPatch(typeof(TacticalLevelController), "ActorEnteredPlay")]
        public static class TacticalLevelController_ActorEnteredPlay_HumanEnemies_Patch
        {
            public static void Postfix(TacticalActorBase actor, TacticalLevelController __instance)
            {
                try
                {
                    if (HumanEnemiesAndTactics != null && actor.BaseDef.name == "Soldier_ActorDef" && actor.InPlay
                        && __instance.CurrentFaction != __instance.GetFactionByCommandName("PX"))
                    {
                        TFTVLogger.Always("The turn number is " + __instance.TurnNumber);
                        if (GetHumanEnemyFactions(__instance) != null)
                        {
                            foreach (TacticalFaction faction in GetHumanEnemyFactions(__instance))
                            {
                                string nameOfFaction = faction.Faction.FactionDef.ShortName;
                                GameTagDef gameTagDef = DefCache.GetDef<GameTagDef>("HumanEnemyFaction_" + nameOfFaction + "_GameTagDef");
                                List<string> factionNames = TFTVHumanEnemiesNames.names[nameOfFaction];

                                if (faction.Actors.Contains(actor))
                                {
                                    UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                                    int rankNumber = UnityEngine.Random.Range(1, 7);
                                    if (rankNumber == 6)
                                    {
                                        actor.GameTags.Add(HumanEnemyTier2GameTag);
                                        actor.GameTags.Add(gameTagDef);
                                        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                                        actor.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                                        TFTVLogger.Always("Name of new enemy is " + actor.name);
                                        TacticalActor tacticalActor = actor as TacticalActor;
                                        AdjustStatsAndSkills(tacticalActor);
                                        factionNames.Remove(actor.name);
                                        TFTVLogger.Always(actor.name + " is now a champ");
                                    }
                                    else if (rankNumber >= 4 && rankNumber < 6)
                                    {
                                        actor.GameTags.Add(HumanEnemyTier3GameTag);
                                        actor.GameTags.Add(gameTagDef);
                                        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                                        actor.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                                        TFTVLogger.Always("Name of new enemy is " + actor.name);
                                        TacticalActor tacticalActor = actor as TacticalActor;
                                        AdjustStatsAndSkills(tacticalActor);
                                        factionNames.Remove(actor.name);
                                        TFTVLogger.Always(actor.name + " is now a ganger");

                                    }
                                    else
                                    {
                                        actor.GameTags.Add(HumanEnemyTier4GameTag);
                                        actor.GameTags.Add(gameTagDef);
                                        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                                        actor.name = factionNames[UnityEngine.Random.Range(0, factionNames.Count)];
                                        TFTVLogger.Always("Name of new enemy is " + actor.name);
                                        TacticalActor tacticalActor = actor as TacticalActor;
                                        AdjustStatsAndSkills(tacticalActor);
                                        factionNames.Remove(actor.name);
                                        TFTVLogger.Always(actor.name + " is now a juve");
                                    }
                                }

                            }

                        }
                    }


                }
                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }
            }
        }

        public static void ChampRecoverWPAura(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(HumanEnemyTier2GameTag))
                            {
                                foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                {
                                    if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                    {
                                        TacticalActor actor = allyTacticalActorBase as TacticalActor;
                                        float magnitude = actor.GetAdjustedPerceptionValue();

                                        if ((allyTacticalActorBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                            && TacticalFactionVision.CheckVisibleLineBetweenActors(allyTacticalActorBase, allyTacticalActorBase.Pos, tacticalActor, true))
                                        {
                                            TFTVLogger.Always("Actor in range and has LoS");
                                            actor.CharacterStats.WillPoints.AddRestrictedToMax(1);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void ApplyTactic(TacticalLevelController controller)
        {
            try
            {
                foreach (string faction in HumanEnemiesAndTactics.Keys)
                {

                    if (HumanEnemiesAndTactics.GetValueSafe(faction) == 1)
                    {
                        TerrifyingAura(controller, faction);
                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 2)
                    {
                        // StartingVolley(controller);
                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 3)
                    {
                        ExperimentalDrugs(controller, faction);
                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 4)
                    {
                        OpticalShield(controller, faction);
                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 5)
                    {
                        FireDiscipline(controller, faction);
                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 6)
                    {

                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 7)
                    {

                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 8)
                    {
                        Ambush(controller, faction);

                    }
                    else if (HumanEnemiesAndTactics.GetValueSafe(faction) == 9)
                    {
                        AssistedTargeting(controller, faction);

                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void TerrifyingAura(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = GetHumanEnemyFactions(controller);
                TacticalFaction phoenix = controller.GetFactionByCommandName("PX");
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {
                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {
                                    foreach (TacticalActorBase phoenixSoldierBase in phoenix.Actors)
                                    {
                                        if (phoenixSoldierBase.BaseDef.name == "Soldier_ActorDef" && phoenixSoldierBase.InPlay)
                                        {
                                            TacticalActor actor = phoenixSoldierBase as TacticalActor;
                                            float magnitude = actor.GetAdjustedPerceptionValue();

                                            if ((phoenixSoldierBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                                && TacticalFactionVision.CheckVisibleLineBetweenActors(phoenixSoldierBase, phoenixSoldierBase.Pos, tacticalActor, true)
                                                && tacticalActor.IsAlive)
                                            {
                                                TFTVLogger.Always(actor.GetDisplayName() + " is within perception range and has LoS on " + tacticalActor.name);
                                                actor.CharacterStats.WillPoints.Subtract(1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void ExperimentalDrugs(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {
                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {
                                    foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                    {
                                        if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay && allyTacticalActorBase.HasGameTag(DefCache.GetDef<GameTagDef>("HumanEnemyTier_4_GameTagDef")))
                                        {
                                            TacticalActor actor = allyTacticalActorBase as TacticalActor;

                                            if (actor.GetAbilityWithDef<Ability>(regeneration) == null
                                                && !actor.HasStatus(regenerationStatus))
                                            {

                                                TFTVLogger.Always("Actor is getting the experimental drugs");
                                                actor.AddAbility(regeneration, actor);
                                                actor.Status.ApplyStatus(regenerationStatus);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void StartingVolley(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = GetHumanEnemyFactions(controller);
                TacticalFaction phoenix = controller.GetFactionByCommandName("PX");
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {
                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {
                                    foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                    {
                                        if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                        {
                                            if (tacticalActor.IsAlive)
                                            {
                                                TacticalActor actor = allyTacticalActorBase as TacticalActor;

                                                if (actor.Equipments.SelectedWeapon != null)
                                                {
                                                    float SelectedWeaponRange = actor.Equipments.SelectedWeapon.WeaponDef.EffectiveRange;
                                                    TFTVLogger.Always("The selected weapon is " + actor.Equipments.SelectedWeapon.DisplayName + " and its maximum range is " + actor.Equipments.SelectedWeapon.WeaponDef.EffectiveRange);

                                                    foreach (TacticalActorBase phoenixSoldierBase in phoenix.Actors)
                                                    {
                                                        if (phoenixSoldierBase.BaseDef.name == "Soldier_ActorDef" && phoenixSoldierBase.InPlay && (phoenixSoldierBase.Pos - allyTacticalActorBase.Pos).magnitude < SelectedWeaponRange / 2
                                                        && TacticalFactionVision.CheckVisibleLineBetweenActors(phoenixSoldierBase, phoenixSoldierBase.Pos, actor, true)
                                                        && !actor.Status.HasStatus(quickAimStatus))
                                                        {
                                                            TFTVLogger.Always("Actor is getting quick aim status");
                                                            //  actor.AddAbility(DefCache.GetDef<AbilityDef>("Regeneration_Torso_Passive_AbilityDef")), actor);
                                                            actor.Status.ApplyStatus(quickAimStatus);
                                                            //  actor.AddAbility(DefCache.GetDef<ApplyStatusAbilityDef>("QuickAim_AbilityDef")), actor);

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void OpticalShield(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {
                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor leader = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {
                                    StatModification stealthBuff = new StatModification() { Modification = StatModificationType.Add, Value = 1, StatName = "Stealth" };

                                    foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                    {
                                        if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                        {
                                            TacticalActor actor = allyTacticalActorBase as TacticalActor;
                                            float magnitude = 12;

                                            if (leader.IsAlive)
                                            {

                                                if ((allyTacticalActorBase.Pos - tacticalActorBase.Pos).magnitude < magnitude)
                                                {
                                                    if (!actor.CharacterStats.Stealth.Modifications.Contains(stealthBuff))
                                                    {
                                                        TFTVLogger.Always("Actor in range, has no optical shield, acquiring optical shield");
                                                        actor.CharacterStats.Stealth.AddStatModification(stealthBuff);

                                                    }
                                                    else
                                                    {
                                                        TFTVLogger.Always("Actor in range, has optical shield and keeps it");
                                                    }

                                                }

                                                else if ((allyTacticalActorBase.Pos - tacticalActorBase.Pos).magnitude >= magnitude
                                                    && actor.CharacterStats.Stealth.Modifications.Contains(stealthBuff))
                                                {
                                                    actor.CharacterStats.Stealth.RemoveStatModification(stealthBuff);
                                                }
                                            }
                                            else if (!leader.IsAlive && actor.CharacterStats.Stealth.Modifications.Contains(stealthBuff))
                                            {
                                                actor.CharacterStats.Stealth.RemoveStatModification(stealthBuff);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void AssistedTargeting(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {
                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor leader = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {
                                    foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                    {
                                        if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                        {
                                            StatModification accuracyBuff1 = new StatModification() { Modification = StatModificationType.Add, Value = 0.15f, StatName = "Accuracy" };
                                            StatModification accuracyBuff2 = new StatModification() { Modification = StatModificationType.Add, Value = 0.3f, StatName = "Accuracy" };
                                            StatModification accuracyBuff3 = new StatModification() { Modification = StatModificationType.Add, Value = 0.45f, StatName = "Accuracy" };
                                            StatModification accuracyBuff4 = new StatModification() { Modification = StatModificationType.Add, Value = 0.6f, StatName = "Accuracy" };

                                            TacticalActor actor = allyTacticalActorBase as TacticalActor;
                                            float magnitude = 12;
                                            int numberOAssists = 0;


                                            if (actor.CharacterStats.Accuracy.Modifications.Contains(accuracyBuff1))
                                            {
                                                actor.CharacterStats.Accuracy.RemoveStatModification(accuracyBuff1);
                                            }
                                            else if (actor.CharacterStats.Accuracy.Modifications.Contains(accuracyBuff2))
                                            {
                                                actor.CharacterStats.Accuracy.RemoveStatModification(accuracyBuff2);
                                            }
                                            else if (actor.CharacterStats.Accuracy.Modifications.Contains(accuracyBuff3))
                                            {
                                                actor.CharacterStats.Accuracy.RemoveStatModification(accuracyBuff3);
                                            }
                                            else if (actor.CharacterStats.Accuracy.Modifications.Contains(accuracyBuff4))
                                            {
                                                actor.CharacterStats.Accuracy.RemoveStatModification(accuracyBuff4);
                                            }

                                            if (leader.IsAlive)
                                            {
                                                foreach (TacticalActorBase assist in faction.Actors)

                                                    if ((allyTacticalActorBase.Pos - assist.Pos).magnitude <= magnitude
                                                        && allyTacticalActorBase != assist)
                                                    {
                                                        numberOAssists++;
                                                    }
                                                if (numberOAssists >= 4)
                                                {
                                                    actor.CharacterStats.Accuracy.AddStatModification(accuracyBuff4);
                                                }
                                                if (numberOAssists == 3)
                                                {
                                                    actor.CharacterStats.Accuracy.AddStatModification(accuracyBuff3);
                                                }
                                                if (numberOAssists == 2)
                                                {
                                                    actor.CharacterStats.Accuracy.AddStatModification(accuracyBuff2);
                                                }
                                                if (numberOAssists == 1)
                                                {
                                                    actor.CharacterStats.Accuracy.AddStatModification(accuracyBuff1);
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void FireDiscipline(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {

                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor leader = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {
                                    // StatModification stealthBuff = new StatModification() { Modification = StatModificationType.Add, Value = 1, StatName = "Stealth" };


                                    foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                    {
                                        if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                        {
                                            TacticalActor actor = allyTacticalActorBase as TacticalActor;
                                            float magnitude = 20;

                                            if (leader.IsAlive)
                                            {
                                                if ((allyTacticalActorBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                                    && actor.GetAbilityWithDef<Ability>(returnFire) == null && actor != leader)
                                                {
                                                    TFTVLogger.Always("Actor in range, return fire");
                                                    actor.AddAbility(returnFire, actor);
                                                }
                                                else if (actor.GetAbilityWithDef<Ability>(returnFire) != null && (!actor.HasGameTag
                                                    (heavy) && (actor.LevelProgression.Level > 1 ||
                                                    !actor.HasGameTag(HumanEnemyTier4GameTag))))
                                                {
                                                    actor.RemoveAbility(returnFire);
                                                }
                                            }
                                            else if (!leader.IsAlive && actor.GetAbilityWithDef<Ability>(returnFire) != null && (!actor.HasGameTag
                                                    (heavy) && (actor.LevelProgression.Level > 1 ||
                                                    !actor.HasGameTag(HumanEnemyTier4GameTag))))
                                            {
                                                actor.RemoveAbility(returnFire);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void Ambush(TacticalLevelController controller, string factionName)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        if (faction.Faction.FactionDef.ShortName == factionName)
                        {
                            foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                            {
                                TacticalActor leader = tacticalActorBase as TacticalActor;

                                if (tacticalActorBase.HasGameTag(HumanEnemyTier1GameTag))
                                {

                                    foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                    {
                                        if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                        {
                                            TacticalActor actor = allyTacticalActorBase as TacticalActor;
                                            float magnitude = 10;

                                            if (leader.IsAlive)
                                            {
                                                bool enemiesNear = false;
                                                foreach (TacticalFaction faction2 in controller.Factions)
                                                {
                                                    if (faction2.GetRelationTo(faction) == PhoenixPoint.Common.Core.FactionRelation.Enemy)
                                                    {
                                                        foreach (TacticalActorBase enemy in faction2.Actors)
                                                        {
                                                            if (enemy.IsAlive && enemy.BaseDef.name == "Soldier_ActorDef" && (allyTacticalActorBase.Pos - enemy.Pos).magnitude < magnitude)
                                                            {
                                                                enemiesNear = true;
                                                            }

                                                        }
                                                    }
                                                }

                                                if (!enemiesNear)
                                                {
                                                    TFTVLogger.Always("Leader is alive and no enemies in range, " + actor.name + " got the ambush ability");
                                                    actor.AddAbility(ambush, actor);
                                                }
                                                else
                                                {
                                                    if (actor.GetAbilityWithDef<Ability>(ambush) != null)
                                                    {
                                                        TFTVLogger.Always("Leader is alive, but there are enemies in range, " + actor.name + " lost the ambush ability");
                                                        actor.RemoveAbility(ambush);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                if (actor.GetAbilityWithDef<Ability>(ambush) != null)
                                                {
                                                    TFTVLogger.Always("Leader is dead, " + actor.name + " lost the ambush ability");
                                                    actor.RemoveAbility(ambush);
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        [HarmonyPatch(typeof(TacticalLevelController), "ActorDied")]
        public static class TacticalLevelController_ActorDied_HumanEnemiesTactics_BloodRush_Patch
        {
            public static void Postfix(DeathReport deathReport)
            {
                try
                {
                    if (HumanEnemiesAndTactics.ContainsKey(deathReport.Actor.TacticalFaction.Faction.FactionDef.ShortName)
                        && HumanEnemiesAndTactics.GetValueSafe(deathReport.Actor.TacticalFaction.Faction.FactionDef.ShortName) == 6)
                    {


                        if (deathReport.Actor.HasGameTag(HumanEnemyTier2GameTag) || deathReport.Actor.HasGameTag(HumanEnemyTier1GameTag))
                        {
                            foreach (TacticalActorBase allyTacticalActorBase in deathReport.Actor.TacticalFaction.Actors)
                            {
                                TacticalActor tacticalActor = allyTacticalActorBase as TacticalActor;

                                if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay
                                    && !tacticalActor.HasStatus(frenzy))
                                {
                                    allyTacticalActorBase.Status.ApplyStatus(frenzy);

                                }

                            }

                        }
                    }

                }
                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }
            }
        }

        [HarmonyPatch(typeof(TacticalLevelController), "ActorDamageDealt")]
        public static class TacticalLevelController_ActorDamageDealt_HumanEnemiesTactics_Retribution_Patch
        {
            public static void Postfix(TacticalActor actor, IDamageDealer damageDealer)
            {
                try
                {

                    if (actor.HasGameTag(HumanEnemyTier1GameTag) && damageDealer != null && HumanEnemiesAndTactics.ContainsKey(actor.TacticalFaction.Faction.FactionDef.ShortName)
                    && HumanEnemiesAndTactics.GetValueSafe(actor.TacticalFaction.Faction.FactionDef.ShortName) == 7)
                    {
                        TacticalActorBase attackerBase = damageDealer.GetTacticalActorBase();
                        TacticalActor attacker = attackerBase as TacticalActor;

                        if (!attacker.Status.HasStatus(mFDStatus) && actor.TacticalFaction != attacker.TacticalFaction)
                        {
                            attacker.Status.ApplyStatus(mFDStatus);

                        }

                    }
                }
                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }
            }
        }

        public static void TestingAura(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);

                foreach (TacticalFaction faction in enemyHumanFactions)
                {
                    foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                    {
                        TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                        if (tacticalActorBase.HasGameTag(DefCache.GetDef<GameTagDef>("HumanEnemyTier_1_GameTagDef")))
                        {
                            foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                            {
                                // TFTVLogger.Always("Ally pos " + allyTacticalActorBase.Pos);
                                //   TFTVLogger.Always("Actor pos " + tacticalActor.Pos);
                                // TFTVLogger.Always("ActorBase pos " + tacticalActorBase.Pos);
                                float magnitude = 24;

                                if ((allyTacticalActorBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                    && allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay
                                    && TacticalFactionVision.CheckVisibleLineBetweenActors(allyTacticalActorBase, allyTacticalActorBase.Pos, tacticalActor, true))
                                {
                                    TFTVLogger.Always("Actor in range and has LoS");
                                    ItemSlotStatsModifyStatusDef eRStatusEffect = DefCache.GetDef<ItemSlotStatsModifyStatusDef>("E_Status [ElectricReinforcement_AbilityDef]");
                                    allyTacticalActorBase.Status.ApplyStatus(eRStatusEffect);
                                }

                            }
                        }

                    }
                }

            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }


        [HarmonyPatch(typeof(TacticalFaction), "RequestEndTurn")]
        public static class TacticalFaction_RequestEndTurn_StartingVolleyTactic_Patch
        {
            public static void Prefix(TacticalFaction __instance)
            {
                try
                {
                    if (HumanEnemiesAndTactics.ContainsKey(__instance.Faction.FactionDef.ShortName)
                        && HumanEnemiesAndTactics.GetValueSafe(__instance.Faction.FactionDef.ShortName) == 2
                        && __instance.TacticalLevel.TurnNumber > 0 && __instance.TacticalLevel.GetFactionByCommandName("PX") == __instance)
                    {
                        StartingVolley(__instance.TacticalLevel, __instance.Faction.FactionDef.ShortName);
                    }
                }
                catch (Exception e)
                {
                    TFTVLogger.Error(e);
                }
            }
        }
    }
}
