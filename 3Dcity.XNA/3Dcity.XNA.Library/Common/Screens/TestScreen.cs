using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreenPlay , IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			DetectTarget(gameTime);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.SpriteManager.Draw();
			// Text data last!
		}

	}
}
