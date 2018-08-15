using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame.Common.Static
{
	public static class Assets
	{
		// Fonts.
		public static SpriteFont EmulogicFont;

		// Sound.
		public static IDictionary<SoundEffectType, SoundEffectInstance> SoundEffectDictionary;
		public static Song GameMusicSong;
		public static Song GameOverSong;

		// Textures.
		public static Texture2D SplashTexture;

		public static Texture2D SpriteSheet01Texture;
		public static Texture2D SpriteSheet02Texture;

		public static Texture2D SteveProTexture40;
		public static Texture2D SteveProTexture80;
		public static Texture2D SteveProTexture160;
		public static Texture2D SteveProTexture200;

		public static Texture2D BulletsTexture;
		//public static Texture2D Enemies96Texture;
		public static Texture2D Enemies120Texture;
		public static Texture2D Enemy25Texture;
		public static Texture2D Enemy32Texture;
		public static Texture2D Enemy40Texture;
		public static Texture2D Enemy50Texture;
		public static Texture2D Enemy64Texture;
		public static Texture2D Enemy80Texture;
		public static Texture2D Enemy96Texture;
		public static Texture2D Enemy120Texture;

		// TODO delete
		public static Texture2D ZZindigoTexture;
	}
}
