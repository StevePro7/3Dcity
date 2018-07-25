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

		String ContentRoot { get; }
	}

	public class ContentManager : IContentManager 
	{
		private readonly IContentFactory contentFactory;
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
			ContentRoot = String.Format("{0}{1}", root, Constants.CONTENT_DIRECTORY);
			texturesRoot = String.Format("{0}/{1}/", ContentRoot, TEXTURES_DIRECTORY);
		}

		public void LoadContent()
		{
			// Fonts.
			String fontRoot = String.Format("{0}/{1}/", ContentRoot, FONTS_DIRECTORY);
			Assets.EmulogicFont = contentFactory.LoadFont(fontRoot + "Emulogic");

			// Sounds.

			// Textures.
			Assets.SteveProTexture40 = contentFactory.LoadTexture(texturesRoot + "StevePro40");
			Assets.SteveProTexture80 = contentFactory.LoadTexture(texturesRoot + "StevePro80");
			Assets.SteveProTexture160 = contentFactory.LoadTexture(texturesRoot + "StevePro160");
			Assets.SteveProTexture200 = contentFactory.LoadTexture(texturesRoot + "StevePro200");
			Assets.GameScreen800 = contentFactory.LoadTexture(texturesRoot + "GameScreen800");
			Assets.GameScreen960 = contentFactory.LoadTexture(texturesRoot + "GameScreen960");
			Assets.IconTexture = contentFactory.LoadTexture(texturesRoot + "play80");

			Assets.BackgroundTexture = contentFactory.LoadTexture(texturesRoot + "background");
			Assets.ForegroundTexture = contentFactory.LoadTexture(texturesRoot + "foreground01");
			Assets.ButtonTexture = contentFactory.LoadTexture(texturesRoot + "button");
			Assets.JoypadTexture = contentFactory.LoadTexture(texturesRoot + "joypad");
			Assets.StarsTexture = contentFactory.LoadTexture(texturesRoot + "stars01");
		}

		public void LoadContentSplash()
		{
			// TODO revert!
			String splash = (0 == MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay) ? "StevePro160" : "Splash";
			Assets.SplashTexture = contentFactory.LoadTexture(texturesRoot + splash);
		}

		public String ContentRoot { get; private set; }
	}
}
