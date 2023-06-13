using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{
	public class _AstralStar2 : ModProjectile
	{
		public int[] dustTypes = new int[]
   {
			ModContent.DustType<AstralBlue>(),
			ModContent.DustType<AstralOrange>()
   };

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Astral Star");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.alpha = 100;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.localNPCHitCooldown = 10;
			Projectile.usesLocalNPCImmunity = true;
		}

		public override void AI()
		{
			Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * Projectile.direction;
			Lighting.AddLight(Projectile.Center, 0.3f, 0.5f, 0.1f);
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 20 + Main.rand.Next(40);
				if (Utils.NextBool(Main.rand, 5))
				{
					SoundEngine.PlaySound(SoundID.Item9, new Vector2?(Projectile.position));
				}
			}
			float scaleAmt = Main.mouseTextColor / 200f - 0.35f;
			scaleAmt *= 0.2f;
			Projectile.scale = scaleAmt + 0.95f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 15f)
			{
				int astral = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Utils.Next<int>(Main.rand, this.dustTypes), 0f, 0f, 100, default(Color), 0.8f);
				Main.dust[astral].noGravity = true;
				Main.dust[astral].velocity *= 0f;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(200, 200, 200, Projectile.alpha));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180, false);
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180, true, false);
		}

		public override void Kill(int timeLeft)
		{
			if (Main.myPlayer == Projectile.owner)
			{
				SoundEngine.PlaySound(SoundID.Item9, new Vector2?(Projectile.position));
				Projectile.position = Projectile.Center;
				Projectile.width = (Projectile.height = 96);
				Projectile.position.X = Projectile.position.X - Projectile.width / 2;
				Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
				for (int d = 0; d < 2; d++)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Utils.Next<int>(Main.rand, this.dustTypes), 0f, 0f, 50, default(Color), 1f);
				}
				for (int d2 = 0; d2 < 20; d2++)
				{
					int astral = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Utils.Next<int>(Main.rand, this.dustTypes), 0f, 0f, 0, default(Color), 1.5f);
					Main.dust[astral].noGravity = true;
					Main.dust[astral].velocity *= 3f;
					astral = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 173, 0f, 0f, 50, default(Color), 1f);
					Main.dust[astral].velocity *= 2f;
					Main.dust[astral].noGravity = true;
				}
				if (Main.netMode != 2)
				{
					for (int g = 0; g < 3; g++)
					{
						Gore.NewGore(Projectile.GetSource_Death(null), Projectile.position, new Vector2(Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
					}
				}
			}
		}
	}
}

