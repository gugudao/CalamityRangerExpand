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

namespace CalamityAmmo.Global
{
    // Here is a class dedicated to showcasing Send/ReceiveExtraAI()
    public class GlobalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

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
            if (modplayer.Coil3 && projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<MidasCoin>())
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
            if (modplayer.Arcane2 && (projectile.arrow || projectile.type == ModContent.ProjectileType<MirageArrow_Proj>()
                || projectile.type == ModContent.ProjectileType<MirageArrow_Proj2>())
                && projectile.type!= ModContent.ProjectileType<ArcaneArrow_Proj>()
                &&projectile.type != 631)
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
    public abstract class ThePackRockets : ModProjectile
    {
        
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor, 1, null, true);
            Projectile.rotation = Projectile.velocity.ToRotation() ;
            if (Projectile.frame < 12)
            {
                CalamityUtils.DrawAfterimagesCentered(Projectile, 2, lightColor, 1);
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/ThePackRockets").Value,
            Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 18, 82,18), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(41, 9), Projectile.scale,
            Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
               
            }
            else
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture).Value,
            Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 32, 40), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(16, 4), Projectile.scale,
            Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            }
            
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PR");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "PR");
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 1;
            Init();
        }
        public virtual void Init()
        {
        }
        public virtual void Inai()
        {
            //帧图！！
        }
        public override void AI()
        {
            Inai();
            Vector2 targetCenter = Projectile.Center;
            float minTargetDistance = 2500f;
            bool homeIn = false;
            Tile tile = Framing.GetTileSafely((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16));
            if(tile != null)
            {

            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float distanceFromTarget = Projectile.Center.ManhattanDistance(Main.npc[i].Center);
                    float Linghuodistance = 150f;
                    if(AIType==ProjectileID.RocketIII|| AIType == ProjectileID.RocketIV)
                    {
                        Linghuodistance = 200f;
                    }
                    if (AIType == ProjectileID.MiniNukeGrenadeI || AIType == ProjectileID.MiniNukeGrenadeII)
                    {
                        Linghuodistance = 250f;
                    }
                    if (distanceFromTarget < Linghuodistance)
                    {
                        if (Projectile.owner == Main.myPlayer)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                Vector2 velocity = Utils.NextVector2Unit(Main.rand, 0f, 6.28318548f) * Utils.NextFloat(Main.rand, 6f, 12f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<ThePackMinissile>(), (int)(Projectile.damage * 0.25), Projectile.knockBack, Projectile.owner, 0f, 0f);
                            }
                        }
                        SoundEngine.PlaySound( SoundID.Item14, new Vector2?(Projectile.position));
                        Projectile.Kill();
                        return;
                    }
                    if (distanceFromTarget < minTargetDistance)
                    {
                        minTargetDistance = distanceFromTarget;
                        targetCenter = Main.npc[i].Center;
                        homeIn = true;
                    }
                }
            }
            if (homeIn)
            {
                Projectile.velocity = (Projectile.velocity * 15f + Projectile.SafeDirectionTo(targetCenter, default(Vector2?)) * 30f) / 16f;
                return;
            }
           /* Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }*/
            if (Projectile.wet && (AIType == ProjectileID.WetRocket || AIType == ProjectileID.LavaRocket || AIType == ProjectileID.HoneyRocket || AIType == ProjectileID.DryRocket))
            {
                Projectile.penetrate = 0;
                return;
            }
            Projectile.ai[0]++;
            Projectile.alpha = Terraria.Utils.Clamp(255 - (int)Projectile.ai[0] * 15, 0, 255);
            if (Projectile.ai[0] < 20) return;
            if (Main.rand.Next(1, 4) <= 2)
            {
                int dust = Dust.NewDust(Projectile.position, 18, 18, 6);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;
            }
            else
            {
                int goreIndex = Dust.NewDust(Projectile.Center , 0, 0, 31);
                Main.dust[goreIndex].alpha = 66;
                Main.dust[goreIndex].velocity = Vector2.Normalize(Projectile.velocity).RotatedByRandom(Math.PI / 10) * -1f;
                Main.dust[goreIndex].velocity *= Main.rand.NextFloat(-1, 2);
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                Projectile.type = AIType;
                return true;
            }
            return false;
        }
        public override bool PreKill(int timeLeft)
        {
            Projectile.type = AIType;
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 2;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.width = 300;
            Projectile.height = 300;
            if (AIType == ProjectileID.RocketIII || AIType == ProjectileID.RocketIV)
            {
                Projectile.width = 400;
                Projectile.height = 400;
            }
            if (AIType == ProjectileID.MiniNukeRocketI || AIType == ProjectileID.MiniNukeRocketII)
            {
                Projectile.width = 500;
                Projectile.height = 500;
            }

            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            for (int num621 = 0; num621 < 40; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 255, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num622].velocity *= 3f;
                if (Utils.NextBool(Main.rand, 2))
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 60; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 255, 0f, 0f, 0, default(Color), 2f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 255, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num624].velocity *= 2f;
            }
            Projectile.Damage();
        }
    }
    internal class ThePack_RocketI : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketI;
            Projectile.frame = 0;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 1)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
          
        }
    }
    internal class ThePack_RocketII : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketII;
            Projectile.frame = 1;
        }
    }
    internal class ThePack_RocketIII : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketIII;
            Projectile.frame = 2;
        }
    }
    internal class ThePack_RocketIV : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketIV;
            Projectile.frame = 3;
        }
    }
    internal class ThePack_ClusterRocketI : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.ClusterRocketI;
            Projectile.frame = 4;
        }
    }
    internal class ThePack_ClusterRocketII : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.ClusterRocketII;
            Projectile.frame = 5;
        }
    }
    internal class ThePack_MiniNukeRocketI : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.MiniNukeRocketI;
            Projectile.frame = 6;
        }
    }
    internal class ThePack_MiniNukeRocketII : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.MiniNukeRocketII;
            Projectile.frame = 7;
        }
    }
    internal class ThePack_WetRocket : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.WetRocket;
            Projectile.frame = 8;
        }
    }
    internal class ThePack_LavaRocket : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.LavaRocket;
            Projectile.frame = 9;
        }
    }
    internal class ThePack_HoneyRocket : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.HoneyRocket;
            Projectile.frame = 10;
        }
    }
    internal class ThePack_DryRocket : ThePackRockets
    {
        public override string Texture => "ContinentOfJourney/Projectiles/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.DryRocket;
            Projectile.frame = 11;
        }
    }
}


				

		




