using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreenPlay, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			//LoadTextData();
			UpdateGrid = false;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			base.LoadContent();

			// TODO delete
			MyGame.Manager.BulletManager.Reset(2, 100, 200);

			Byte enemySpawn = MyGame.Manager.ConfigManager.GlobalConfigData.EnemySpawn;	// 1;  // TODO level config
			Byte enemyTotal = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyTotal;	// 1;  // TODO level config
			MyGame.Manager.EnemyManager.Reset(LevelType, enemySpawn, 1000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.ExplosionManager.Reset(enemySpawn, MyGame.Manager.ConfigManager.GlobalConfigData.ExplodeDelay);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			CheckLevelComplete = false;
			NextScreen = CurrScreen;

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			DetectBullets();
			UpdateBullets(gameTime);
			VerifyBullets();

			// Explosions.
			UpdateExplosions(gameTime);
			VerifyExplosions();

			// Enemies.
			UpdateEnemies(gameTime);
			VerifyEnemies();
			if (NextScreen != CurrScreen)
			{
				return (Int32)NextScreen;
			}

			// Icons.
			UpdateIcons();

			// Score.
			UpdateScore(gameTime);

			// Summary.
			UpdateLevel();
			if (NextScreen != CurrScreen)
			{
				return (Int32)NextScreen;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();
			MyGame.Manager.DebugManager.Draw();
		}

	}
}