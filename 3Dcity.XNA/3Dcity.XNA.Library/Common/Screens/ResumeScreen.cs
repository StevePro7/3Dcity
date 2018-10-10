using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ResumeScreen : BaseScreenPlay, IScreen
	{
		private UInt16 delay1, delay2, timer;
		private Boolean flag;

		public override void Initialize()
		{
			base.Initialize();
			//UpdateGrid = true;
			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;

			// TODO make delay values configurable!
			delay1 = 200;
			delay2 = 5000;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			// Resume screen cannot die not matter what!
			Invincibile = true;
			timer = 0;
			flag = true;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);
			if (Timer > delay1)
			{
				timer += Timer;
				if (timer > delay2)
				{
					return (Int32)ScreenType.Ready;
				}

				Timer -= delay1;
				flag = !flag;
			}

			// Begin common code...
			CheckLevelComplete = false;

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			//DetectBullets();
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
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02(flag);
			if (flag)
			{
				MyGame.Manager.SpriteManager.LargeTarget.Draw();
			}

			// Text data last!
			DrawTextCommon();
			MyGame.Manager.ScoreManager.DrawBlink();
		}

	}
}
