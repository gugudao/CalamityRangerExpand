using System;

using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using ReLogic.Content;
using Terraria.GameContent;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Materials;
using CalamityAmmo.Projectiles;
using CalamityMod.Items.Placeables;
using Terraria.Audio;
using CalamityMod.Items;
using CalamityMod;
using CalamityAmmo.Ammos.Post_MoonLord;
using CalamityMod.Items.Weapons.Ranged;

namespace CalamityAmmo.Accessories
{
    public class ArcaneQuiver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "奥术箭袋");
            Tooltip.SetDefault("Allows you consume mana to enchant arrows and increase damage when holding a bow or repeater, \n" +
                "Mana usage and damage scale are based on usetime of the weapon\n" +
                "Mana Sickness also reduces player's ranged damage\n" +
                "greatly increases arrow speed\n20% chance to not consume arrows\n" +
                "Energy, power. My people are addicted to it!");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),
                "使用弓弩时可消耗魔力为箭矢附魔，提升伤害\n" +
                "魔力消耗与伤害增幅取决于武器的攻速\n" +
                "打开饰品可见性将把任何种类的箭矢替换为追踪的奥术箭\n" +
                "法力充盈时，奥术箭的伤害更高\n" +
                "箭的速度大大提高\n20%的几率不消耗箭\n" +
                "魔力病也会影响远程伤害\n" +
                "魔法，能量。我的人民陷入其中不能自拔!") ;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 65, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Arcane = true;
            if(!hideVisual)
            {
                modplayer.Arcane2 = true;
            }
            player.magicQuiver = true;
        }
        /*public override void AddRecipes()
        {
            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.MagicQuiver, 1);
            recipe2.AddIngredient(ItemID.ArcaneFlower, 1);
            recipe2.AddIngredient(ItemID.ArcaneFlower, 5);
            recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.StalkersQuiver, 1);
            recipe2.AddIngredient(ItemID.ManaFlower, 1);
            recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
            recipe2.AddIngredient(ItemID.ArcaneFlower, 5);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
        }*/
    }
    public class ArcaneArrow_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arcane Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 360;
            Projectile.light = 0f;
            Projectile.extraUpdates = 1;
            Projectile.arrow = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {

        }
        public override void ModifyDamageScaling(ref float damageScale)
        {
            Player player = Main.player[Projectile.owner];
          
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            //Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            if (Main.rand.Next(9) == 0)
            {
                int num9 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num9].velocity *= 0.3f;
                Main.dust[num9].position.X = Projectile.position.X + (float)(Projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
                Main.dust[num9].position.Y = Projectile.position.Y + (float)(Projectile.height / 2) + (float)Main.rand.Next(-4, 5);
                Main.dust[num9].noGravity = true;
                Main.dust[num9].velocity += Main.rand.NextVector2Circular(2f, 2f);
            }
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, Projectile.ai[0], 12f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
        public override void Kill(int timeLeft)
        {
            
                int width2 = Projectile.width;
                int height2 = Projectile.height;
                Projectile.Resize(128, 128);
                Projectile.Resize(width2, height2);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
                Vector2 target4 = Projectile.Center;
                int num6;
                for (int m = 0; m < Projectile.oldPos.Length; m = num6 + 1)
                {
                    Vector2 vector4 = Projectile.oldPos[m];
                    if (vector4 == Vector2.Zero)
                    {
                        break;
                    }
                    Color newColor2 = Main.hslToRgb(0.444444448f + Main.rand.NextFloat() * 0.222222224f, 1f, 0.65f);
                    int num21 = Main.rand.Next(1, 4);
                    float num22 = MathHelper.Lerp(0.3f, 1f, Utils.GetLerpValue((float)Projectile.oldPos.Length, 0f, (float)m, true));
                    if ((float)m >= (float)Projectile.oldPos.Length * 0.3f)
                    {
                        num6 = num21;
                        num21 = num6 - 1;
                    }
                    if ((float)m >= (float)Projectile.oldPos.Length * 0.75f)
                    {
                        num21 -= 2;
                    }
                    Vector2 value3 = vector4.DirectionTo(target4).SafeNormalize(Vector2.Zero);
                    target4 = vector4;
                    float num9;
                    for (float num23 = 0f; num23 < (float)num21; num23 = num9 + 1f)
                    {
                        bool flag = Main.rand.Next(3) == 0;
                        if (flag)
                        {
                            int num24 = Dust.NewDust(vector4, Projectile.width, Projectile.height, 267, 0f, 0f, 0, newColor2, 1f);
                            Dust dust2 = Main.dust[num24];
                            dust2.velocity *= Main.rand.NextFloat() * 0.8f;
                            Main.dust[num24].noGravity = true;
                            Main.dust[num24].scale = Main.rand.NextFloat() * 0.8f;
                            Main.dust[num24].fadeIn = Main.rand.NextFloat() * 1.2f * num22;
                            dust2 = Main.dust[num24];
                            dust2.velocity += value3 * 6f;
                            dust2 = Main.dust[num24];
                            dust2.scale *= num22;
                            if (num24 != 6000)
                            {
                                Dust dust6 = Dust.CloneDust(num24);
                                dust2 = dust6;
                                dust2.scale /= 2f;
                                dust2 = dust6;
                                dust2.fadeIn /= 2f;
                                dust6.color = new Color(255, 255, 255, 255);
                            }
                        }
                        else
                        {
                            Dust dust7 = Dust.NewDustDirect(vector4, Projectile.width, Projectile.height, 15, -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                            Main.rand.Next(2);
                            dust7.noGravity = true;
                            Dust dust2 = dust7;
                            dust2.velocity *= 2f;
                            dust2 = dust7;
                            dust2.velocity += value3 * 9f;
                            dust2 = dust7;
                            dust2.scale *= num22;
                            dust7.fadeIn = (0.6f + Main.rand.NextFloat() * 0.4f) * num22;
                            dust7.noLightEmittence = (dust7.noLight = true);
                        }
                        num9 = num23;
                    }
                    num6 = m;
                }
                for (int n = 0; n < 20; n = num6 + 1)
                {
                    Dust dust8 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, 0f, 0f, 0, default(Color), 1f);
                    dust8.noGravity = true;
                    dust8.velocity = Main.rand.NextVector2Circular(1f, 1f) * 1.5f;
                    dust8.scale = 1.2f + Main.rand.NextFloat() * 0.5f;
                    dust8.noLightEmittence = (dust8.noLight = true);
                    Dust dust2 = dust8;
                    dust2.velocity += Projectile.velocity * 0.01f;
                    dust2 = dust8;
                    dust2.position += dust8.velocity * (float)Main.rand.Next(1, 16);
                    dust8 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 15, 0f, 0f, 100, default(Color), 1f);
                    dust2 = dust8;
                    dust2.velocity *= 1.2f;
                    dust8.noLightEmittence = (dust8.noLight = true);
                    dust2 = dust8;
                    dust2.velocity += Projectile.velocity * 0.01f;
                    dust2 = dust8;
                    dust2.scale *= 0.8f + Main.rand.NextFloat() * 0.2f;
                    dust2 = dust8;
                    dust2.position += dust8.velocity * (float)Main.rand.Next(1, 16);
                    num6 = n;
                }
            }
        }
    }


