using System;
using CalamityMod;
using CalamityMod.Items.Materials;
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
    public class ChewingGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chewing Gun");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "口枪糖");
            Tooltip.SetDefault("barely-usable");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "勉强能冲");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Minishark);
            Item.value = 200;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Gel,18).AddTile(TileID.WorkBenches).Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.Next(100) >= 33;
        }
    }
}

