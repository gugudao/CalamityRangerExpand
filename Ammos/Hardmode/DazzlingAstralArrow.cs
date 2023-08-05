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
using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using static CalamityAmmo.CAEUtils;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod.CalPlayer;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class DazzlingAstralArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
                // DisplayName.SetDefault("Dazzling Astral Arrow");
                //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "炫星箭");
                // Tooltip.SetDefault(IsChinese() ? "星星，排列好了！\n你确定？" : "Look to the skies , The stars align");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在打败白金星舰后升级\n在打败星神游龙会再次升级");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ослепительная Астральная Стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "БЗвезды расставлены!\n вы уверены?");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 1, 0);
            Item.rare = 9;
            Item.shoot = ModContent.ProjectileType < _DazzlingAstralArrow>();
            Item.shootSpeed = 5f;
            Item.ArmorPenetration = 5;
            Item.ammo = AmmoID.Arrow;
        }
        public override void UpdateInventory(Player player)
        {
            int i = Item.stack;
            if (!DownedBossSystem.downedAstrumDeus)
            {
                Item.SetDefaults(ModContent.ItemType<AstralArrow>(), true);
                Item.stack = i;
            }
        }
    }
}
