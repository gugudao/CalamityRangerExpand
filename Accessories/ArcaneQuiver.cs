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
using CalamityMod.Tiles.FurnitureVoid;


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
                "使你的箭矢在飞行轨迹上留下无害的紫色奥术粒子" ) ;
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
        public override void AddRecipes()
        {
            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.MagicQuiver, 1);
            recipe2.AddIngredient(ItemID.ArcaneFlower, 1);
            recipe2.AddIngredient(ItemID.CrystalShard, 5);
            recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.StalkersQuiver, 1);
            recipe2.AddIngredient(ItemID.ManaFlower, 1);
            recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
            recipe2.AddIngredient(ItemID.CrystalShard, 5);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
        }
    }
    public class ArcaneArrow_Proj : ModProjectile
    {
        private Vector2[] oldPosi = new Vector2[5];
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
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 360;
            Projectile.light = 0f;
            Projectile.extraUpdates = 0;
            Projectile.arrow = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255);
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
            Projectile.rotation = Utils.ToRotation(Projectile.velocity)+MathHelper.ToRadians(90f);
            if (Main.rand.Next(2) == 0)
            {
                int num9 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 21, 0f, 0f, 100, default, 1.75f);
                Main.dust[num9].velocity *= 0.3f;
                Main.dust[num9].position.X = Projectile.position.X + (float)(Projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
                Main.dust[num9].position.Y = Projectile.position.Y + (float)(Projectile.height / 2) + (float)Main.rand.Next(-4, 5);
                Main.dust[num9].noGravity = true;
                Main.dust[num9].velocity += Main.rand.NextVector2Circular(2f, 2f);
            }
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 400f, Projectile.velocity.Length(), 5f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            
            
                SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
                
            }
        }
    }


