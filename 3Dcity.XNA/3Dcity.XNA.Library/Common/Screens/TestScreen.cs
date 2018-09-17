﻿using System;
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
			Byte enemySpawn = MyGame.Manager.ConfigManager.GlobalConfigData.EnemySpawn;	// 1;
			Byte enemyTotal = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyTotal;	// 1;
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
					//MyGame.Manager.EnemyManager.CheckThisEnemy(enemy);
					Byte enemyID = enemy.ID;

					Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
					if (!check)
					{
						MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					}

					//Byte enemySpawn = MyGame.Manager.EnemyManager.EnemySpawn;
					//Byte enemyTotal = MyGame.Manager.EnemyManager.EnemyTotal;
					//if (enemySpawn >= enemyTotal)
					//{
					//    enemy.None();
					//}
					//else
					//{
					//    MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					//}
				}

				Boolean gameover = MyGame.Manager.EnemyManager.CheckEnemiesNone();
				if (gameover)
				{
					return (Int32)ScreenType.Cont; // TODO actually finished level!
				}
			}

			//MyGame.Manager.EnemyManager.CheckAllEnemies();
			if (MyGame.Manager.CollisionManager.EnemysCollisionList.Count > 0)
			{
				// Check collisions here.
			}

			number = MyGame.Manager.InputManager.Number();
			if (Constants.INVALID_INDEX != number)
			{
				// Retrieve slotID from bullet position
				// (determine if collision!)
				Byte slotID = (Byte)number;
				if (MyGame.Manager.EnemyManager.EnemyDict.ContainsKey(slotID))
				{
					Enemy enemy = MyGame.Manager.EnemyManager.EnemyDict[slotID];
					if (0 != enemy.FrameCount)
					{
						// Can kill initial enemy [at frame count = 0] because enemy will be "hidden".
						ExplodeType explodeType = enemy.FrameIndex < 5 ? ExplodeType.Small : ExplodeType.Big;
						MyGame.Manager.ExplosionManager.LoadContent(slotID, explodeType);
						MyGame.Manager.ExplosionManager.Explode(slotID, enemy.ID, enemy.Position);
						//enemy.Dead();
					}
				}
				else
				{
					MyGame.Manager.Logger.Info("miss " + (slotID).ToString());
				}
			}


			// EXPLOSIONS.
			MyGame.Manager.ExplosionManager.Update(gameTime);


			if (MyGame.Manager.ExplosionManager.ExplosionTest.Count > 0)
			{
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

					//MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
					//Byte enemySpawn = MyGame.Manager.EnemyManager.EnemySpawn;
					//Byte enemyTotal = MyGame.Manager.EnemyManager.EnemyTotal;
					//if (enemySpawn >= enemyTotal)
					//{
					//    enemy.None();
					//}
					//else
					//{
					//    MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					//}
				}
			}

			return (Int32)CurrScreen;
		}

		private void RespawnEnemy()
		{
			
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
