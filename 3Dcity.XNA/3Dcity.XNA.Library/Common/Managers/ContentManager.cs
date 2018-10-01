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
			if (MyGame.Manager.ConfigManager.GlobalConfigData.LoadAudio)
			{
				String soundsRoot = String.Format("{0}/{1}/", contentRoot, SOUND_DIRECTORY);

				Assets.SongDictionary = new Dictionary<SongType, Song>();
				for (SongType key = SongType.BossMusic; key <= SongType.GameTitle; ++key)
				{
					String assetName = String.Format("{0}{1}", soundsRoot, key);
					Song value = contentFactory.LoadSong(assetName);
					Assets.SongDictionary.Add(key, value);
				}

				Assets.SoundEffectDictionary = new Dictionary<SoundEffectType, SoundEffectInstance>();
				for (SoundEffectType key = SoundEffectType.Funny; key <= SoundEffectType.Early; ++key)
				{
					String assetName = String.Format("{0}{1}", soundsRoot, key);
					SoundEffectInstance value = contentFactory.LoadSoundEffectInstance(assetName);
					Assets.SoundEffectDictionary.Add(key, value);
				}
			}

			// Textures.
			Assets.SpriteSheet01Texture = contentFactory.LoadTexture(texturesRoot + "spritesheet01-1024");
			Assets.SpriteSheet02Texture = contentFactory.LoadTexture(texturesRoot + "spritesheet02-1024");


			// TODO delete
			//Assets.GameTitle = contentFactory.LoadTexture(texturesRoot + "3Dcity");
			//Assets.OrbGreen32 = contentFactory.LoadTexture(texturesRoot + "OrbGreen32");
			//Assets.OrbRed32 = contentFactory.LoadTexture(texturesRoot + "OrbRed32");
			//Assets.Enemy120 = contentFactory.LoadTexture(texturesRoot + "Enemy120");
			//Assets.Target56 = contentFactory.LoadTexture(texturesRoot + "Target56");
			//Assets.Target64 = contentFactory.LoadTexture(texturesRoot + "Target64");
			//Assets.SteveProTexture40 = contentFactory.LoadTexture(texturesRoot + "StevePro40");
			//Assets.SteveProTexture80 = contentFactory.LoadTexture(texturesRoot + "StevePro80");
			//Assets.SteveProTexture160 = contentFactory.LoadTexture(texturesRoot + "StevePro160");
			//Assets.SteveProTexture200 = contentFactory.LoadTexture(texturesRoot + "StevePro200");
			//Assets.Target64 = contentFactory.LoadTexture(texturesRoot + "Target64");
			//Assets.title01 = contentFactory.LoadTexture(texturesRoot + "title01");
			Assets.ZZindigoTexture = contentFactory.LoadTexture(texturesRoot + "ZZindigo");
		}

		public void LoadContentSplash()
		{
			// TODO revert!
			String splash = (0 == MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay) ? "StevePro160" : "Splash";
			Assets.SplashTexture = contentFactory.LoadTexture(texturesRoot + splash);
		}

	}
}
