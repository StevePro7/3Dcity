using System;
using System.Collections.Generic;
using WindowsGame.Common.Data;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreenPlay : BaseScreen
	{
		protected LevelType LevelType { get; private set; }
		protected Byte LevelIndex { get; private set; }
		protected LevelConfigData LevelConfigData { get; private set; }
		protected Boolean Invincibile { get; set; }
		protected Boolean CheckLevelComplete { get; set; }

		//public override void Initialize()
		//{
		//    base.Initialize();
		//}

		public override void LoadContent()
		{
			// Load the configuration for level type + index.
			LevelType = MyGame.Manager.LevelManager.LevelType;
			LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(LevelType, LevelIndex);
			LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			Boolean isGodMode = MyGame.Manager.StateManager.IsGodMode;
			Invincibile = isGodMode || LevelConfigData.BonusLevel;

			// Bullets.
			Byte bulletMaximumNum = LevelConfigData.BulletMaximumNum;
			UInt16 bulletFrameDelay = LevelConfigData.BulletFrameDelay;
			UInt16 bulletShootDelay = LevelConfigData.BulletShootDelay;
			MyGame.Manager.BulletManager.Reset(bulletMaximumNum, bulletFrameDelay, bulletShootDelay);

			base.LoadContent();
		}

		//public override Int32 Update(GameTime gameTime)
		//{
		//    base.Update(gameTime);
		//    if (GamePause)
		//    {
		//        return (Int32)CurrScreen;
		//    }

		//    return (Int32)CurrScreen;
		//}


		// Target.
		protected static void DetectTarget(GameTime gameTime)
		{
			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);
		}


		// Bullets.
		protected static void DetectBullets()
		{
			// Test shooting enemy ships.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX == bulletIndex)
				{
					return;
				}

				// TODO check!
				MyGame.Manager.SoundManager.PlaySoundEffect();

				Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
				MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
			}
		}
		protected static void UpdateBullets(GameTime gameTime)
		{
			MyGame.Manager.BulletManager.Update(gameTime);
		}
		protected void VerifyBullets()
		{
			if (0 == MyGame.Manager.BulletManager.BulletTest.Count)
			{
				return;
			}

			IList<Bullet> bulletTest = MyGame.Manager.BulletManager.BulletTest;
			for (Byte testIndex = 0; testIndex < bulletTest.Count; testIndex++)
			{
				Bullet bullet = bulletTest[testIndex];
				SByte testID = MyGame.Manager.CollisionManager.DetermineEnemySlot(bullet.Position);

				// Check to ensure bullet will something.
				if (Constants.INVALID_INDEX == testID)
				{
					continue;
				}

				// Check to ensure bullet in same slot as enemy.
				Byte slotID = (Byte)testID;
				if (!MyGame.Manager.EnemyManager.EnemyDict.ContainsKey(slotID))
				{
					continue;
				}

				// It could be possible that the enemy in this slot is already dead!
				Enemy enemy = MyGame.Manager.EnemyManager.EnemyDict[slotID];
				if (EnemyType.Dead == enemy.EnemyType)
				{
					continue;
				}

				// Can kill initial enemy [at frame count = 0] because enemy will be "hidden".
				Byte enemyCount = enemy.FrameCount;
				if (0 == enemy.FrameCount)
				{
					continue;
				}

				// Check if bullet collides with enemy.
				Byte enemyFrame = enemy.FrameIndex;
				Boolean collide = MyGame.Manager.CollisionManager.BulletCollideEnemy(enemy.Position, bullet.Position, LevelType, enemyFrame);
				if (!collide)
				{
					continue;
				}

				// Collision!	Enemy dead and trigger explode...
				MyGame.Manager.ScoreManager.UpdateGameScore(enemyFrame);

				ExplodeType explodeType = enemy.FrameIndex < 4 ? ExplodeType.Small : ExplodeType.Big;
				Byte enemyID = enemy.ID;
				MyGame.Manager.ExplosionManager.LoadContent(enemyID, explodeType);
				MyGame.Manager.ExplosionManager.Explode(enemyID, explodeType, enemy.Position);
				enemy.Dead();
			}
		}


		// Explosions.
		protected static void UpdateExplosions(GameTime gameTime)
		{
			MyGame.Manager.ExplosionManager.Update(gameTime);
		}
		protected void VerifyExplosions()
		{
			if (0 == MyGame.Manager.ExplosionManager.ExplosionTest.Count)
			{
				return;
			}

			// Iterate all enemy ships to test and add to misses.
			IList<Byte> explosionTest = MyGame.Manager.ExplosionManager.ExplosionTest;
			for (Byte testIndex = 0; testIndex < explosionTest.Count; testIndex++)
			{
				Byte enemyID = explosionTest[testIndex];
				Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
				if (!check)
				{
					MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
				}
				else
				{
					CheckLevelComplete = true;
				}
			}
		}


		// Enemies.
		protected static void UpdateEnemies(GameTime gameTime)
		{
			MyGame.Manager.EnemyManager.Update(gameTime);
		}
		protected void VerifyEnemies()
		{
			if (0 == MyGame.Manager.EnemyManager.EnemyTest.Count)
			{
				return;
			}

			// Enemy has maxed out frames so check for collision.
			LargeTarget target = MyGame.Manager.SpriteManager.LargeTarget;
			IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;

			for (Byte testIndex = 0; testIndex < enemyTest.Count; testIndex++)
			{
				Enemy enemy = enemyTest[testIndex];
				if (!Invincibile)
				{
					// First check if enemy instantly kills target.
					Boolean test = CheckEnemyKillTarget(enemy, target);

					// Instant death!	Game Over.
					if (test)
					{
						// Do NOT reset enemy here as we want to see Target killed by Enemy!
						NextScreen = ScreenType.Dead;
						return;
						//return (Int32)ScreenType.Dead;
					}

					// Enemy not kill target but missed so increment miss total.
					MyGame.Manager.ScoreManager.IncrementMisses();
					if (MyGame.Manager.ScoreManager.MissesTotal >= Constants.MAX_MISSES)
					{
						// If maximum misses then game over.
						enemy.Reset();
						NextScreen = ScreenType.Dead;
						return;
						//return (Int32)ScreenType.Dead;
					}
				}

				// Finally, check if anymore enemies to spawn...
				Byte enemyID = enemy.ID;
				Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
				if (!check)
				{
					MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
				}
				else
				{
					CheckLevelComplete = true;
				}
			}
		}
		private static Boolean CheckEnemyKillTarget(BaseSprite enemy, BaseSprite target)
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


		// Icons.
		protected static void UpdateIcons()
		{
			Byte canShootIndex = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateFireIcon(canShootIndex);
		}


		// Score.
		protected static void UpdateScore(GameTime gameTime)
		{
			MyGame.Manager.ScoreManager.Update(gameTime);
		}


		// Summary.
		protected void UpdateLevel()
		{
			if (!CheckLevelComplete)
			{
				return;
			}

			Boolean noMoreEnemies = MyGame.Manager.EnemyManager.CheckEnemiesNone();
			if (noMoreEnemies)
			{
				NextScreen = ScreenType.Finish;
				return;
				//return (Int32)ScreenType.Finish;
			}
		}


		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Individual texture.
			//MyGame.Manager.DebugManager.Draw();

			// Text data last!
			//MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}