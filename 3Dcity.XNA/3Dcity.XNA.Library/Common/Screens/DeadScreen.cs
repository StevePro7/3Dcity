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
		private bool flag;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = new Vector2[2];
			BackedPositions[0] = new Vector2(290, 197 + Constants.GameOffsetY);
			BackedPositions[1] = new Vector2(290, 217 + Constants.GameOffsetY);

			deathPosition = MyGame.Manager.TextManager.GetTextPosition(15, 11);

			delay1 = MyGame.Manager.ConfigManager.GlobalConfigData.DeadDelay;

			// TODO Unlimited continues?
			NextScreen = ScreenType.Cont;
			//NextScreen = ScreenType.Over;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			Boolean miss = Constants.MAX_MISSES == MyGame.Manager.ScoreManager.MissesTotal;
			deathText = miss ? Globalize.DEAD_OPTION1 : Globalize.DEAD_OPTION2;
			delay2 = miss ? (UInt16) 600 : (UInt16) 100;

			flag = false;
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
			if (Timer > delay1)
			{
				return (Int32) NextScreen;
			}

			if (flag)
			{
				return (Int32)CurrScreen;
			}

			if (Timer > delay2)
			{
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Aaargh);
				flag = true;
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

			if (!flag)
			{
				return;
			}

			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(deathText, deathPosition);
		}

	}
}
