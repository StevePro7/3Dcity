using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Define;
using WindowsGame.Define.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class InitScreen : BaseScreen, IScreen 
	{
		private Vector2 bannerPosition;
		private Int32 nextScreen;
		private UInt16 splashDelay;
		private Boolean join;

		public override void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;
			bannerPosition = new Vector2(wide, high);

			nextScreen = GetNextScreen();
			splashDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay;
			join = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			MyGame.Manager.ThreadManager.LoadContentAsync();
		}

		public Int32 Update(GameTime gameTime)
		{
			UpdateTimer(gameTime);

			// Do not attempt to progress until join.
			join = MyGame.Manager.ThreadManager.Join(1);
			if (!join)
			{
				return (Int32)ScreenType.Init;
			}

			if (Timer > splashDelay)
			{
				return nextScreen;
			}
			return (Int32)ScreenType.Init;
		}

		public override void Draw()
		{
			// TODO delegate this to device manager??
			Engine.Game.Window.Title = GetType().Name;

			Engine.SpriteBatch.Draw(Assets.SplashTexture, bannerPosition, Color.White);
		}

		private static Int32 GetNextScreen()
		{
			ScreenType screenType = MyGame.Manager.ConfigManager.GlobalConfigData.ScreenType;
			if (ScreenType.Splash == screenType || ScreenType.Init == screenType)
			{
				screenType = ScreenType.Title;
			}

			return (Int32)screenType;
		}

	}
}
