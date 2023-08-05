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
using CalamityMod.Buffs.Pets;
using CalamityMod.Items.Mounts;
using Microsoft.Xna.Framework.Input;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.SummonItems;

namespace CalamityAmmo.Accessories
{
    public class EvilMushroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Evil Mushroom");
            ////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "恶毒的蘑菇");
            // Tooltip.SetDefault("Both you and the target enemy gain Marked for Death");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "你和被命中的敌人均获得死亡标记");
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
            CreateRecipe()
            .AddIngredient(ItemID.VileMushroom, 1)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

            var recipe2 = CreateRecipe();
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

            recipe2 = CreateRecipe();
            recipe2.AddRecipeGroup(RecipeGroupID.Wood, 2);
            recipe2.AddIngredient(ItemID.RedDye, 1);
            recipe2.ReplaceResult(ModContent.ItemType<TrinketofChi>(), 1);
            recipe2.AddTile(TileID.HeavyWorkBench);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Chain, 1);
            recipe2.AddIngredient(ItemID.FlinxFur, 2);
            recipe2.ReplaceResult(ModContent.ItemType <TundraLeash> (), 1);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.ShadowKey, 1);
            recipe2.AddIngredient(ItemID.Obsidian, 5);
            recipe2.AddIngredient(ItemID.DemonTorch, 1);
            recipe2.ReplaceResult(ModContent.ItemType< OnyxExcavatorKey > (), 1);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.GoldWatch,1);
            recipe2.AddIngredient(ItemID.Gladius, 2);
            recipe2.ReplaceResult(ModContent.ItemType < GladiatorsLocket>(), 1);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.GlowingMushroom, 50);
            recipe2.ReplaceResult(ModContent.ItemType < FungalSymbiote > (), 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.EbonstoneBlock, 50);
            recipe2.ReplaceResult(ModContent.ItemType<CorruptionEffigy>(), 1);
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.CrimstoneBlock, 50);
            recipe2.ReplaceResult(ModContent.ItemType <CrimsonEffigy> (), 1);
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.Register();

            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.StoneBlock, 50);
            recipe2.ReplaceResult(ModContent.ItemType<Terminus>(), 1);
            recipe2.Register();
        }
    }
}

