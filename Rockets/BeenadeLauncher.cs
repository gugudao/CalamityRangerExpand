using System;
using System.Collections.Generic;
using System.Net.Sockets;
using CalamityAmmo.Global;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Tiles.AstralDesert;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Rockets

{
    public class BeenadeLauncher : ModItem
    {
        int i = 0;
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Beenade Launcher");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蜜蜂掷弹筒");
			// Tooltip.SetDefault("Fires a brittle spiky ball\nEvery four attack will shoot 6 sand blasts");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "使用手榴弹作为弹药\n" +"");
			Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
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
            Item.shootSpeed = 6.5f;
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
            
            return null ;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int grenadeType=0;
            float angle = Utils.ToRotation(velocity) + (float)Math.PI/2;
            //Main.NewText(angle.ToRotationVector2());
            if (angle <= -Math.PI / 4 || angle >= Math.PI / 4)
            {
                return false;
            }
           if (source.AmmoItemIdUsed == ModContent.ItemType<Plaguenade>())
            {
                grenadeType = ModContent.ProjectileType<PlaguenadeProj>();
                Projectile.NewProjectile(source, position, velocity*2/3, grenadeType, damage, knockback, player.whoAmI);
                return false;
            }
            /*if (source.AmmoItemIdUsed == ItemID.Grenade)
                grenadeType = ProjectileID.Grenade;
            if (source.AmmoItemIdUsed == ItemID.Beenade)
                grenadeType = ProjectileID.Beenade;
            if (source.AmmoItemIdUsed == ItemID.StickyGrenade)
                grenadeType = ProjectileID.StickyGrenade;
            if (source.AmmoItemIdUsed == ItemID.BouncyGrenade)
                grenadeType = ProjectileID.BouncyGrenade;
            if (source.AmmoItemIdUsed == ItemID.PartyGirlGrenade)
                grenadeType = ProjectileID.PartyGirlGrenade;
            */
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BeeWax, 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
