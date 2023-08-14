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



// �����ռ䣬ע����Ҫ���ļ��е�������ͬ
namespace CalamityAmmo
{
    // ��ҪMod��
    public class CalamityAmmo : Mod
    {

        public static bool ShowDustTestUI = false;
        public static CalamityAmmo Instance;
        public static Effect exEffect;

        public override void PostSetupContent()
        {
            //�����shader�ļ�����׺.xnb������ô�������ﱣ�棬���� exEffect = Assets.Request<Effect>("·��/shader�ļ�����������׺��").Value;
        }

        internal static Dictionary<string, int> MyGlows;
        private List<Asset<Texture2D>> assets;
        public override void Load()
        {
            //��һ���ִ����Ǵ���glowmask�Ļ������룬glowmask����������Ʒ�����׵���ͼ�ϵ�һ�㷢����ͼ��ԭ����Ҫ�ֶ����룬��δ����������Զ�����
            //�㴢����ģ���ļ�������ļ���ֻҪ�ļ�������������_Glow��������׺�����ͻᱻ����
            {
                MyGlows = new Dictionary<string, int>();
                assets = new List<Asset<Texture2D>>();
                string assetsPath;
                int i = 0;

                foreach (Type type in typeof(CalamityAmmo).Assembly.GetTypes().OrderBy((Type t) => t.FullName, StringComparer.InvariantCulture))
                {
                    assetsPath = type.FullName.Replace('.', '/') + "_Glow";
                    if (!type.IsAbstract && type.IsSubclassOf(typeof(ModItem)) && ModContent.HasAsset(assetsPath))
                    {
                        i++;
                        assets.Add(ModContent.Request<Texture2D>(assetsPath));
                        MyGlows.Add(type.Name, TextureAssets.GlowMask.Length - 1 + i);
                    }
                }

                List<Asset<Texture2D>> assetsGlow = new List<Asset<Texture2D>>(TextureAssets.GlowMask);
                assetsGlow.AddRange(assets);
                TextureAssets.GlowMask = assetsGlow.ToArray();
            }

        }

        public override void Unload()
        {
            //��������Modж��ʱ��Ѵ����glowmaskҲж�����ó��ڴ�
            //List<Asset<Texture2D>> assetsGlow = new List<Asset<Texture2D>>(TextureAssets.GlowMask);
            //assetsGlow.RemoveAll(x => assets.Find(y => y == x) != default(Asset<Texture2D>));
            //TextureAssets.GlowMask = assetsGlow.ToArray();
        }

        public CalamityAmmo()
        {
            Instance = this;
            // MOD�ĳ�ʼ����������������һЩ����
            // ע�⣬���Load() ������һ��

            // ������Щ�˽�һ�¾����ˣ�ÿ��mod��Ҫ�����
        }
        



        }
}
