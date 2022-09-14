﻿using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Statuses;
using HarmonyLib;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Statuses;
using PhoenixPoint.Tactical.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TFTV
{
    internal class TFTVHumansEnemiesTactics
    {
        public static int roll = 0;

        private static readonly DefRepository Repo = TFTVMain.Repo;
        public static void ChampRecoverWPAura(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_2_GameTagDef"))))
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

        public static void RollTactic()
        {
            try
            {
                UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
                roll = UnityEngine.Random.Range(1, 9);

            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }





        public static void ApplyTactic(TacticalLevelController controller, int roll)
        {
            try
            {

                if (roll == 1)
                {
                    TerrifyingAura(controller);
                }
                else if (roll == 2)
                {
                    StartingVolley(controller);
                }
                else if (roll == 3)
                {
                    ExperimentalDrugs(controller);
                }
                else if (roll == 4)
                {
                    OpticalShield(controller);

                }
                else if (roll == 5)
                {
                    FireDiscipline(controller);
                }
                else if (roll == 6)
                {

                }
                else if (roll == 7)
                {

                }
                else if (roll == 8)
                {
                    Ambush(controller);

                }
                else if (roll == 9)
                {
                    AssistedTargeting(controller);

                }


            }
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void TerrifyingAura(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                TacticalFaction phoenix = controller.GetFactionByCommandName("PX");
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
                            {
                                foreach (TacticalActorBase phoenixSoldierBase in phoenix.Actors)
                                {
                                    if (phoenixSoldierBase.BaseDef.name == "Soldier_ActorDef" && phoenixSoldierBase.InPlay)
                                    {
                                        TacticalActor actor = phoenixSoldierBase as TacticalActor;
                                        float magnitude = actor.GetAdjustedPerceptionValue();

                                        if ((phoenixSoldierBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                            && TacticalFactionVision.CheckVisibleLineBetweenActors(phoenixSoldierBase, phoenixSoldierBase.Pos, tacticalActor, true))
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
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void ExperimentalDrugs(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
                            {
                                foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                {
                                    if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay && allyTacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_4_GameTagDef"))))
                                    {


                                        TacticalActor actor = allyTacticalActorBase as TacticalActor;

                                        TFTVLogger.Always("Actor is getting the experimental drugs");
                                        actor.AddAbility(Repo.GetAllDefs<AbilityDef>().FirstOrDefault(p => p.name.Equals("Regeneration_Torso_Passive_AbilityDef")), actor);
                                        actor.Status.ApplyStatus(Repo.GetAllDefs<HealthChangeStatusDef>().FirstOrDefault(p => p.name.Equals("Regeneration_Torso_Constant_StatusDef")));

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

        public static void StartingVolley(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
                            {

                                foreach (TacticalActorBase allyTacticalActorBase in faction.Actors)
                                {
                                    if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                    {
                                        if (tacticalActor.IsAlive)
                                        {
                                            TacticalActor actor = allyTacticalActorBase as TacticalActor;

                                            TFTVLogger.Always("Actor is getting quick aim status");
                                            //  actor.AddAbility(Repo.GetAllDefs<AbilityDef>().FirstOrDefault(p => p.name.Equals("Regeneration_Torso_Passive_AbilityDef")), actor);
                                            actor.Status.ApplyStatus(Repo.GetAllDefs<AddAttackBoostStatusDef>().FirstOrDefault(p => p.name.Equals("E_Status [QuickAim_AbilityDef]")));
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

        public static void OpticalShield(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor leader = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
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

                                            if ((allyTacticalActorBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                                && !actor.CharacterStats.Stealth.Modifications.Contains(stealthBuff))
                                            {
                                                TFTVLogger.Always("Actor in range, optical shield");
                                                actor.CharacterStats.Stealth.AddStatModification(stealthBuff);
                                            }
                                            else if (actor.CharacterStats.Stealth.Modifications.Contains(stealthBuff) && actor != leader)
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
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void AssistedTargeting(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor leader = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
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
                                            if(numberOAssists >= 4) 
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
                                            if (numberOAssists ==1)
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
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }



        public static void FireDiscipline(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor leader = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
                            {
                                // StatModification stealthBuff = new StatModification() { Modification = StatModificationType.Add, Value = 1, StatName = "Stealth" };

                                ReturnFireAbilityDef returnFire = Repo.GetAllDefs<ReturnFireAbilityDef>().FirstOrDefault(p => p.name.Equals("ReturnFire_AbilityDef"));
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
                                                (Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("Heavy_ClassTagDef"))) && (actor.LevelProgression.Level > 1 ||
                                                !actor.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_4_GameTagDef"))))))
                                            {
                                                actor.RemoveAbility(returnFire);
                                            }
                                        }
                                        else if (!leader.IsAlive && actor.GetAbilityWithDef<Ability>(returnFire) != null && (!actor.HasGameTag
                                                (Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("Heavy_ClassTagDef"))) && (actor.LevelProgression.Level > 1 ||
                                                !actor.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_4_GameTagDef"))))))
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
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }

        public static void Ambush(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor leader = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
                            {

                                PassiveModifierAbilityDef ambush = Repo.GetAllDefs<PassiveModifierAbilityDef>().FirstOrDefault(p => p.name.Equals("HumanEnemiesTacticsAmbush_AbilityDef"));
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
            catch (Exception e)
            {
                TFTVLogger.Error(e);
            }
        }


        [HarmonyPatch(typeof(TacticalLevelController), "ActorDied")]
        public static class TacticalLevelController_ActorDied_HumanEnemiesTactics_BloodRush_Patch
        {
            public static void Postfix(DeathReport deathReport, TacticalLevelController __instance)
            {
                try
                {
                    if (roll == 6)
                    {
                        GameTagDef champTag = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_2_GameTagDef"));
                        GameTagDef leaderTag = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"));

                        if (deathReport.Actor.HasGameTag(champTag) || deathReport.Actor.HasGameTag(leaderTag))
                        {
                            foreach (TacticalActorBase allyTacticalActorBase in deathReport.Actor.TacticalFaction.Actors)
                            {
                                if (allyTacticalActorBase.BaseDef.name == "Soldier_ActorDef" && allyTacticalActorBase.InPlay)
                                {
                                    allyTacticalActorBase.Status.ApplyStatus(Repo.GetAllDefs<StatusDef>().FirstOrDefault(sd => sd.name.Equals("Frenzy_StatusDef")));

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

                    if (roll == 7)
                    {
                        GameTagDef leaderTag = Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"));

                        if (actor.HasGameTag(leaderTag) && damageDealer != null)
                        {
                            TacticalActorBase attackerBase = damageDealer.GetTacticalActorBase();
                            TacticalActor attacker = attackerBase as TacticalActor;
                            attacker.Status.ApplyStatus(Repo.GetAllDefs<HitPenaltyStatusDef>().FirstOrDefault(p => p.name.Equals("E_PureDamageBonusStatus [MarkedForDeath_AbilityDef]")));
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

                        if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
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
                                    ItemSlotStatsModifyStatusDef eRStatusEffect = Repo.GetAllDefs<ItemSlotStatsModifyStatusDef>().FirstOrDefault(p => p.name.Equals("E_Status [ElectricReinforcement_AbilityDef]"));
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


        /*   public static void OverwhelmingAura(TacticalLevelController controller)
        {
            try
            {
                List<TacticalFaction> enemyHumanFactions = TFTVHumanEnemies.GetHumanEnemyFactions(controller);
                TacticalFaction phoenix = controller.GetFactionByCommandName("PX");
                if (enemyHumanFactions.Count != 0)
                {
                    foreach (TacticalFaction faction in enemyHumanFactions)
                    {
                        foreach (TacticalActorBase tacticalActorBase in faction.Actors)
                        {
                            TacticalActor tacticalActor = tacticalActorBase as TacticalActor;

                            if (tacticalActorBase.HasGameTag(Repo.GetAllDefs<GameTagDef>().FirstOrDefault(p => p.name.Equals("HumanEnemyTier_1_GameTagDef"))))
                            {
                                foreach (TacticalActorBase phoenixSoldierBase in phoenix.Actors)
                                {
                                    if (phoenixSoldierBase.BaseDef.name == "Soldier_ActorDef" && phoenixSoldierBase.InPlay)
                                    {
                                        TacticalActor actor = phoenixSoldierBase as TacticalActor;
                                        float magnitude = 10;

                                        if ((phoenixSoldierBase.Pos - tacticalActorBase.Pos).magnitude < magnitude
                                            && TacticalFactionVision.CheckVisibleLineBetweenActors(phoenixSoldierBase, phoenixSoldierBase.Pos, tacticalActor, true))
                                        {
                                            
                                               TFTVLogger.Always(actor.GetDisplayName() + " is within 10 tile range and has LoS on " + tacticalActor.name);
                                            actor.CharacterStats.SetStatValue(PhoenixPoint.Common.Entities.StatModificationTarget.ActionPoints, 0.5f);


                                            TFTVLogger.Always("Actor has " + actor.CharacterStats.ActionPoints.IntValue);
                                        
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
        }*/
    }
}