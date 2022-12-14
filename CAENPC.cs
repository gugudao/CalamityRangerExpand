using System;
using System.ComponentModel;
using CalamityAmmo.Accessories;
using CalamityAmmo.Ammos.Hardmode;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityAmmo.Projectiles.Post_MoonLord;
using CalamityAmmo.Rockets;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using CsvHelper.TypeConversion;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace CalamityAmmo
{
    public class CAENPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int carrot = 0;
        public int GrapeTime = 0;
        
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (projectile.type == ModContent.ProjectileType<_DazzlingAstralArrow>() &&
               Main.myPlayer == projectile.owner)
            {
                Vector2 spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                int star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center + npc.velocity * (600 / 24) - spawnPos) * 24f, ProjectileID.StarCannonStar, (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center + npc.velocity * (600 / 24) - spawnPos) * 24f, ProjectileID.HallowStar, (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<AstralStar>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<AncientStar>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<FallenStarProj>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<HadalUrnStarfish>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<LeonidStar>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<FakeSeaStar1>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
                spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<FakeSeaStar2>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                Main.projectile[star].usesIDStaticNPCImmunity = false;
                Main.projectile[star].usesLocalNPCImmunity = true;
                Main.projectile[star].localNPCHitCooldown = 21;
                Main.projectile[star].netUpdate = true;
            }
            
            if (npc.type != NPCID.TargetDummy && Main.myPlayer == projectile.owner && projectile.type == ModContent.ProjectileType<_Seed>()
                && player.ownedProjectileCounts[ModContent.ProjectileType<_Flower>()] <= 0)
            {
                Vector2 spawnPos = npc.Center - new Vector2(0, npc.height / 2);
                int proj = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, npc.velocity * 8, ModContent.ProjectileType<_Flower>(), projectile.damage, projectile.knockBack, Main.myPlayer);
                Main.projectile[proj].netUpdate = true;
            }
            if (Main.myPlayer == projectile.owner && projectile.type == ModContent.ProjectileType<_ElementalArrow>())
             {
                carrot++;
                if (carrot > 4) carrot = 1;
                
                if (carrot>=4)
                {
                    int select = Main.rand.Next(0, 3);
                    if (select == 0)
                    {
                        Vector2 spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 3);
                        int solar = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 14f, ModContent.ProjectileType<MetorSolar>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                        Main.projectile[solar].usesIDStaticNPCImmunity = false;
                        Main.projectile[solar].usesLocalNPCImmunity = true;
                    }
                    if(select  == 1)
                    {
                        Vector2 spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 6);
                        int vortex = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<LightningVortex>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                        Main.projectile[vortex].usesIDStaticNPCImmunity = false;
                        Main.projectile[vortex].usesLocalNPCImmunity = true;
                    }
                     if (select == 2&& player.ownedProjectileCounts[ModContent.ProjectileType<HealingStarcloud>()] <= 0)
                    {
                        Vector2 spawnPos = player.Center - new Vector2(0, 200);
                        int nebula = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<HealingStarcloud>(), (int)(projectile.damage * 0.6f), projectile.knockBack, Main.myPlayer);
                        Main.projectile[nebula].usesIDStaticNPCImmunity = false;
                        Main.projectile[nebula].usesLocalNPCImmunity = true;
                    }
                }
             }
            if(modplayer.Grape&&projectile.DamageType==DamageClass.Ranged &&projectile.owner==Main.myPlayer)
            {
                GrapeTime += 1;
                Main.NewText(GrapeTime);
                if(GrapeTime>=7)
                {
                    GrapeTime = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 vec = Main.MouseWorld - player.Center;
                        vec = Vector2.Normalize(vec);
                        Vector2 finalVec = vec * 16f + new Vector2(Main.rand.NextFloatDirection() * 5f,
                            Main.rand.NextFloatDirection() * 5f);
                        int solar = Projectile.NewProjectile(npc.GetSource_FromAI(),
                            player.Center, 
                            finalVec, projectile.type, 
                            (int)(projectile.damage * 0.6f), 
                            projectile.knockBack, Main.myPlayer);
                        Main.projectile[solar].usesIDStaticNPCImmunity = false;
                        Main.projectile[solar].usesLocalNPCImmunity = true;
                        Main.projectile[solar].DamageType=DamageClass.Generic;
                    }
                    
                }
            }
         }
    }
    public class NPCLoot:GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if(npc.type==ModContent.NPCType<WulfrumDrone>()||
                npc.type == ModContent.NPCType<WulfrumGyrator>()||
                npc.type == ModContent.NPCType<WulfrumHovercraft>()||
                npc.type == ModContent.NPCType<WulfrumRover>()||
                npc.type == ModContent.NPCType<WulfrumAmplifier>())
            {
                npcLoot.Add(ModContent.ItemType<WulfrumCoil>(), new Fraction(7, 100));
            }
            if (npc.type== ModContent.NPCType<Crabulon>())
            {
                LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfectedCrabGill>(),5));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MarvelousMycelium>(), 5));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MushroomMortar>(), 4));
                npcLoot.Add(notExpertRule);
            }
            if (npc.type == ModContent.NPCType<DesertScourgeHead>())
            {
                LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SandWorm>(), 4));
                npcLoot.Add(notExpertRule);
            }
            if(npc.type == ModContent.NPCType<DesertNuisanceHead>())
            {
                npcLoot.Add(ModContent.ItemType<SandWorm>(), new Fraction(5, 100));
            }
            if(npc.type==NPCID.QueenBee)
            {
                LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BeenadeLauncher>(), 4));
                npcLoot.Add(notExpertRule);
            }
        }
    }
    public class ArmsDealer : GlobalNPC
    {
        public override bool AppliesToEntity(NPC npc, bool lateInstatiation)
        {
            return npc.type == NPCID.ArmsDealer;
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.ArmsDealer)
            {
                if (DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<FastHolster>());
                    nextSlot++;
                }
            }
        }
        public class Demolitionist : GlobalNPC
        {
            public override bool AppliesToEntity(NPC npc, bool lateInstatiation)
            {
                return npc.type == NPCID.Demolitionist;
            }
            public override void SetupShop(int type, Chest shop, ref int nextSlot)
            {
                if (type == NPCID.Demolitionist)
                {
                    if (DownedBossSystem.downedDesertScourge)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<SandRocket>());
                        nextSlot++;
                    }
                    if (DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Aerocket>());
                        nextSlot++;
                    }
                }
            }
        }
    }


    }
 

