using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreenPlay , IScreen
	{
		private Byte x, y, z;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			x = 128;
			y = 215;
			z = 1;
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

			Boolean test = MyGame.Manager.InputManager.StatusBar();
			if (test)
			{
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.SpriteManager.Draw();
			Engine.SpriteBatch.Draw(Assets.TLBar, new Vector2(x - z, y - z), Color.White);
			Engine.SpriteBatch.Draw(Assets.TRBar, new Vector2(x + z, y - z), Color.White);
			Engine.SpriteBatch.Draw(Assets.BLBar, new Vector2(x - z, y + z), Color.White);
			Engine.SpriteBatch.Draw(Assets.BRBar, new Vector2(x + z, y + z), Color.White);

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
