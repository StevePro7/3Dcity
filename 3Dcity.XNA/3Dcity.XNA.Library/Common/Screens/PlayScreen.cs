using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreenPlay, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;
			PrevScreen = ScreenType.Quit;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();
			NextScreen = CurrScreen;
			MyGame.Manager.RenderManager.SetGridDelay(LevelConfigData.GridDelay);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			// Check to go back first.
			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				MyGame.Manager.SoundManager.PauseMusic();
				return (Int32)PrevScreen;
			}


			// Log delta to monitor performance!
#if DEBUG
			//string time = gameTime.ElapsedGameTime.TotalSeconds.ToString();
			//MyGame.Manager.Logger.Info(time);
			//Console.WriteLine(time);
			//System.Diagnostics.Debug.WriteLine(time);
#endif


			// Begin common code...
			CheckLevelComplete = false;

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
				return (Int32) NextScreen;
			}

			// Icons.
			UpdateIcons();

			// Score.
			UpdateScore(gameTime);

			// Summary.
			UpdateLevel();
			if (NextScreen != CurrScreen)
			{
				return (Int32) NextScreen;
			}

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
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();

			// Text data last!
			DrawTextCommon();
			MyGame.Manager.ScoreManager.DrawBlink();
		}

	}
}
