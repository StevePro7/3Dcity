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
			//LoadTextData();		// TODO delete
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			// Not bad settings for default.
			MyGame.Manager.BulletManager.Reset(10, 200, 100);

			LevelType levelType = MyGame.Manager.LevelManager.LevelType;
			const Byte enemySpawn = 1;
			const Byte enemyTotal = 3;
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 2000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.SoundManager.PlayMusic(SongType.GameMusic);
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
				// TODO delete
				MyGame.Manager.SoundManager.PlaySoundEffect();

				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX != bulletIndex)
				{
					Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
					MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
				}
			}

			// BULLETS.
			// Update bullets and test collision.
			MyGame.Manager.BulletManager.Update(gameTime);
			if (MyGame.Manager.CollisionManager.BulletCollisionList.Count > 0)
			{
				// Check collisions here.
			}


			// ENEMYS.
			// Update enemies and test collisions.
			MyGame.Manager.EnemyManager.Update(gameTime);
			if (MyGame.Manager.EnemyManager.EnemyTest.Count > 0)
			{
				Boolean collision = MyGame.Manager.CollisionManager.CheckOne();
				if (collision)
				{
					return (Int32)ScreenType.Over;
				}
			}
			//MyGame.Manager.EnemyManager.CheckAllEnemies();
			if (MyGame.Manager.CollisionManager.EnemysCollisionList.Count > 0)
			{
				// Check collisions here.
			}


			// Update fire icon.
			Byte canShootIndex = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			//MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, fireIcon);
			MyGame.Manager.IconManager.UpdateFireIcon(canShootIndex);

			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.LevelManager.DrawLevelOrb();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			//MyGame.Manager.TextManager.Draw(TextDataList);		// TODO delete
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.LevelManager.DrawLevelData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
