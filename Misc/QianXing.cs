using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.DataStructures;
using System.Collections.Generic;
using ReLogic.Content;
using Terraria.ModLoader.IO;
using CalamityAmmo.Ammos.Pre_Hardmode;
using Terraria.GameContent.Events;
using CalamityAmmo.Accessories;
using CalamityMod;
using CalamityAmmo.Ammos.Hardmode;
using static CalamityAmmo.CAEUtils;
using CalamityAmmo.Ammos.Post_MoonLord;

namespace CalamityAmmo.Misc
{
    [AutoloadHead]
    public class QianXing:ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(GetTranslation("NPCs.QianXing.DisplayName"));
            Main.npcFrameCount[Type] = 23; // The amount of frames the NPC has
            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
            NPCID.Sets.AttackFrameCount[Type] = NPCID.Sets.AttackFrameCount[NPCID.Stylist];
            NPCID.Sets.DangerDetectRange[Type] = 100; // The amount of pixels away from the center of the npc that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = NPCID.Sets.AttackType[NPCID.Stylist];
            NPCID.Sets.AttackTime[Type] = 24; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 5;
            NPCID.Sets.HatOffsetY[Type] = 0; // For when a party is active, the party hat spawns at a Y offset.

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                              // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                              // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
            // NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love) // Example Person prefers the forest.
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Dislike) // Dislikes living near the merchant.
                .SetNPCAffection(NPCID.Angler, AffectionLevel.Hate) // Hates living near the demolitionist.
            ; // < Mind the semicolon!
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 20000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Stylist;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.CalamityAmmo.Bestiary.QianXing"))});
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        { // Requirements for the town NPC to spawn.
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (!player.active)
					continue;

				for (int j = 0; j < 58; j++)
				{
					Item item = player.inventory[j];
					if (item != null && item.stack > 0 && (item.ammo != 0 || item.useAmmo != 0))
						return true;
				}
			}

			return false;
		}
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
               GetTranslation("QX")
            };
        }
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            {
                    if (NPC.homeless)
                    {
                        if (Utils.NextBool(Main.rand, 2))
                        {
                            return GetTranslation("Homeless1");
                        }
                        return GetTranslation("Homeless2");
                    }
                    else
                    {
                        int Fisher = NPC.FindFirstNPC(369);
                    bool FisherIsAround = Fisher != -1;
                    int Tinker = NPC.FindFirstNPC(107);
                    bool TinkerIsAround = Tinker!= -1;
                    IList<string> dialogue = new List<string>();
                        dialogue.Add(GetTranslation("Any1"));
                        dialogue.Add(GetTranslation("Any2"));
                        dialogue.Add(GetTranslation("Any3"));
                        //dialogue.Add("想联机的话，承影粉丝群里有灾厄服务器哦，找我就行");
                        dialogue.Add(GetTranslation("Any4"));
                        if(Main.dayTime)
                        {
                            dialogue.Add(GetTranslation("Day1"));
                            dialogue.Add(GetTranslation("Day2"));
                            dialogue.Add(GetTranslation("Day3"));
                            dialogue.Add(GetTranslation("Day4"));
                        }
                        if(!Main.dayTime)
                        {
                            dialogue.Add(GetTranslation("Night1"));
                            dialogue.Add(GetTranslation("Night2"));
                            dialogue.Add(GetTranslation("Night3"));
                            dialogue.Add(GetTranslation("Night4"));
                        }
                        if (Main.bloodMoon)
                        {
                            dialogue.Add(GetTranslation("BM1"));
                            dialogue.Add(GetTranslation("BM2"));
                        }
                    if (BirthdayParty.PartyIsUp)
                    {
                        dialogue.Add(GetTranslation("Party"));
                    }
                    if(FisherIsAround)
                    {
                        dialogue.Add(GetTranslation("HateAnger1")+Main.npc[Fisher].GivenName+ GetTranslation("HateAnger2"));
                    }
                    if (TinkerIsAround)
                    {
                        dialogue.Add( GetTranslation("Goblin1")+Main.npc[Tinker].GivenName + GetTranslation("Goblin2"));
                    }
                    return dialogue[Main.rand.Next(dialogue.Count)];
                    }
                }
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 18;
            knockback = 5f;
        }
        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = ModContent.Request<Texture2D>("CalamityAmmo/Misc/翡翠斧").Value;
            //（注意，这里的Item是Texture2D形式，也就是说，只要有材质就够了）
            scale = 1f;
            //贴图大小，和实际尺寸无关
            //offset这个向量值是决定武器绘制在NPC的哪个位置，平时不用
        }
        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 40;
            itemHeight =38;
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            //翻译“商店文本”
            button = Language.GetTextValue("LegacyInterface.28");
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            //如果按下第一个按钮，则开启商店
            if (firstButton)
            {
                shop = true;
            }
            //在if之后可以写第二个按钮的作用，自己想想能加点啥
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            //设置商品
            //shop.item[nextSlot].SetDefaults(ItemID.StarCloak);
            //设置价格
            //shop.item[nextSlot].value = 10;
            //切换到下一栏，否则你的NPC只能卖一件物品
            //nextSlot++;
            //Player player = 
            if(Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<WulfrumArrow>());
                nextSlot++;
               if(Main.LocalPlayer.ZoneDesert)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<DesertFeatherArrow>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ElectricArrow>());
                    nextSlot++;
                }
                if (DownedBossSystem.downedDesertScourge)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PearlArrow>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<VictideArrow>());
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<WeakAstralArrow>());
                    nextSlot++;
                    if (Main.LocalPlayer.ZoneNormalUnderground)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<NapalmBullet>());
                        nextSlot++;
                    }
                    if (DownedBossSystem.downedAquaticScourge)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<FossilArrow>());
                        nextSlot++;
                    }
                    if (DownedBossSystem.downedRavager || NPC.downedGolemBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<HydrothermicArrow>());
                        nextSlot++;
                    }
                    if(NPC.downedMoonlord)
                    {
                        //shop.item[nextSlot].SetDefaults(ModContent.ItemType<ElementalArrow>());
                        //nextSlot++;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<GoldenFeatherArrow>());
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<MirageArrow>());
                        nextSlot++;
                    }
                }
            }
            else
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<WulfrumBullet>());
                nextSlot++;
                if (DownedBossSystem.downedDesertScourge)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PearlBullet>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<SPBullet>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<VictideBullet>());
                    nextSlot++;
                }
                if (DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<RottenBullet>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<BloodBullet>());
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<WeakAstralBullet>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<NapalmBullet>());
                    nextSlot++;
                    if (DownedBossSystem.downedAquaticScourge)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<FossilBullet>());
                        nextSlot++;
                    }
                    if (DownedBossSystem.downedRavager || NPC.downedGolemBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<HydrothermicBullet>());
                        nextSlot++;
                    }
                    if (NPC.downedMoonlord)
                    {
                        //shop.item[nextSlot].SetDefaults(ModContent.ItemType<ElementalArrow>());
                        //nextSlot++;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<ElementalBullet>());
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulBullet>());
                        nextSlot++;
                    }
                }
            }
        }
    }
}
