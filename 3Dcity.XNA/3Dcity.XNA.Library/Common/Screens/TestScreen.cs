using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
	{
		private SByte number;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			number = Constants.INVALID_INDEX;
		}

		public override void LoadContent()
		{

			LevelType levelType = MyGame.Manager.StateManager.LevelType;
			const Byte enemySpawn = 2;
			const Byte enemyTotal = 2;
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 2000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.ExplosionManager.Reset(8, 100);

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

				// Iterate all enemy ships to test and add to misses.
				IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;
				for (Byte testIndex = 0; testIndex < enemyTest.Count; testIndex++)
				{
					// TODO update score manager => 1x miss per enemy here!
					// if (misses >= 4) then return game over

					Enemy enemy = enemyTest[testIndex];
					MyGame.Manager.EnemyManager.CheckThisEnemy(enemy);

					Byte enemyAvoid = MyGame.Manager.EnemyManager.EnemyAvoid;
					Byte enemyKills = MyGame.Manager.EnemyManager.EnemyKills;
					Byte enemyTotal = MyGame.Manager.EnemyManager.EnemyTotal;
					if (enemyAvoid + enemyKills >= enemyTotal)
					{
						enemy.None();
					}

					Boolean gameover = MyGame.Manager.EnemyManager.CheckEnemiesNone();
					if (gameover)
					{
						return (Int32) ScreenType.Cont; // TODO actually finished level.
					}
					else
					{
						MyGame.Manager.EnemyManager.SpawnOneEnemy(enemy.ID);
					}
				}

				

			}
			//MyGame.Manager.EnemyManager.CheckAllEnemies();
			if (MyGame.Manager.CollisionManager.EnemysCollisionList.Count > 0)
			{
				// Check collisions here.
			}


			MyGame.Manager.ExplosionManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();


			// Sprite sheet #02.
			MyGame.Manager.DebugManager.Draw();

			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			MyGame.Manager.ExplosionManager.Draw();


			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
}
