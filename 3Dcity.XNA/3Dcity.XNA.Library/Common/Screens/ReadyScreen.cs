using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen
	{
		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();
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

			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				Vector2 pos2 = MyGame.Manager.SpriteManager.LargeTarget.Position;
				Vector2 pos3 = new Vector2(pos2.X + 28, pos2.Y + 28);
				SByte index = MyGame.Manager.CollisionManager.DetermineEnemySlot(pos2);

				String msg = String.Format("({0},{1})  [({2},{3}]] => {4}", pos2.X, pos2.Y, pos3.X, pos3.Y, index);
				MyGame.Manager.Logger.Info(msg);
			}


			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Individual texture.
			//MyGame.Manager.DebugManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
