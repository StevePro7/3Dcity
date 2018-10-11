using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DeadScreen : BaseScreenSelect, IScreen
	{
		private Vector2 deathPosition;
		private String deathText;
		private UInt16 delay1;
		private UInt16 delay2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = new Vector2[2];
			BackedPositions[0] = new Vector2(290, 197 + Constants.GameOffsetY);
			BackedPositions[1] = new Vector2(290, 217 + Constants.GameOffsetY);

			deathPosition = MyGame.Manager.TextManager.GetTextPosition(15, 11);
			delay1 = MyGame.Manager.ConfigManager.GlobalConfigData.DeadDelay;

			Boolean unlimitedCont = MyGame.Manager.ConfigManager.GlobalConfigData.UnlimitedCont;
			NextScreen = unlimitedCont ? NextScreen = ScreenType.Cont : NextScreen = ScreenType.Over;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SoundManager.StopMusic();

			Boolean miss = Constants.MAX_MISSES == MyGame.Manager.ScoreManager.MissesTotal;
			deathText = miss ? Globalize.DEAD_OPTION1 : Globalize.DEAD_OPTION2;
			delay2 = miss ? (UInt16) 600 : Constants.SLIGHT_PAUSE;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);

			// Initial pause.
			if (Timer <= delay2)
			{
				return (Int32)CurrScreen;
			}

			// Now can check to pro actively goto next screen.
			Boolean status = MyGame.Manager.InputManager.StatusBar();
			if (status)
			{
				return (Int32)NextScreen;
			}

			// Time expired so advance.
			if (Timer > delay1)
			{
				return (Int32) NextScreen;
			}

			// Ensure sound effect once.
			if (Flag1)
			{
				return (Int32)CurrScreen;
			}

			MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Aaargh);
			Flag1 = true;

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);
			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();
			DrawBacked();

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();

			if (!Flag1)
			{
				return;
			}

			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(deathText, deathPosition);
		}

	}
}
