﻿using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LoadScreen : BaseScreenPlay, IScreen
	{
		private Vector2 enemyTotalPosition;
		private String enemyTotalText;

		private Vector2 levelNamePosition;
		private Vector2 levelTextPosition;
		private String levelName;
		private String levelValu;
		private UInt16 loadDelay;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(235, 195, 385, 217);

			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;
			NextScreen = ScreenType.Ready;

			enemyTotalPosition = MyGame.Manager.TextManager.GetTextPosition(25, 10);
			levelNamePosition = MyGame.Manager.TextManager.GetTextPosition(19, 11);
			levelTextPosition = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			loadDelay = MyGame.Manager.ConfigManager.GlobalConfigData.LoadDelay;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			LevelType = MyGame.Manager.LevelManager.LevelType;
			LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(LevelType, LevelIndex);
			LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			// Resets all relevant score level info,
			MyGame.Manager.ScoreManager.ResetLevel();

			// Bullets.
			MyGame.Manager.BulletManager.Reset(LevelConfigData.BulletMaxim, LevelConfigData.BulletFrame, LevelConfigData.BulletShoot);

			// Enemies.
			MyGame.Manager.EnemyManager.Reset(LevelType, LevelConfigData);

			// Explosions.
			MyGame.Manager.ExplosionManager.Reset(LevelConfigData.EnemySpawn, LevelConfigData.ExplodeDelay);

			levelName = MyGame.Manager.LevelManager.LevelName;
			levelValu = MyGame.Manager.LevelManager.LevelValu;
			base.LoadContent();

			// Must set this after base load content.
			enemyTotalText = EnemyTotal.ToString().PadLeft(3, '0');

			MyGame.Manager.RenderManager.SetGridDelay(LevelConfigData.GridDelay);
			MyGame.Manager.StateManager.SetKillSpace(Vector2.Zero);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.SpriteManager.LargeTarget.SetHomeSpot();
			MyGame.Manager.SoundManager.StopMusic();
			MyGame.Manager.SoundManager.PlayMusic(SongType.GameMusic);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);
			if (Timer >= loadDelay)
			{
				return (Int32)NextScreen;
			}

			Boolean statusBar = MyGame.Manager.InputManager.StatusBar();
			if (statusBar)
			{
				return (Int32)NextScreen; 
			}

			// Target.
			DetectTarget(gameTime);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();
			DrawBacked();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(levelName, levelNamePosition);
			MyGame.Manager.TextManager.DrawText(levelValu, levelTextPosition);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, enemyTotalText, enemyTotalPosition, Color.White);
		}

	}
}
