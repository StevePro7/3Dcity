using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class InitScreen : IScreen
	{
		private Vector2 bannerPosition;
		private Int32 nextScreen;
		private UInt16 splashDelay;
		private UInt16 splashTimer;
		private Boolean join;

		public void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;
			bannerPosition = new Vector2(wide, high);

			nextScreen = GetNextScreen();
			splashDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay;
			splashTimer = 0;
			join = false;
		}

		public void LoadContent()
		{
			splashTimer = 0;
			MyGame.Manager.ThreadManager.LoadContentAsync();
		}

		public Int32 Update(GameTime gameTime)
		{
			splashTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;

			// Do not attempt to progress until join.
			join = MyGame.Manager.ThreadManager.Join(1);
			if (!join)
			{
				return (Int32)ScreenType.Init;
			}

			if (splashTimer > splashDelay)
			{
				return nextScreen;
			}
			return (Int32)ScreenType.Init;
		}

		public void Draw()
		{
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
