using System;
using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using global::CalamityAmmo.Projectiles.Hardmode;
using static Terraria.ModLoader.PlayerDrawLayer;
using CalamityAmmo.Projectiles.Post_MoonLord;
using System.Collections.Generic;
using System.Linq;
using UtfUnknown.Core.Models.MultiByte.Chinese;
using CalamityMod.Items.Accessories;

namespace CalamityAmmo.Misc
{
    public class Chengying_Fumo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chengying's Fumo");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "精美毛绒玩具");
            Tooltip.SetDefault("You should understand that we cannot deal with the darkness with gentleness, but with fire.");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "你要明白，我们不能用温柔去应对黑暗，要用火。");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BeachBall);
            Item.width = 76;
            Item.height = 116;
            Item.rare = ModContent.RarityType<FirstRed>();
            Item.value = 0;
            Item.shoot = ModContent.ProjectileType<Chengying_Fumo_Proj>();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = Enumerable.FirstOrDefault<TooltipLine>(tooltips, (TooltipLine x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
            if(Main.dayTime)  
            {
                tooltip.Text = "“举杯吧！敬那些想要杀死我的人！”\n“你们失败了。”";
            }
            if(!Main.dayTime)
            {
                tooltip.Text = "“你要明白，我们不能用温柔去应对黑暗，要用火。”";
            }
            tooltip.OverrideColor = CAEUtils.ColorSwap(new Color(191, 0, 15), new Color(255, 79, 63), 3f);
        }
        public override void UpdateInventory(Player player)
        {
            player.AddBuff(BuffID.WellFed3, 2);
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.DevilHorns, 1)
                .AddIngredient(ItemID.Silk, 60)
                .AddIngredient(ItemID.BeachBall, 2)
                .AddIngredient(ItemID.Hay,100)
                .AddIngredient(ModContent.ItemType<DemonicBoneAsh>(), 5)
                .AddIngredient(ItemID.DemonHeart, 1)
                .AddIngredient(ItemID.SoulofLight, 3)
                .AddIngredient(ItemID.SoulofNight, 3)
                .AddIngredient(ItemID.SoulofFlight, 3)
                .AddIngredient(ItemID.BlackThread, 9)
                .AddIngredient(ItemID.RedDye, 1)
                .AddIngredient(ItemID.PurpleDye, 1)
                .AddIngredient(ItemID.BlackDye, 1)
                .AddIngredient(ItemID.BrownDye, 1)
                .AddIngredient(ItemID.SilverDye, 1)
                .AddIngredient(ItemID.MagicLavaDropper, 2)
                .AddTile(TileID.Loom)
                .Register();
        }

        public class Chengying_Fumo_Proj : ModProjectile
        {
            public override string Texture
            {
                get
                {
                    return "CalamityAmmo/Misc/Chengying_Fumo";
                }
            }
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Chengying's Fumo");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }
            public override void SetDefaults()
            {
                Projectile.width = 76;
                Projectile.height = 116;
                Projectile.netImportant = true;
                Projectile.aiStyle = 32;
                Projectile.friendly = true;
            }
            public override void Kill(int timeLeft)
            {
                Item.NewItem(Projectile.GetSource_FromThis(),
                    new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height),
                    ModContent.ItemType<Chengying_Fumo>());
            }
        }
    }

    public class FirstRed : ModRarity
    {

        
        public override Color RarityColor
        {
            get
            {
                return CAEUtils.ColorSwap(new Color(191, 0, 15), new Color(255, 79, 63), 3f);
            }
        }
        public override int GetPrefixedRarity(int offset, float valueMult)
        {
           /*switch (offset)
            {
                case -2:
                    return 11;
                case -1:
                    return ModContent.RarityType<Turquoise>();
                case 1:
                    return ModContent.RarityType<DarkBlue>();
                case 2:
                    return ModContent.RarityType<Violet>();
            }*/
            return Type;
        }
    }

     
}
