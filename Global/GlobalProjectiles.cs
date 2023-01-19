using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using System.IO;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityAmmo.Accessories;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.EntitySources;
using CalamityMod;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod.NPCs.Bumblebirb;
using CalamityAmmo.Ammos.Post_MoonLord;
using System;
using Terraria.Localization;
using Terraria.Audio;
using CalamityMod.Projectiles.Rogue;
using CalamityAmmo.Rockets;


namespace CalamityAmmo.Global
{
    // Here is a class dedicated to showcasing Send/ReceiveExtraAI()
    public class GlobalProjectiles : GlobalProjectile
    {
        
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            
        }
        public override bool PreAI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (modplayer.Arcane2 && (projectile.arrow|| projectile.type == ModContent.ProjectileType<MirageArrow_Proj>()
                || projectile.type == ModContent.ProjectileType<MirageArrow_Proj2>())
                &&projectile.type!= ModContent.ProjectileType<ArcaneArrow_Proj>()
                && projectile.type != 631)
            {
                projectile.penetrate = 0;
                projectile.active = false;
                return false;
            }
            return true;
        }
        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (modplayer.Radio && player.heldProj != projectile.whoAmI)
            {
                foreach (var proj in Main.projectile)
                    if (proj.type != ModContent.ProjectileType<TeslaAura>() && proj.CountsAsClass<RangedDamageClass>())
                    {
                        int Proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), proj.Center, proj.velocity, ModContent.ProjectileType<TeslaAura>(), (int)(projectile.damage * 0.2f), 0f, player.whoAmI);
                        Main.projectile[Proj].Center = proj.Center;
                        Main.projectile[Proj].velocity = proj.velocity;
                        Main.projectile[Proj].timeLeft = 1;
                    }
            }
            if (player.HeldItem.type == ModContent.ItemType<BeenadeLauncher>() || player.HeldItem.type == ModContent.ItemType<PlaguenadeLauncher>())
            {
                if (projectile.type == ModContent.ProjectileType<PlaguenadeProj>() && projectile.owner == Main.myPlayer)
                {
                    projectile.timeLeft = 300;
                    projectile.DamageType = DamageClass.Ranged;
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                        {
                            float distanceFromTarget = projectile.Center.ManhattanDistance(Main.npc[i].Center);
                            float Linghuodistance = 128f;
                            if (distanceFromTarget < Linghuodistance)
                            {
                                if (projectile.owner == Main.myPlayer)
                                {
                                }
                                SoundEngine.PlaySound(SoundID.Item14, new Vector2?(projectile.position));
                                projectile.Kill();
                                //Main.NewText(projectile.timeLeft);
                            }
                        }

                        //Main.NewText(projectile.timeLeft);
                    }
                }
            }
        }
        public override void ModifyDamageScaling(Projectile projectile, ref float damageScale)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (player.HasBuff(94))
            {
                player.GetDamage<RangedDamageClass>() *= 0.5f;
            }
            if (projectile.arrow && modplayer.Arcane)
            {
                damageScale = 1.05f + player.HeldItem.useTime / 420f;
            }
           
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            
            if (modplayer.Coil && projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<MidasCoin>())
            {
                projectile.velocity *= 1.08f;
            }
            if (modplayer.Coil2)
            {
                if (projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<MidasCoin>())
                {
                    projectile.velocity *= 1.2f;
                }
                if (Main.rand.NextBool(144))
                {
                    player.AddBuff(144, 45);
                }
            }
            if ((modplayer.Coil3||modplayer.Coil4) && projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<MidasCoin>())
            {
                projectile.velocity *= 1.35f;
            }
          

            if (modplayer.Radio && player.heldProj != projectile.whoAmI)
            {

                if (projectile.type != ModContent.ProjectileType<TeslaAura>() && projectile.CountsAsClass<RangedDamageClass>())
                {
                    Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<TeslaAura>(), (int)(projectile.damage * 0.2f), 0f, player.whoAmI);

                }

            }
            if (modplayer.Arcane2 && (projectile.arrow //|| projectile.type == ModContent.ProjectileType<MirageArrow_Proj>()
                || projectile.type == ModContent.ProjectileType<MirageArrow_Proj2>())
                && projectile.type != ModContent.ProjectileType<ArcaneArrow_Proj>()
                && projectile.type != 631
                && player.HeldItem.type!= 4953
                 && player.HeldItem.type != 3540)
            {
                projectile.penetrate = 0;
                projectile.active = false;
                int damage = projectile.damage;
                if ((float)player.statMana / (float)player.statManaMax2 >= 0.8)
                {
                    damage = projectile.damage + (int)player.GetDamage<RangedDamageClass>().ApplyTo(15f);
                }
                //Main.NewText((float)player.statMana / (float)player.statManaMax2);
               int proj= Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<ArcaneArrow_Proj>(), damage, projectile.knockBack, player.whoAmI);
                Main.projectile[proj].CritChance =(int) player.GetCritChance<RangedDamageClass>();
                
                  
            }
           
        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();

        }
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            /*if (modplayer.Arcane)
            {
                if (!target.boss&&target.type!=NPCID.Frog)
                {
                    target.active = false;
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                   {
                        NPC.NewNPC(Projectile.GetSource_NaturalSpawn(), (int)target.Center.X, (int)target .Center.Y-20,
                            NPCID.Frog,0,0,0,(int)target.whoAmI);
                        
                    }
                }*/
                if (modplayer.Coil2 && Main.rand.NextBool(100))
                {
                    target.AddBuff(144, 45);
                }
            }
        }
    }
namespace CalamityAmmo.Global
{
    
}


				

		




