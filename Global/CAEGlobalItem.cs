using Terraria;
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
            if(player.HeldItem.DamageType==DamageClass.Ranged)
            {
                if (Main.rand.NextBool(8))
                {
                    {
                        Vector2 direction= Vector2.Normalize(Main.MouseWorld - player.Center);
                        Projectile.NewProjectile(Projectile.GetSource_NaturalSpawn(), position, velocity, ModContent.ProjectileType<FungiOrb>(), (int)(item.damage * 0.3f), 0f, player.whoAmI);
                    }
                }
            }
            return true;
        }
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (modplayer.Holster&& item.useAmmo == AmmoID.Bullet)
            {
                return UseSpeedMultiplier(item, player) *1.1f;
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
    public class ModifyCal : GlobalItem
    {

    }
}