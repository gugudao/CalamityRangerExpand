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
                "魔力消耗与伤害增幅和武器攻速相关\n" +
                "魔力病也会影响远程伤害\n" +
                "箭的速度大大提高\n20%的几率不消耗箭\n" +
                "魔法，能量。我的人民陷入其中不能自拔!");
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
            player.magicQuiver = true;
        }
        /*public override void AddRecipes()
        {
            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.MagicQuiver, 1);
            recipe2.AddIngredient(ItemID.ArcaneFlower, 1);
            recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.StalkersQuiver, 1);
            recipe2.AddIngredient(ItemID.ManaFlower, 1);
            recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
            recipe2.AddTile(TileID.TinkerersWorkbench);
            recipe2.Register();
        }*/
    }
}

