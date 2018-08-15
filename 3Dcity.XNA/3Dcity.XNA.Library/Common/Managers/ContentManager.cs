using System;
using System.Collections.Generic;
using WindowsGame.Master;
using WindowsGame.Master.Factorys;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame.Common.Managers
{
	public interface IContentManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadContentSplash();

		//String ContentRoot { get; }
	}

	public class ContentManager : IContentManager 
	{
		private readonly IContentFactory contentFactory;
		private String contentRoot;
		private String texturesRoot;

		private const String FONTS_DIRECTORY = "Fonts";
		private const String SOUND_DIRECTORY = "Sound";
		private const String TEXTURES_DIRECTORY = "Textures";

		public ContentManager(IContentFactory contentFactory)
		{
			this.contentFactory = contentFactory;
		}

		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
			contentRoot = String.Format("{0}{1}", root, Constants.CONTENT_DIRECTORY);
			texturesRoot = String.Format("{0}/{1}/", contentRoot, TEXTURES_DIRECTORY);
		}

		public void LoadContent()
		{
			// Fonts.
			String fontsRoot = String.Format("{0}/{1}/", contentRoot, FONTS_DIRECTORY);
			Assets.EmulogicFont = contentFactory.LoadFont(fontsRoot + "Emulogic");

			// Sounds.
			String soundsRoot = String.Format("{0}/{1}/", contentRoot, SOUND_DIRECTORY);
			Assets.GameMusicSong = Engine.Content.Load<Song>(soundsRoot + "GameMusic");
			Assets.GameOverSong = Engine.Content.Load<Song>(soundsRoot + "GameOver");

			Assets.SoundEffectDictionary = new Dictionary<SoundEffectType, SoundEffectInstance>();
			for (SoundEffectType key = SoundEffectType.Funny; key <= SoundEffectType.Early; ++key)
			{
				String assetName = String.Format("{0}{1}", soundsRoot, key);
				SoundEffectInstance value = contentFactory.LoadSoundEffectInstance(assetName);
				Assets.SoundEffectDictionary.Add(key, value);
			}

			// Textures.
			Assets.SpriteSheet01Texture = contentFactory.LoadTexture(texturesRoot + "spritesheet01-1024");
			Assets.SpriteSheet02Texture = contentFactory.LoadTexture(texturesRoot + "spritesheet02-1024");

			Assets.SteveProTexture40 = contentFactory.LoadTexture(texturesRoot + "StevePro40");
			Assets.SteveProTexture80 = contentFactory.LoadTexture(texturesRoot + "StevePro80");
			Assets.SteveProTexture160 = contentFactory.LoadTexture(texturesRoot + "StevePro160");
			Assets.SteveProTexture200 = contentFactory.LoadTexture(texturesRoot + "StevePro200");

			Assets.Target40Texture = contentFactory.LoadTexture(texturesRoot + "Target40");
			Assets.Target64Texture = contentFactory.LoadTexture(texturesRoot + "Target64");
			//Assets.Target80Texture = contentFactory.LoadTexture(texturesRoot + "Target80");

			Assets.BulletsTexture = contentFactory.LoadTexture(texturesRoot + "bullets");
			//Assets.Enemies96Texture = contentFactory.LoadTexture(texturesRoot + "enemies96");
			Assets.Enemies120Texture = contentFactory.LoadTexture(texturesRoot + "enemies120");

			Assets.Enemy25Texture = contentFactory.LoadTexture(texturesRoot + "25");
			Assets.Enemy32Texture = contentFactory.LoadTexture(texturesRoot + "32");
			Assets.Enemy40Texture = contentFactory.LoadTexture(texturesRoot + "40");
			Assets.Enemy50Texture = contentFactory.LoadTexture(texturesRoot + "50");
			Assets.Enemy64Texture = contentFactory.LoadTexture(texturesRoot + "64");
			Assets.Enemy80Texture = contentFactory.LoadTexture(texturesRoot + "80");
			Assets.Enemy96Texture = contentFactory.LoadTexture(texturesRoot + "96");
			Assets.Enemy120Texture = contentFactory.LoadTexture(texturesRoot + "120");

			Assets.Explosion64Texture = contentFactory.LoadTexture(texturesRoot + "Explosion64");
			Assets.Explosion80Texture = contentFactory.LoadTexture(texturesRoot + "Explosion80");
			Assets.Explosion128Texture = contentFactory.LoadTexture(texturesRoot + "Explosion128");
			Assets.Explosion160Texture = contentFactory.LoadTexture(texturesRoot + "Explosion160");
		}

		public void LoadContentSplash()
		{
			// TODO revert!
			String splash = (0 == MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay) ? "StevePro160" : "Splash";
			Assets.SplashTexture = contentFactory.LoadTexture(texturesRoot + splash);
		}

		//public String ContentRoot { get; private set; }
	}
}
