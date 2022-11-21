using System;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;



    namespace CalamityAmmo.Rockets

    {
        public class PlaguenadeLauncher : ModItem
        {
            int i = 0;
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault(" Plaguenade Launcher");
                DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "瘟疫蜜蜂掷弹筒");
                Tooltip.SetDefault("Fires a brittle spiky ball\nEvery four attack will shoot 6 sand blasts");
                Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "使用手榴弹作为弹药\n" +
                    "");
                SacrificeTotal = 1;
            }

            public override void SetDefaults()
            {
                Item.damage = 120;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 86;
                Item.height = 34;
                Item.useTime = 45;
                Item.useAnimation = 45;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.knockBack = 2f;
                Item.value = Item.buyPrice(0, 80, 0, 0);
                Item.rare = ItemRarityID.Green;
                Item.UseSound = new SoundStyle?(SoundID.Item40);
                Item.autoReuse = true;
                Item.shoot = ProjectileID.Beenade;
                Item.shootSpeed = 6f;
                Item.useAmmo = ItemID.Beenade;
                //Item.Calamity().canFirePointBlankShots = true;
            }

            public override void ModifyWeaponCrit(Player player, ref float crit)
            {

            }

            public override Vector2? HoldoutOffset()
            {
                return new Vector2?(new Vector2(0f, 0f));
            }
            public override bool? UseItem(Player player)
            {

                return null;
            }
            public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
            {
                int grenadeType = 0;
                float angle = Utils.ToRotation(velocity) + (float)Math.PI / 2;
                //Main.NewText(angle.ToRotationVector2());
                if (angle <= -Math.PI / 4 || angle >= Math.PI / 4)
                {
                    return false;
                }
                /*if (source.AmmoItemIdUsed == ModContent.ItemType<Plaguenade>())
                     grenadeType = ModContent.ProjectileType<PlaguenadeProj>();
                 if (source.AmmoItemIdUsed == ItemID.Grenade)
                     grenadeType = ProjectileID.Grenade;
                 if (source.AmmoItemIdUsed == ItemID.Beenade)
                     grenadeType = ProjectileID.Beenade;
                 if (source.AmmoItemIdUsed == ItemID.StickyGrenade)
                     grenadeType = ProjectileID.StickyGrenade;
                 if (source.AmmoItemIdUsed == ItemID.BouncyGrenade)
                     grenadeType = ProjectileID.BouncyGrenade;
                 if (source.AmmoItemIdUsed == ItemID.PartyGirlGrenade)
                     grenadeType = ProjectileID.PartyGirlGrenade;
                 Projectile.NewProjectile(source, position, velocity, grenadeType, damage, knockback, player.whoAmI);*/
                return true;
            }
            public override void AddRecipes()
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<BeenadeLauncher>(), 1);
                recipe.AddIngredient(ModContent.ItemType<InfectedArmorPlating>(),8);
                recipe.AddIngredient(ModContent.ItemType<PlagueCellCanister>(), 15);
                recipe.AddTile(ModContent.TileType <PlagueInfuser>());
                recipe.Register();
            }
        }
    }


