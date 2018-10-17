using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreenPlay , IScreen
	{
		private Rectangle[] images;
		private Byte x, y, z;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			images = MyGame.Manager.ImageManager.BorderRectangles;
			x = 160;
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
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(x - z, y - z), images[0], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(x + z, y - z), images[1], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(x - z, y + z), images[2], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(x + z, y + z), images[3], Color.White);

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
