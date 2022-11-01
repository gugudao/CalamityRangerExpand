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

namespace CalamityAmmo.Global
{
    // Here is a class dedicated to showcasing Send/ReceiveExtraAI()
    public class GlobalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

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
                        Main.projectile[Proj].velocity= proj.velocity;
                        Main.projectile[Proj].timeLeft = 1;
                    }
            }

            //Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<TeslaAura>(), (int)(projectile.damage * 0.2f), 0f, player.whoAmI);

            base.AI(projectile);
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
                if (Main.rand.NextBool(150))
                {
                    player.AddBuff(144, 30);
                }
            }
            if (modplayer.Coil3 && projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<MidasCoin>())
            {
                projectile.velocity *= 1.35f;
            }
            if (modplayer.Spore && projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI 
                 && projectile.type != ModContent.ProjectileType<MidasCoin>()
                 && projectile.type != ModContent.ProjectileType<Spore1>()
                 && projectile.type != ModContent.ProjectileType<Spore2>()
                 && projectile.type != ModContent.ProjectileType<Spore3>()
                 && projectile.type != ModContent.ProjectileType<Crabulon_Spore>()
                 && projectile.type != ModContent.ProjectileType<FungiOrb>()
                 && projectile.type != ModContent.ProjectileType<FungiOrb2>())
            {
                if (Main.rand.NextBool(8))
                {
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<FungiOrb>(), (int)(projectile.damage * 0.5f), 0f, player.whoAmI);
                    }
                }
            }

            if (modplayer.Radio && player.heldProj != projectile.whoAmI)
            {
                
                    if (projectile.type != ModContent.ProjectileType<TeslaAura>() && projectile.CountsAsClass<RangedDamageClass>())
                    {
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<TeslaAura>(), (int)(projectile.damage * 0.2f), 0f, player.whoAmI);
                    
                    }

            }

        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
    
        }
    }
}
				

		




