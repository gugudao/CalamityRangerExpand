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
    public class EvilMushroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Evil Mushroom");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "恶毒的蘑菇");
            Tooltip.SetDefault("Both you and the target enemy gain Marked for Death");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "你和被命中的敌人均获得死亡标记");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 0, 2, 0);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Evil = true;

        }
        public override void AddRecipes()
        {
 
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VileMushroom, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.ViciousMushroom, 1);
            recipe2.ReplaceResult(ModContent.ItemType<EvilMushroom>(), 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Geode, 1);
            recipe2.AddIngredient(ItemID.Granite, 8);
            recipe2.ReplaceResult(ModContent.ItemType <UnstableGraniteCore> (), 1);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Geode, 1);
            recipe2.AddIngredient(ItemID.Ruby, 1);
            recipe2.AddIngredient(ItemID.Sapphire, 1);
            recipe2.AddIngredient(ItemID.Amber, 1);
            recipe2.AddIngredient(ItemID.Emerald, 1);
            recipe2.AddIngredient(ItemID.Amethyst, 1);
            recipe2.ReplaceResult(ModContent.ItemType<LuxorsGift>(), 1);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}

