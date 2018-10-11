using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ResumeScreen : BaseScreenPlay, IScreen
	{
		private UInt16 bigDelay, smlDelay;
		private UInt16 timer;
		private Boolean flag;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;

			// TODO make delay values configurable!
			bigDelay = MyGame.Manager.ConfigManager.GlobalConfigData.ResumeDelay;
			smlDelay = 200;

			//PrevScreen = ScreenType.Ready;
			PrevScreen = ScreenType.Play;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			// Resume screen cannot die not matter what!
			Invincibile = true;
			NextScreen = CurrScreen;

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

			// Check status bar to fast forward.
			Boolean statusBar = MyGame.Manager.InputManager.StatusBar();
			if (statusBar)
			{
				return (Int32) NextScreen;
			}

			// Be careful on this screen as re-use logic from PlayScreen:
			// Next could be Dead but won't because you'll be invincible;
			// Next could be Finish if all enemies complete during blink.
			// Therefore, previous should be used to go "back" to Ready!
			UpdateTimer(gameTime);
			if (Timer > smlDelay)
			{
				timer += Timer;
				if (timer > bigDelay)
				{
					return (Int32) PrevScreen;
				}

				Timer -= smlDelay;
				flag = !flag;
			}

			// Begin common code...
			CheckLevelComplete = false;

			// Target.
			DetectTarget(gameTime);

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
