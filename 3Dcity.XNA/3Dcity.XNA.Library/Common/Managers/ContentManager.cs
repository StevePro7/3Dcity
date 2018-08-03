using System;
using WindowsGame.Define.Factorys;
using WindowsGame.Common.Static;

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
			String fontRoot = String.Format("{0}/{1}/", contentRoot, FONTS_DIRECTORY);
			Assets.EmulogicFont = contentFactory.LoadFont(fontRoot + "Emulogic");

			// Sounds.
			//String soundsRoot = String.Format("{0}/{1}/", contentRoot, SOUND_DIRECTORY);

			// Textures.
			Assets.SteveProTexture40 = contentFactory.LoadTexture(texturesRoot + "StevePro40");
			Assets.SteveProTexture80 = contentFactory.LoadTexture(texturesRoot + "StevePro80");
			Assets.SteveProTexture160 = contentFactory.LoadTexture(texturesRoot + "StevePro160");
			Assets.SteveProTexture200 = contentFactory.LoadTexture(texturesRoot + "StevePro200");
			//Assets.GameScreen800 = contentFactory.LoadTexture(texturesRoot + "GameScreen800");
			//Assets.GameScreen960 = contentFactory.LoadTexture(texturesRoot + "GameScreen960");

			Assets.Target40Texture = contentFactory.LoadTexture(texturesRoot + "Target40");
			Assets.Target64Texture = contentFactory.LoadTexture(texturesRoot + "Target64");
			Assets.Target80Texture = contentFactory.LoadTexture(texturesRoot + "Target80");

			Assets.Enemy25Texture = contentFactory.LoadTexture(texturesRoot + "25");
			Assets.Enemy32Texture = contentFactory.LoadTexture(texturesRoot + "32");
			Assets.Enemy40Texture = contentFactory.LoadTexture(texturesRoot + "40");
			Assets.Enemy50Texture = contentFactory.LoadTexture(texturesRoot + "50");
			Assets.Enemy64Texture = contentFactory.LoadTexture(texturesRoot + "64");
			Assets.Enemy80Texture = contentFactory.LoadTexture(texturesRoot + "80");
			Assets.Enemy96Texture = contentFactory.LoadTexture(texturesRoot + "96");
			Assets.Enemy120Texture = contentFactory.LoadTexture(texturesRoot + "120");
			//Assets.Enemy128Texture = contentFactory.LoadTexture(texturesRoot + "128");

			Assets.PlayTexture = contentFactory.LoadTexture(texturesRoot + "play");
			Assets.PauseTexture = contentFactory.LoadTexture(texturesRoot + "pause");
			Assets.SoundOnTexture = contentFactory.LoadTexture(texturesRoot + "soundOn");
			Assets.SoundOffTexture = contentFactory.LoadTexture(texturesRoot + "soundOff");

			Assets.BackgroundTexture = contentFactory.LoadTexture(texturesRoot + "background");
			Assets.Foreground01Texture = contentFactory.LoadTexture(texturesRoot + "foreground01");
			Assets.Foreground02Texture = contentFactory.LoadTexture(texturesRoot + "foreground02");
			Assets.Foreground03Texture = contentFactory.LoadTexture(texturesRoot + "foreground03");
			Assets.ButtonOnTexture = contentFactory.LoadTexture(texturesRoot + "ButtonOn");
			Assets.ButtonOffTexture = contentFactory.LoadTexture(texturesRoot + "ButtonOff");
			Assets.JoypadTexture = contentFactory.LoadTexture(texturesRoot + "joypad");
			Assets.Stars01Texture = contentFactory.LoadTexture(texturesRoot + "stars01");
			Assets.Stars02Texture = contentFactory.LoadTexture(texturesRoot + "stars02");
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
