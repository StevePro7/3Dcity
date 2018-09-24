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
		public static IDictionary<SongType, Song> SongDictionary;
		//public static Song GameMusicSong;
		//public static Song GameOverSong;

		// Textures.
		public static Texture2D SplashTexture;

		public static Texture2D SpriteSheet01Texture;
		public static Texture2D SpriteSheet02Texture;
		public static Texture2D GameTitle;

		public static Texture2D OrbGreen32;
		public static Texture2D OrbRed32;


		// TODO delete
		//public static Texture2D Enemy120;
		//public static Texture2D Target56;
		//public static Texture2D Target64;
		//public static Texture2D SteveProTexture40;
		//public static Texture2D SteveProTexture80;
		//public static Texture2D SteveProTexture160;
		//public static Texture2D SteveProTexture200;
		//public static Texture2D title01;
		public static Texture2D ZZindigoTexture;
	}
}
