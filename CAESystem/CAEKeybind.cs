using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.CAESystem
{
	public class CAEKeybind: ModSystem
	{
		public static ModKeybind OddHotKey { get; private set; }

		public override void Load()
		{
			OddHotKey = KeybindLoader.RegisterKeybind(Mod, "Odd", "G");
		}
		public override void Unload()
		{
			OddHotKey=null;
		}
	}
}
