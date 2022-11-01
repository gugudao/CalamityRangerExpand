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
using CalamityMod.Buffs.StatDebuffs;
using CalamityAmmo.Ammos.Post_MoonLord;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Items.Accessories;

namespace CalamityAmmo.Accessories
{
    public class LifeMushroom : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Live Mushroom");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "一般路过的蘑菇");
            Tooltip.SetDefault("Both you and the target enemy gain Marked for Death");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在玩家附近生成一个治愈蘑菇，拾取后恢复15点生命值");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.Rarity3BuyPrice;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Live = true;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
    public class HealOrb : ModProjectile
    {
        public override string Texture => "CalamityAmmo/Accessories/LifeMushroom";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heal Orb");
            DisplayName.AddTranslation(GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "治疗菇");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true; Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Rectangle selfbox = new((int)Projectile.position.X, (int)Projectile.position.Y,
   Projectile.width, Projectile.height);
            Rectangle targetbox = new((int)player.position.X, (int)player.position.Y, player.width, player.height);
            Projectile.rotation += 0.2f;
            if (selfbox.Intersects(targetbox))
            {
                if (!player.moonLeech && Main.myPlayer == Projectile.owner)
                {
                    player.statLife += (int)Projectile.ai[0];
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }
                    player.HealEffect((int)Projectile.ai[0], true);
                    NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null);
                }
                Projectile.penetrate = 0;            
            }
        }
    }
}


