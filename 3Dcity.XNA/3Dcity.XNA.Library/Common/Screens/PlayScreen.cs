using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			// Not bad settings for default.
			MyGame.Manager.BulletManager.Reset(10, 200, 100);

			LevelType levelType = MyGame.Manager.StateManager.LevelType;
			MyGame.Manager.EnemyManager.Reset(levelType, 5, 2000, 5000);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.ScoreManager.Reset();
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

			MyGame.Manager.CollisionManager.ClearCollisionList();

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			// Test shooting enemy ships.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX != bulletIndex)
				{
					Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
					MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
				}
			}

			// Update bullets and test collision.
			MyGame.Manager.BulletManager.Update(gameTime);
			if (MyGame.Manager.CollisionManager.BulletCollisionList.Count > 0)
			{
				// Check collisions here.
			}

			// Update enemies and test collisions.
			MyGame.Manager.EnemyManager.Update(gameTime);
			MyGame.Manager.EnemyManager.CheckAllEnemies();
			if (MyGame.Manager.CollisionManager.EnemysCollisionList.Count > 0)
			{
				// Check collisions here.
			}


			// Update fire icon.
			Byte fireIcon = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, fireIcon);

			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
}
