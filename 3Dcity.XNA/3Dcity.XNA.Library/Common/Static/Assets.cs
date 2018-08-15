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
		//public static Texture2D GameScreen800;
		//public static Texture2D GameScreen960;

		public static Texture2D Target40Texture;
		public static Texture2D Target64Texture;
		//public static Texture2D Target80Texture;
		public static Texture2D PlayTexture;
		public static Texture2D PauseTexture;
		public static Texture2D SoundOnTexture;
		public static Texture2D SoundOffTexture;

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
		//public static Texture2D Enemy128Texture;

		//public static Texture2D Explosion64Texture;
		//public static Texture2D Explosion80Texture;
		//public static Texture2D Explosion128Texture;
		//public static Texture2D Explosion160Texture;

		// Initial screen.
		public static Texture2D BackgroundTexture;
		public static Texture2D Foreground01Texture;
		public static Texture2D Foreground02Texture;
		public static Texture2D Foreground03Texture;
		//public static Texture2D ButtonTexture;
		public static Texture2D ButtonOnTexture;
		public static Texture2D ButtonOffTexture;
		public static Texture2D JoypadTexture;
		public static Texture2D Stars01Texture;
		public static Texture2D Stars02Texture;

		// TODO delete
		//public static Texture2D ZZwhiteTexture;
		public static Texture2D ZZindigoTexture;
	}
}
