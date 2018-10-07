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
		private UInt16 delay1, delay2;
		private Boolean flag;

		public override void Initialize()
		{
			base.Initialize();
			//LoadTextData();
			UpdateGrid = false;

			delay1 = 1000;
			delay2 = 5000;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			MyGame.Manager.DebugManager.Reset();

			// Load the configuration for level type + index.
			LevelType = MyGame.Manager.LevelManager.LevelType;
			LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(LevelType, LevelIndex);
			LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			Boolean isGodMode = MyGame.Manager.StateManager.IsGodMode;
			Invincibile = isGodMode || LevelConfigData.BonusLevel;

			// Bullets.
			Byte bulletMaxim = LevelConfigData.BulletMaxim;
			UInt16 bulletFrame = LevelConfigData.BulletFrame;
			UInt16 bulletShoot = LevelConfigData.BulletShoot;
			MyGame.Manager.BulletManager.Reset(bulletMaxim, bulletFrame, bulletShoot);
			// TODO delete
			MyGame.Manager.BulletManager.Reset(2, 100, 200);

			// Enemies.
			Byte enemySpawn = LevelConfigData.EnemySpawn;
			Byte enemyTotal = LevelConfigData.EnemyTotal;
			MyGame.Manager.EnemyManager.Reset(LevelType, enemySpawn, 1000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			// Explosions.
			UInt16 explodeDelay = LevelConfigData.ExplodeDelay;
			MyGame.Manager.ExplosionManager.Reset(LevelConfigData.EnemySpawn, explodeDelay);

			// Resume screen cannot die
			Invincibile = true;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);

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
			DrawSheet01();

			DrawSheet02();
			if (flag)
			{
				MyGame.Manager.SpriteManager.LargeTarget.Draw();
			}

			DrawText();
			//base.Draw();
			//MyGame.Manager.DebugManager.Draw();
		}

	}
}