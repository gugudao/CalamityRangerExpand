using System;
using CalamityMod;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Misc

{
    public class SpectreRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Spectre Rifle");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "幽灵狙击枪"); 
            // base.Tooltip.SetDefault("Converts musket balls into powerful homing souls");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "将火枪子弹转化为强大的追踪灵魂弹");
            base.Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.Item.damage = 147;
            base.Item.DamageType = DamageClass.Ranged;
            base.Item.width = 88;
            base.Item.height = 30;
            base.Item.useTime = 48;
            base.Item.useAnimation = 48;
            base.Item.useStyle = 5;
            base.Item.noMelee = true;
            base.Item.knockBack = 6f;
            base.Item.value = Item.buyPrice(0, 80, 0, 0);
            base.Item.rare = ItemRarityID.Yellow;
            base.Item.UseSound = new SoundStyle?(SoundID.Item40);
            base.Item.autoReuse = false;
            base.Item.shoot = ProjectileID.LostSoulFriendly;
            base.Item.shootSpeed = 12f;
            base.Item.useAmmo = AmmoID.Bullet;
            base.Item.Calamity().canFirePointBlankShots = true;
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += 25f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2?(new Vector2(-10f, 0f));
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == 14)
            {
                int proj = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, 297, damage, knockback, player.whoAmI, 2f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
                Main.projectile[proj].extraUpdates += 2;
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.SpectreBar, 7).AddIngredient<CoreofEleum>(3).AddTile(TileID.MythrilAnvil).Register();
        }
    }
}

