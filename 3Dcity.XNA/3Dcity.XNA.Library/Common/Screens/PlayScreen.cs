using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreen, IScreen
	{
		private Boolean isGodMode;
		private Boolean checkLevelComplete;

		public override void Initialize()
		{
			base.Initialize();
			//LoadTextData();		// TODO delete
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			isGodMode = MyGame.Manager.StateManager.IsGodMode;

			// Not bad settings for default.
			//MyGame.Manager.BulletManager.Reset(10, 200, 100);
			MyGame.Manager.BulletManager.Reset(1, 200, 500);

			LevelType levelType = MyGame.Manager.LevelManager.LevelType;
			Byte levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(levelType, levelIndex);

			//const Byte enemySpawn = 1;
			//const Byte enemyTotal = 3;
			Byte enemySpawn = MyGame.Manager.ConfigManager.GlobalConfigData.EnemySpawn;	// 1;  // TODO level config
			Byte enemyTotal = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyTotal;	// 1;  // TODO level config
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 1000, 5000, enemyTotal);
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

			checkLevelComplete = false;

			// Log delta to monitor performance!
#if DEBUG
			//MyGame.Manager.Logger.Info(gameTime.ElapsedGameTime.TotalSeconds.ToString());
#endif

			// TODO delete
			//MyGame.Manager.CollisionManager.ClearCollisionList();

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
					// TODO check!
					MyGame.Manager.SoundManager.PlaySoundEffect();

					Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
					MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
				}
			}

			// BULLETS.
			// Update bullets and test collision.
			MyGame.Manager.BulletManager.Update(gameTime);
			if (MyGame.Manager.BulletManager.BulletTest.Count > 0)
			{
				IList<Bullet> bulletTest = MyGame.Manager.BulletManager.BulletTest;
				for (Byte testIndex = 0; testIndex < bulletTest.Count; testIndex++)
				{
					Bullet bullet = bulletTest[testIndex];
					SByte slotID = MyGame.Manager.CollisionManager.DetermineEnemySlot(bullet.Position);

					// Check to ensure bullet will something.
					if (Constants.INVALID_INDEX == slotID)
					{
						continue;
					}
					// Check to ensure bullet in same slot as enemy.
					if (!MyGame.Manager.EnemyManager.EnemyDict.ContainsKey((Byte)slotID))
					{
						continue;
					}
				}
			}
			//if (MyGame.Manager.CollisionManager.BulletCollisionList.Count > 0)
			//{
			//    // Check collisions here.
			//}


			// ENEMYS.
			// Update enemies and test collisions.
			MyGame.Manager.EnemyManager.Update(gameTime);
			if (MyGame.Manager.EnemyManager.EnemyTest.Count > 0)
			{
				// Enemy has maxed out frames so check for collision.
				checkLevelComplete = true;

				LargeTarget target = MyGame.Manager.SpriteManager.LargeTarget;
				IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;

				for (Byte testIndex = 0; testIndex < enemyTest.Count; testIndex++)
				{
					Enemy enemy = enemyTest[testIndex];
					if (!isGodMode)
					{
						// First check if enemy instantly kills target.
						Boolean test = CheckEnemyKillTarget(enemy, target);

						// Instant death!	Game Over.
						if (test)
						{
							// Do NOT reset enemy here as we want to see Target killed by Enemy!
							return (Int32) ScreenType.Dead;
						}

						// Enemy not kill target but missed so increment miss total.
						MyGame.Manager.ScoreManager.IncrementMisses();
						if (MyGame.Manager.ScoreManager.MissesTotal >= Constants.MAX_MISSES)
						{
							// If maximum misses then game over.
							enemy.Reset();
							return (Int32) ScreenType.Dead;
						}
					}

					// Finally, check if anymore enemies to spawn...
					Byte enemyID = enemy.ID;
					Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
					if (!check)
					{
						MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					}
				}
			}



			// TODO delete
			//if (MyGame.Manager.EnemyManager.EnemyTest.Count > 0)
			//{
			//    Boolean collision = MyGame.Manager.CollisionManager.CheckOne();
			//    if (collision)
			//    {
			//        return (Int32)ScreenType.Over;
			//    }
			//}
			//MyGame.Manager.EnemyManager.CheckAllEnemies();
			//if (MyGame.Manager.CollisionManager.EnemysCollisionList.Count > 0)
			//{
			//    // Check collisions here.
			//}


			// Update fire icon.
			Byte canShootIndex = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateFireIcon(canShootIndex);


			// Finally, check to see if all enemies finished = level complete.
			if (checkLevelComplete)
			{
				Boolean noMoreEnemies = MyGame.Manager.EnemyManager.CheckEnemiesNone();
				if (noMoreEnemies)
				{
					return (Int32)ScreenType.Finish;
				}
			}

			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		private static Boolean CheckEnemyKillTarget(Enemy enemy, LargeTarget target)
		{
			Boolean test = MyGame.Manager.CollisionManager.BoxesCollision(enemy.Position, target.Position);
			if (!test)
			{
				return false;
			}

			test = MyGame.Manager.CollisionManager.ColorCollision(enemy.Position, target.Position);
			if (!test)
			{
				return false;
			}

			return true;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.DebugManager.Draw();		// TODO delete
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
