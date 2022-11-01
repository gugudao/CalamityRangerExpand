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

namespace CalamityAmmo.Global
{
    // Here is a class dedicated to showcasing Send/ReceiveExtraAI()
    public class CAEGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
              if (item.type == ModContent.ItemType<GloveOfRecklessness>())
              {
                 tooltips.Add(new TooltipLine(Mod,"GORtooltip", "提高15%远程攻速，但是降低10%远程伤害与5%远程暴击"));
              }
            if (item.type == ModContent.ItemType<GloveOfPrecision>())
            {
                tooltips.Add(new TooltipLine(Mod, "GOPtooltip", "降低15%远程攻速，但是提高10%远程伤害与5%远程暴击"));
            }




        }
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if (modplayer.Holster&& item.useAmmo == AmmoID.Bullet)
            {
                return UseSpeedMultiplier(item, player) *1.12f;
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