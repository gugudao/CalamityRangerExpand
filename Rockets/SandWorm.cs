using System;
using System.Collections.Generic;
using CalamityAmmo.Global;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
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
    public class SandWorm : ModItem
    {
        int i = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Dog");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "沙狗");
            Tooltip.SetDefault("Fires a brittle spiky ball\nEvery four attack will shoot 6 sand blasts");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "发射一个易碎的刺球\n" +
                "每攻击四次额外发射一小波沙暴");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 106;
            Item.height = 35;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle?(SoundID.Item40);
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 10f;
            //Item.useAmmo = AmmoID.Rocket;
            //Item.Calamity().canFirePointBlankShots = true;
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            //crit += 22f;
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
            int b = Projectile.NewProjectile(source, position , velocity, ModContent.ProjectileType<SpikyBall>(), damage, knockback, player.whoAmI);
            //Main.projectile[b].position += velocity*7+new Vector2(0,-10);
            i++;
            //if (i > 3) i = 0;
            if (i >= 4)
            {
                i= 0;
                for (float rad = (float)Math.PI / -12f; rad <= Math.PI / 4f; rad += (float)Math.PI / 18f)
                {
                    Vector2 finalVec = (velocity.ToRotation() + rad).ToRotationVector2() * 8f;
                    int a =Projectile.NewProjectile(source, position, finalVec, ModContent.ProjectileType<GreatSandBlast>(), damage, knockback, player.whoAmI);
                    Main.projectile[a].friendly = true;
                    Main.projectile[a].hostile = false;
                    Main.projectile[a].tileCollide = true;
                    //Main.projectile[a].position += player.direction > 0 ? new Vector2(15f, -4f) : new Vector2(-15f, -4f);
                }
            }
            return false;
        }
    }
    public class SpikyBall:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiky Ball");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "沙刺球");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Кровавая пуля");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        }
    public override void SetDefaults()
    {
        Projectile.width = 4;
        Projectile.height = 14;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 300;
        Projectile.tileCollide = true;
        Projectile.ignoreWater = true;
        Projectile.usesLocalNPCImmunity = true;//NPC是不是按照弹幕ID来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则八个都能击中，不骗伤，原版夜明弹的反骗伤就是如此）
        Projectile.localNPCHitCooldown = 15;//上一个设定为true则被调用，NPC按照弹幕ID来获取多少无敌帧
        Projectile.usesIDStaticNPCImmunity = false;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透，原版用它来控制喽啰的输出上限）
        Projectile.idStaticNPCHitCooldown = 10;//上一个设定为true则被调用，NPC按照弹幕类型来获取多少无敌帧
        Projectile.netImportant = false;
        //Projectile.extraUpdates = 2;
        //Projectile.Calamity().pointBlankShotDuration = 18;
    }
    public override bool? CanCutTiles() => true;
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.rotation += Math.Sign(Projectile.velocity.X) * 0.1f;
            if (Projectile.ai[0] >= 30)
            {
                Projectile.velocity.Y += 0.2f;
                if (Projectile.velocity.Y > 12f) Projectile.velocity.Y = 12f;
                Projectile.velocity.X *= 0.99f;
            }
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        
    }
    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    {
        base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
    }
    public override bool PreDraw(ref Color lightColor)
    {
        //Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
        return base.PreDraw(ref lightColor);
    }

    public override bool PreKill(int timeLeft)
    {
        
        Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

        return base.PreKill(timeLeft);
    }
        public override void Kill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center - Projectile.velocity, new Vector2(6, 0).RotatedByRandom(Math.PI * 2f), ModContent.ProjectileType<Spike>(), Projectile.damage/3, Projectile.knockBack, Projectile.owner);
                }
            }
        }

    }
    public class Spike: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spike");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "斯派克");
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.friendly = true; Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override bool? CanCutTiles()
        {
            return true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            int frameheight = texture.Height / Main.projFrames[Projectile.type];
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, frameheight * Projectile.frame, texture.Width, frameheight), lightColor, Projectile.rotation,
            new Vector2(texture.Width / 2, frameheight / 2), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool PreKill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado);
            }
            return base.PreKill(timeLeft);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y > 12f) Projectile.velocity.Y = 12f;
            Projectile.rotation = Projectile.velocity.ToRotation() + ((float)Math.PI / 4f)*3f;
        }
    }
}



