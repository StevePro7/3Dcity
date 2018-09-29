using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class IntroScreen : BaseScreen, IScreen
	{
		private Vector2 startPosition;
		private Vector2 titlePosition;
		private Vector2 moverPosition;
		private Single startY;
		private Single titleY;
		private Single deltaY;

		public override void Initialize()
		{
			base.Initialize();

			titlePosition = new Vector2((Constants.ScreenWide - 240) / 2.0f, (Constants.ScreenHigh - 160) / 2.0f + 94);
			startPosition = new Vector2(titlePosition.X, Constants.ScreenHigh - 160);

			startY = startPosition.Y;
			titleY = titlePosition.Y;
		}

		public override void LoadContent()
		{
			UInt16 introDelay = MyGame.Manager.ConfigManager.GlobalConfigData.IntroDelay;
			deltaY = startY - titleY;
			deltaY = introDelay / deltaY;
			moverPosition = startPosition;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			if (startY > titleY)
			{
				Single delta = (Single) gameTime.ElapsedGameTime.TotalSeconds;
				startY -= delta * deltaY * 1;
				moverPosition.Y = startY;
			}
			else
			{
				return (Int32) ScreenType.Diff;
			}
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();

			// Individual texture.
			MyGame.Manager.RenderManager.DrawTitle();

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
		}

	}
}
