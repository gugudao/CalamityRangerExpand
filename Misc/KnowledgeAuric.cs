using System;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Misc
{
    public class KnowledgeAuric : LoreItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auric Compressed Biscuits");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "圣金源压缩饼干");
            Tooltip.SetDefault("This twisted dreamscape, surrounded by unnatural pillars under a dark and hazy sky.\nNatural law has been upturned. What will you make of it?");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),
                "军队许久未开拔出征，朕的饼干都碎了！\n" +
                "咱们这儿烂一点，后勤处就烂一片！\n" +
                "咱们要是全烂了，泰拉各地就会揭竿而起，让咱们死无葬身之地呀，想想吧……");
            SacrificeTotal = 1;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<Violet>();
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.KnowledgeAuric = true;
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddTile(TileID.Bookcases).AddIngredient(ModContent.ItemType<AuricBar>(), 1).AddIngredient(ModContent.ItemType<PearlShard>(), 10).Register();
        }
    }
}

