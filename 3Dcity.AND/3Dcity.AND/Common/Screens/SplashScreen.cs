using System;
using WindowsGame.Common.Static;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Define;

namespace WindowsGame.Common.Screens
{
	public class SplashScreen : BaseScreen, IScreen
	{
		private Vector2 bannerPosition;
		private Boolean flag;

		public override void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;

			bannerPosition = new Vector2(wide, high);
			flag = false;
		}

		public Int32 Update(GameTime gameTime)
		{
			return flag ? (Int32)ScreenType.Init : (Int32)ScreenType.Splash;
		}

		public override void Draw()
		{
			// TODO delegate this to device manager??
			Engine.Game.Window.Title = GetType().Name;// Globalize.GAME_TITLE;

			Engine.SpriteBatch.Draw(Assets.SplashTexture, bannerPosition, Color.White);
			flag = true;
		}

	}
}