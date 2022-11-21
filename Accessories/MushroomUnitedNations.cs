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
using CalamityMod.Buffs.StatDebuffs;
using CalamityAmmo.Ammos.Post_MoonLord;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Items.Accessories;
using static Humanizer.In;
using CalamityMod.Projectiles.Ranged;
using CalamityAmmo.Projectiles.Post_MoonLord;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod.Items.Weapons.Ranged;

namespace CalamityAmmo.Accessories
{
    public class MushroomUnitedNations : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom United Nations");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蘑拟黏合国");
            Tooltip.SetDefault("Both you and the target enemy gain Marked for Death");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),
                "生成一个治愈蘑菇，拾取后恢复15点生命值\n"+
                "你和被命中的敌人均获得死亡标记\n" +
                "受到伤害时释放一大片蘑菇孢子\n"+
                "未受伤时减少5点防御与2点生命再生，提升15%远程伤害与5%远程暴击率\n" +
                "受伤后不再减少生存属性并给予与菇共生增益\n"+
                "随着时间推移召唤可以伤害敌人的孢子\n" +
                "发射远程弹幕时有概率额外射出命中后分裂的真菌弹\n" +
                "隐藏饰品可见性时给予幻菇中毒buff\n" +
                "一杆蘑菇狙击枪悬浮在你的头顶，狙杀进入射程的敌人\n"+
                "敌人将不那么容易瞄准你");
            Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(4, 8, false));
            ItemID.Sets.AnimatesAsSoul[base.Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.Rarity10BuyPrice;
            Item.rare = 9;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Evil = true;
            modplayer.Spore = true;
            modplayer.Mycelium = true;
            modplayer.MUN=true;
            modplayer.Live=true;
            player.Calamity().fCarapace = true;
            if (hideVisual)
            {
                modplayer.Odd = true;
            }
           

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShroomiteHeadgear, 1);
            recipe.AddIngredient(ItemID.ShroomiteMask, 1);
            recipe.AddIngredient(ItemID.ShroomiteHelmet, 1);
            recipe.AddIngredient(ItemID.ShroomiteBar, 10);
            recipe.AddIngredient(ModContent.ItemType<LifeMushroom>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EvilMushroom>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FungalCarapace>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MarvelousMycelium>(), 1);
            recipe.AddIngredient(ModContent.ItemType<InfectedCrabGill>(), 1);
            recipe.AddIngredient(ModContent.ItemType<OdddMushroom>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Shroomer>(), 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
    public class Shroomere:ModProjectile
    {
        public Player Owner
        {
            get
            {
                return Main.player[Projectile.owner];
            }
        }
        public override void AI()
        {
            CaePlayer modplayer = Owner.GetModPlayer<CaePlayer>();
            if(modplayer.MUN)
            {
                Projectile.timeLeft = 2;
            }
            if(!modplayer.MUN)
            {
                Projectile.penetrate = 0;
            }
            Projectile.ai[0] += 1;
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.8f, 0.8f, 0.8f);
            Projectile.Center = Owner.Center+new Vector2(0,-100);
            if (Projectile.ai[0] >= 28)
            {
                Vector2 targetPos = Main.player[Projectile.owner].Center;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy() && (Vector2.Distance(npc.Center, Projectile.Center) < 1040f))
                    {
                        if ((targetPos == Main.player[Projectile.owner].Center) ||
                            (Vector2.Distance(targetPos, Main.player[Projectile.owner].Center)
                            >
                            Vector2.Distance(npc.Center, Main.player[Projectile.owner].Center)) &&
                Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetPos = npc.Center;
                            Projectile.rotation = Vector2.Normalize(targetPos - Projectile.Center).ToRotation();
                            Projectile.direction = Projectile.spriteDirection = (Projectile.Center.X > npc.Center.X) ? -1 : 1;
                            //if (Projectile.spriteDirection == -1)
                            //{
                            //    Projectile.rotation += MathHelper.Pi;
                            //}
                        }
                    }
                }
                if (targetPos != Main.player[Projectile.owner].Center && Main.myPlayer == Projectile.owner)
                {
                    SoundEngine.PlaySound(SoundID.Item40, Projectile.position);
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), 
                       Projectile.Center +( (Projectile.direction == 1) ? Vector2.Normalize(targetPos - Projectile.Center)*50: Vector2.Normalize(targetPos - Projectile.Center) *50), Vector2.Normalize(targetPos - Projectile.Center) * 16,ProjectileID.BulletHighVelocity , Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].usesIDStaticNPCImmunity = false;
                    Main.projectile[proj].usesLocalNPCImmunity = true;
                    Main.projectile[proj].localNPCHitCooldown = 15;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                        Projectile.Center + ((Projectile.direction == 1) ? Vector2.Normalize(targetPos - Projectile.Center) * 24: Vector2.Normalize(targetPos - Projectile.Center) * 24), Vector2.Normalize(targetPos - Projectile.Center) *16, ModContent.ProjectileType<Shroom>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].usesIDStaticNPCImmunity = false;
                    Main.projectile[proj].usesLocalNPCImmunity = true;
                    Main.projectile[proj].localNPCHitCooldown = 15;
                }
                Projectile.ai[0] = 1;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture, new Vector2(Projectile.Center.X, Projectile.Center.Y )
 - Main.screenPosition, new Rectangle(0, 0, 90, 28), lightColor, Projectile.rotation, new Vector2(45, 14), Projectile.scale, Projectile.spriteDirection == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
            return false;
        }
    }
}

