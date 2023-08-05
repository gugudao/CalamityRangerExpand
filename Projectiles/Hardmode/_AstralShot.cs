﻿using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{
    public class _AstralShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Astral Laser");
            Main.projFrames[base.Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 10;
            base.Projectile.height = 10;
            base.Projectile.friendly = true;
            base.Projectile.ignoreWater = true;
            base.Projectile.tileCollide = true ;
            base.Projectile.alpha = 255;
            base.Projectile.penetrate = 1;
            base.Projectile.timeLeft = 450;
        }

        public override void AI()
        {
            if (base.Projectile.ai[0] == 1f)
            {
                base.Projectile.extraUpdates = 2;
            }
            else if (Vector2.Distance(new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]), base.Projectile.Center) < 80f)
            {
                base.Projectile.tileCollide = true;
            }
            base.Projectile.frameCounter++;
            if (base.Projectile.frameCounter > 4)
            {
                base.Projectile.frame++;
                base.Projectile.frameCounter = 0;
            }
            if (base.Projectile.frame > 3)
            {
                base.Projectile.frame = 0;
            }
            if (base.Projectile.alpha > 0)
            {
                base.Projectile.alpha -= 25;
            }
            if (base.Projectile.alpha < 0&&Projectile.timeLeft<50)
            {
                base.Projectile.alpha = 0;
                Projectile.alpha += 25;
            }

            base.Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y,Projectile.velocity.X) + 1.57079637f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
            
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(new Color(255, 255, 255, base.Projectile.alpha));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor, 1, null, true);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, new Vector2?(base.Projectile.position));
            base.Projectile.position = base.Projectile.Center;
            base.Projectile.width = (base.Projectile.height = 10);
            base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
            base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
            for (int num621 = 0; num621 < 5; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num622].velocity *= 3f;
                if (Utils.NextBool(Main.rand, 2))
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 10; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.7f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 1.5f;
                Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1f);
            }
            base.Kill(timeLeft);
        }
    }
}
