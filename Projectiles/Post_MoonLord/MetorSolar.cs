﻿using CalamityMod.Buffs.DamageOverTime;
using CalamityMod;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{
    public class MetorSolar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            // DisplayName.SetDefault("Solar Fireball");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Cолнечного огненный шар");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "日耀火球");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40; 
            Projectile.height = 40; 
            Projectile.friendly = true; 
            Projectile.DamageType = DamageClass.Ranged; 
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false; 
            Projectile.penetrate = 1; 
            Projectile.alpha = 0;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
            // Set both direction and spriteDirection to 1 or -1 (right and left respectively)
            // Projectile.direction is automatically set correctly in Projectile.Update, but we need to set it here or the textures will draw incorrectly on the 1st frame.
            // Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
                // For vertical sprites use MathHelper.PiOver2
            }
            for (int num246 = 0; num246 < 2; num246++)
            {
                float num247 = 0f;
                float num248 = 0f;
                if (num246 == 1)
                {
                    num247 = base.Projectile.velocity.X * 0.5f;
                    num248 = base.Projectile.velocity.Y * 0.5f;
                }
                int num249 = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + num247, base.Projectile.position.Y + 3f + num248) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num249].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                Main.dust[num249].velocity *= 0.2f;
                Main.dust[num249].noGravity = true;
            }
        }
        // Some advanced drawing because the texture image isn't centered or symetrical
        // If you dont want to manually drawing you can use vanilla projectile rendering offsets
        // Here you can check it https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#horizontal-sprite-example
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
             float offsetY = 20f;
            origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item89, new Vector2?(base.Projectile.position));
            base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
            base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
            base.Projectile.width = (int)(128f * base.Projectile.scale);
            base.Projectile.height = (int)(128f * base.Projectile.scale);
            base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
            base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
            for (int num336 = 0; num336 < 8; num336++)
            {
                Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 1.5f);
            }
            for (int num337 = 0; num337 < 32; num337++)
            {
                int num338 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 2.5f);
                Main.dust[num338].noGravity = true;
                Main.dust[num338].velocity *= 3f;
                num338 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, new Color(255, Main.DiscoG, 0), 1.5f);
                Main.dust[num338].velocity *= 2f;
                Main.dust[num338].noGravity = true;
            }
            if (Main.netMode != 2)
            {
                for (int num339 = 0; num339 < 2; num339++)
                {
                    int num340 = Gore.NewGore(base.Projectile.GetSource_Death(null), base.Projectile.position + new Vector2((float)(base.Projectile.width * Main.rand.Next(100)) / 100f, (float)(base.Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                    Gore gore = Main.gore[num340];
                    gore.velocity *= 0.3f;
                    gore.velocity.X = gore.velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
                    gore.velocity.Y = gore.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
                }
            }
            if (base.Projectile.owner == Main.myPlayer)
            {
                base.Projectile.localAI[1] = -1f;
                base.Projectile.maxPenetrate = 0;
                base.Projectile.Damage();
            }
            for (int num341 = 0; num341 < 5; num341++)
            {
                int num342 = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                    244,
                    259,
                    158
                });
                int num343 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, num342, 2.5f * (float)base.Projectile.direction, -2.5f, 0, new Color(255, Main.DiscoG, 0), 1f);
                Main.dust[num343].alpha = 200;
                Main.dust[num343].velocity *= 2.4f;
                Main.dust[num343].scale += Utils.NextFloat(Main.rand);
            }
        }
    }
}
