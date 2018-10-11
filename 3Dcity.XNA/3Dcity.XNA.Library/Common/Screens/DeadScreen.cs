using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DeadScreen : BaseScreenSelect, IScreen
	{
		private UInt16 bigDelay, medDelay, smlDelay;

		private Vector2 deathPosition;
		private String deathText;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = new Vector2[2];
			BackedPositions[0] = new Vector2(290, 197 + Constants.GameOffsetY);
			BackedPositions[1] = new Vector2(290, 217 + Constants.GameOffsetY);

			deathPosition = MyGame.Manager.TextManager.GetTextPosition(15, 11);
			bigDelay = MyGame.Manager.ConfigManager.GlobalConfigData.DeadDelay;
			medDelay = 1500;

			Boolean unlimitedCont = MyGame.Manager.ConfigManager.GlobalConfigData.UnlimitedCont;
			NextScreen = unlimitedCont ? ScreenType.Cont : ScreenType.Over;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SoundManager.StopMusic();

			Boolean miss = Constants.MAX_MISSES == MyGame.Manager.ScoreManager.MissesTotal;
			deathText = miss ? Globalize.DEAD_OPTION1 : Globalize.DEAD_OPTION2;
			smlDelay = miss ? (UInt16) 600 : Constants.SLIGHT_PAUSE;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			UpdateTimer(gameTime);

			// Initial pause.
			if (Timer <= smlDelay)
			{
				return (Int32) CurrScreen;
			}

			if (!Flag1)
			{
				// Ensure sound effect once.
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Aaargh);
				Flag1 = true;
			}

			if (Timer <= medDelay)
			{
				return (Int32) CurrScreen;
			}

			// Now can check to pro actively goto next screen.
			Boolean status = MyGame.Manager.InputManager.StatusBar();
			if (status)
			{
				return (Int32) NextScreen;
			}

			// Time expired so advance.
			if (Timer > bigDelay)
			{
				return (Int32) NextScreen;
			}

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
