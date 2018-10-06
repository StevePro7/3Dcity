using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			//LoadTextData();		// TODO delete
			UpdateGrid = false;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Log delta to monitor performance!
#if DEBUG
			//MyGame.Manager.Logger.Info(gameTime.ElapsedGameTime.TotalSeconds.ToString());
#endif

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);


			// Test shooting enemy ships.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				Vector2 pos = MyGame.Manager.SpriteManager.LargeTarget.Position;
				// MyGame.Manager.SpriteManager.LargeTarget.Position = {X:-2 Y:74}		// TOP LEFT
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.DrawLevelOrb();
			MyGame.Manager.SpriteManager.Draw();

			// Individual texture.
			MyGame.Manager.DebugManager.Draw();

			// Text data last!
			//MyGame.Manager.TextManager.Draw(TextDataList);			// TODO delete
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.LevelManager.DrawLevelData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
