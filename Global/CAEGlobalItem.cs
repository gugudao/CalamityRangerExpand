﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using System.IO;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Weapons.Magic;
using System.Collections.Generic;
using CalamityAmmo.Ammos.Hardmode;
using CalamityMod;
using static CalamityAmmo.CAEUtils;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.Items.TreasureBags;
using Terraria.GameContent.ItemDropRules;
using CalamityAmmo.Accessories;
using CalamityAmmo.Misc;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using CalamityMod.Items.Accessories;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework.Input;
using CalamityAmmo.Ammos.Post_MoonLord;
using Mono.Cecil;
using CalamityMod.Items.Weapons.Ranged;

namespace CalamityAmmo.Global
{
    // Here is a class dedicated to showcasing Send/ReceiveExtraAI()
    public class CAEGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
              if (item.type == ModContent.ItemType<GloveOfRecklessness>())
              {
                 tooltips.Add(new TooltipLine(Mod,"GORtooltip", "类似效果也作用于射手"));
              }
            if (item.type == ModContent.ItemType<GloveOfPrecision>())
            {
                tooltips.Add(new TooltipLine(Mod, "GOPtooltip", "类似效果也作用于射手"));
            }
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            
            if (player.HeldItem.DamageType==DamageClass.Ranged&&modplayer.Spore)
            {
                if (Main.rand.NextBool(8))
                {
                    {
                        //Vector2 direction= Vector2.Normalize(Main.MouseWorld - player.Center);
                        Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FungiOrb>(), (int)(damage * 0.3f), 0f, player.whoAmI);
                    }
                }
            }
            if(player.HeldItem.useAmmo==AmmoID.Arrow&&modplayer.Arcane)
            {
                item.mana = item.useTime /3;
                //item.shoot = ModContent.ProjectileType<ArcaneArrow_Proj>();
            }
            if(player.HeldItem.useAmmo == AmmoID.Arrow && !modplayer.Arcane)
            {
                item.mana = 0;
            }
         
                
           
            return true;
        }
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (modplayer.Holster&& item.useAmmo == AmmoID.Bullet)
            {
                return 1.1f;
            }
          if(modplayer.LowATKspeed)
            {
                return 0.000005f;
            }
            return base.UseSpeedMultiplier(item, player);
        }
        
    }
    public class BossBag:GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if(item.type==ModContent.ItemType<CrabulonBag>())
            {
                itemLoot.Add(ModContent.ItemType<InfectedCrabGill>(), 5, 1, 1);
                itemLoot.Add(ModContent.ItemType<MarvelousMycelium>(), 5, 1, 1);
            }
            if(item.type==ModContent.ItemType<StarterBag>())
            {
                itemLoot.Add(ModContent.ItemType<HardTackChest>(), 1, 1, 1);
            }
        }
    }
    public class ThePackRework : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
                Vector2 shootVel = velocity;
                int rockettype = 0;
            if(player.HeldItem.type==ModContent.ItemType<ThePack>())
            {
                switch (source.AmmoItemIdUsed)
                {
                    case ItemID.RocketI: rockettype = ModContent.ProjectileType<ThePack_RocketI>(); break;
                    case ItemID.RocketII: rockettype = ModContent.ProjectileType<ThePack_RocketII>(); break;
                    case ItemID.RocketIII: rockettype = ModContent.ProjectileType<ThePack_RocketIII>(); break;
                    case ItemID.RocketIV: rockettype = ModContent.ProjectileType<ThePack_RocketIV>(); break;
                    case ItemID.ClusterRocketI: rockettype = ModContent.ProjectileType<ThePack_ClusterRocketI>(); break;
                    case ItemID.ClusterRocketII: rockettype = ModContent.ProjectileType<ThePack_ClusterRocketII>(); break;

                    case ItemID.MiniNukeI: rockettype = ModContent.ProjectileType<ThePack_MiniNukeRocketI>(); break;
                    case ItemID.MiniNukeII: rockettype = ModContent.ProjectileType<ThePack_MiniNukeRocketII>(); break;

                    case ItemID.WetRocket: rockettype = ModContent.ProjectileType<ThePack_WetRocket>(); break;
                    case ItemID.LavaRocket: rockettype = ModContent.ProjectileType<ThePack_LavaRocket>(); break;
                    case ItemID.HoneyRocket: rockettype = ModContent.ProjectileType<ThePack_HoneyRocket>(); break;
                    case ItemID.DryRocket: rockettype = ModContent.ProjectileType<ThePack_DryRocket>(); break;
                }
                Projectile.NewProjectile(source, position, shootVel, rockettype, damage, knockback, player.whoAmI);
                return false;
            }

            return true;
              
            
        }
    }
}