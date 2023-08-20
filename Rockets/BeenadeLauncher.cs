using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Rockets

{
	public class BeenadeLauncher : ModItem
	{
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
			Item.damage = 12;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 86;
			Item.height = 34;
			Item.useTime = 50;
			Item.useAnimation = 50;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 10, 0, 0);
			//Item.value = Item.sellPrice(0, 10, 0, 0);
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

			return null;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			if (source.AmmoItemIdUsed == ModContent.ItemType<Plaguenade>())
			{
				int grenadeType = ModContent.ProjectileType<PlaguenadeProj>();
				Projectile.NewProjectile(source, position, velocity * 2 / 3, grenadeType, damage, knockback, player.whoAmI);
				return false;
			}
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
