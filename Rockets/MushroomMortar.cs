using System;
using System.Collections.Generic;
using CalamityAmmo.Global;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Tiles.AstralDesert;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Rockets

{
    public class MushroomMortar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Mortar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蘑菇迫击炮");
            Tooltip.SetDefault("Grow mushroom");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "种蘑菇\n" +"没做完呢急啥");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 106;
            Item.height = 35;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle?(SoundID.Item40);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Rocket;
            //Item.Calamity().canFirePointBlankShots = true;
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += 22f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2?(new Vector2(0f, 0f));
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 shootVel = velocity;
            int rockettype = 0;
            switch (source.AmmoItemIdUsed)
            {
                case ItemID.RocketI: rockettype = ModContent.ProjectileType<Mushroom_RocketI>(); break;
                case ItemID.RocketII: rockettype = ModContent.ProjectileType<Mushroom_RocketII>(); break;
                case ItemID.RocketIII: rockettype = ModContent.ProjectileType<Mushroom_RocketIII>(); break;
                case ItemID.RocketIV: rockettype = ModContent.ProjectileType<Mushroom_RocketIV>(); break;
                case ItemID.ClusterRocketI: rockettype = ModContent.ProjectileType<Mushroom_ClusterRocketI>(); break;
                case ItemID.ClusterRocketII: rockettype = ModContent.ProjectileType<Mushroom_ClusterRocketII>(); break;

                case ItemID.MiniNukeI: rockettype = ModContent.ProjectileType<Mushroom_MiniNukeRocketI>(); break;
                case ItemID.MiniNukeII: rockettype = ModContent.ProjectileType<Mushroom_MiniNukeRocketII>(); break;

                case ItemID.WetRocket: rockettype = ModContent.ProjectileType<Mushroom_WetRocket>(); break;
                case ItemID.LavaRocket: rockettype = ModContent.ProjectileType<Mushroom_LavaRocket>(); break;
                case ItemID.HoneyRocket: rockettype = ModContent.ProjectileType<Mushroom_HoneyRocket>(); break;
                case ItemID.DryRocket: rockettype = ModContent.ProjectileType<Mushroom_DryRocket>(); break;
            }
            Projectile.NewProjectile(source, position, shootVel, rockettype, damage, knockback, player.whoAmI);

            return false;

        }
        public abstract class MushroomRockets : ModProjectile
        {
            public override bool PreDraw(ref Color lightColor)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2;
                if (Projectile.frame < 12) Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/BulletBlasterRockets").Value,
                Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 40, 32, 40), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(16, 4), Projectile.scale,
                Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                else Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture).Value,
                Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 32, 40), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(16, 4), Projectile.scale,
                Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                return false;
            }
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Mushroom");
                DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蘑菇");
                Main.projFrames[Projectile.type] = 12;
            }
            public override void SetDefaults()
            {
                Projectile.width = 16;
                Projectile.height = 16;
                Projectile.friendly = true;
                Projectile.hostile = false;
                Projectile.penetrate = 1;
                Projectile.timeLeft = 2000;
                Projectile.DamageType = DamageClass.Ranged;
                Projectile.tileCollide = false;
                Projectile.ignoreWater = false;
                Projectile.usesIDStaticNPCImmunity = true;
                Projectile.idStaticNPCHitCooldown = 10;
                Init();
            }
            public virtual void Init()
            {
            }
            public override void AI()
            {
                Projectile.ai[0]++;
                Projectile.alpha = Utils.Clamp(255 - (int)Projectile.ai[0] * 25, 0, 255);
                Tile tile = Framing.GetTileSafely((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16));
                float maxSpeed = 2f;
                if (tile.HasTile && Main.tileSolid[tile.TileType])
                {
                    maxSpeed = 3f;

                    if (Main.tileSand[tile.TileType])
                    {
                        maxSpeed = 4f;
                    }
                    CalamityUtils.HomeInOnNPC(Projectile, true, 300f, 12f, 15f);
                }
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 2f * maxSpeed;
                Main.NewText(Projectile.velocity.Length());

                if (Main.rand.Next(1, 4) <= 2)
                {
                    int dust = Dust.NewDust(Projectile.position + new Vector2(5, 5) - 16 * (Projectile.rotation - (float)Math.PI / 2).ToRotationVector2(), 0, 0, 6);
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0f;
                }
                else
                {
                    int goreIndex = Dust.NewDust(Projectile.Center - new Vector2(3, 3), 0, 0, 31);
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
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.type = AIType;
                    Projectile.width = 144;
                    Projectile.height = 144;
                    if (AIType == ProjectileID.RocketIII || AIType == ProjectileID.RocketIV)
                    {
                        Projectile.width = 272;
                        Projectile.height = 272;
                    }
                    if (AIType == ProjectileID.MiniNukeRocketI || AIType == ProjectileID.MiniNukeRocketII)
                    {
                        Projectile.width = 384;
                        Projectile.height = 384;
                    }
                    Projectile.position.X = Projectile.position.X - Projectile.width / 2;
                    Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
                    Projectile.Damage();
                    Player player = Main.player[Projectile.owner];
                    if (Projectile.Hitbox.Intersects(player.Hitbox))
                    {
                        player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), Projectile.damage, Math.Sign(player.Center.X - Projectile.Center.X));
                    }
                }
                return;
            }
        }
        internal class Mushroom_RocketI : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.RocketI;
                Projectile.frame = 0;
            }
        }
        internal class Mushroom_RocketII : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.RocketII;
                Projectile.frame = 1;
            }
        }
        internal class Mushroom_RocketIII : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.RocketIII;
                Projectile.frame = 2;
            }
        }
        internal class Mushroom_RocketIV : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.RocketIV;
                Projectile.frame = 3;
            }
        }
        internal class Mushroom_ClusterRocketI : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.ClusterRocketI;
                Projectile.frame = 4;
            }
        }
        internal class Mushroom_ClusterRocketII : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.ClusterRocketII;
                Projectile.frame = 5;
            }
        }
        internal class Mushroom_MiniNukeRocketI : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.MiniNukeRocketI;
                Projectile.frame = 6;
            }
        }
        internal class Mushroom_MiniNukeRocketII : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.MiniNukeRocketII;
                Projectile.frame = 7;
            }
        }
        internal class Mushroom_WetRocket : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.WetRocket;
                Projectile.frame = 8;
            }
        }
        internal class Mushroom_LavaRocket : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.LavaRocket;
                Projectile.frame = 9;
            }
        }
        internal class Mushroom_HoneyRocket : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.HoneyRocket;
                Projectile.frame = 10;
            }
        }
        internal class Mushroom_DryRocket : MushroomRockets
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void Init()
            {
                AIType = ProjectileID.DryRocket;
                Projectile.frame = 11;
            }
        }
    }
}




