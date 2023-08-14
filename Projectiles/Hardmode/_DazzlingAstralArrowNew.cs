﻿using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{

	public class _DazzlingAstralArrowNew : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dazzling Astral Arrow");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.arrow = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.extraUpdates = 2;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[Projectile.owner];

			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);

			Vector2 vector5 = new(player.position.X + player.width * 0.5f, player.position.Y + player.height / 2);
			float num49 = 16f;
			float num50 = target.position.X + target.width * 0.5f - vector5.X;
			float num51 = target.position.Y + target.height * 0.5f - vector5.Y;
			float num52 = (float)Math.Sqrt((double)(num50 * num50 + num51 * num51));
			num52 = num49 / num52;
			num50 *= num52;
			num51 *= num52;
			vector5.X += num50 * 7f;
			vector5.Y += num51 * 7f;
			Vector2 shootDirection = Utils.SafeNormalize(new Vector2(num50, num51), Vector2.UnitY);
			Vector2 laserVelocity = shootDirection * num49;
			int type2 = ModContent.ProjectileType<AstralGodRay>();
			int damage2 = Projectile.damage;

			float waveSideOffset = Utils.NextFloat(Main.rand, 9f, 14f);
			Vector2 perp = Utils.RotatedBy(shootDirection, -1.5707963705062866, default(Vector2)) * waveSideOffset;
			for (int i2 = -1; i2 <= 1; i2 += 2)
			{
				Vector2 laserStartPos = vector5 + i2 * perp + Utils.NextVector2CircularEdge(Main.rand, 6f, 6f);
				Projectile p = Projectile.NewProjectileDirect(target.GetSource_FromAI(null), laserStartPos, laserVelocity * 1.1f, type2, damage2, 0f, player.whoAmI, player.Center.X, player.Center.Y);
				p.localAI[1] = i2 * 0.5f;
				p.friendly = true;
				p.CritChance = Projectile.CritChance;
				p.hostile = false;

			}

		}
		public override bool PreAI()
		{
			Projectile.ai[0]++;
			Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f && Utils.NextBool(Main.rand, 2))
			{
				int randomDust = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					ModContent.DustType<AstralOrange>(),
					ModContent.DustType<AstralBlue>()
				});
				int astral = Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
				Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
				Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
				Main.dust[astral].alpha = Projectile.alpha;
				Vector2 Normal = Projectile.velocity.RotatedBy(Math.PI / 2);
				Normal.Normalize();
				Main.dust[astral].velocity += Normal * (float)Math.Sin(Projectile.ai[0]);
				Main.dust[astral].noGravity = true;
			}


			return false;
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{

		}
		public override void Kill(int timeLeft)
		{
			Vector2 projSpawn = new(Projectile.Center.X + Main.rand.Next(144) - Main.rand.Next(144), Projectile.Center.Y - 600);
			int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), projSpawn, Vector2.Normalize(Projectile.Center - projSpawn) * 18,
												ModContent.ProjectileType<_AstralStar>(), (int)(Projectile.damage * 0.5), 2, 0);
			Main.projectile[proj].netUpdate = true;
			base.Kill(timeLeft);
		}

	}
}




