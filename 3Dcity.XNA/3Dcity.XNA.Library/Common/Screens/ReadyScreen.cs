using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			MyGame.Manager.ScoreManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);


			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.SpriteManager.Draw();
			MyGame.Manager.LevelManager.DrawLevelOrb();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.LevelManager.DrawLevelRoman();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
