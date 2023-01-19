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

namespace CalamityAmmo.Accessories
{
    public class TitaniumCoil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Coil ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "钛金线圈");
            Tooltip.SetDefault("Increased ranged damage ,amplification depends on attack speed\nMediumly increases all ranged projectile velocity");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "提高远程伤害\n远程武器的攻速越慢，伤害越高\n中幅提升远程弹幕的飞行速度");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Coil4 = true;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (modplayer.Coil3)
            {
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ModifiedCoil>());
            recipe.AddIngredient(ItemID.RangerEmblem,1);
            recipe.AddIngredient(ItemID.TitaniumBar, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}


