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
    public class GrapeLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suspended Shotgun Shell Launcher");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霰弹下挂");
            Tooltip.SetDefault("Fire a series of bullets after hit enemy 8 times in a row");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "连续命中敌人8次后发射一连串子弹");
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
            if(player.HeldItem.useAmmo==AmmoID.Bullet)
            {
                modplayer.Grape = true;
            }
            else modplayer.Grape = false;

        }
        public override void AddRecipes()
        {

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.Grapes, 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

        }
    }
}


