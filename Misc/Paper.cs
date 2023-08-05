using System;
using CalamityAmmo.Accessories;
using CalamityAmmo.Ammos.Hardmode;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Potions;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Misc

{
    public class Paper : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wood Pulp Paper");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "木浆纸");
            //Tooltip.SetDefault("{$CommonItemTooltip.MajorStats}\nJust one look and you're full");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "{$CommonItemTooltip.MajorStats}\n光是看上一眼就饱了");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 88;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 1;
            Item.autoReuse = false;
            Item.consumable = true;
            Item.maxStack = 9999;
            
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<Paper>(), 1);
            recipe2.ReplaceResult(4344, 1);
            recipe2.Register();
        }
    }
    public class Paper2 : ModItem
    {
        public override string Texture => "CalamityAmmo/Misc/Paper";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bamboo Pulp Paper");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "竹浆纸");
            //Tooltip.SetDefault("{$CommonItemTooltip.MajorStats}\nJust one look and you're full");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "{$CommonItemTooltip.MajorStats}\n光是看上一眼就饱了");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 88;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 1;
            Item.autoReuse = false;
            Item.consumable = true;
            Item.maxStack = 9999;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(4564, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<Paper2>(), 1);
            recipe2.ReplaceResult(4343, 1);
            recipe2.Register();
        }
    }
}

