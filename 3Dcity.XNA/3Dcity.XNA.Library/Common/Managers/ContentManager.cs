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
			String fontRoot = String.Format("{0}/{1}/", contentRoot, FONTS_DIRECTORY);
			Assets.EmulogicFont = contentFactory.LoadFont(fontRoot + "Emulogic");
		}

		public void LoadContentSplash()
		{
			// TODO revert!
			String splash = (0 == MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay) ? "StevePro" : "Splash";
			Assets.SplashTexture = contentFactory.LoadTexture(texturesRoot + splash);

			Assets.BackgroundTexture = contentFactory.LoadTexture(texturesRoot + "background");
			Assets.ForegroundTexture = contentFactory.LoadTexture(texturesRoot + "foreground01");
			Assets.JoypadTexture = contentFactory.LoadTexture(texturesRoot + "joypad");
			Assets.StarsTexture = contentFactory.LoadTexture(texturesRoot + "stars01");
		}

	}
}
