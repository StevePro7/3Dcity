using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreenPlay, IScreen
	{
		private UInt16 readyDelay;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;
			readyDelay = MyGame.Manager.ConfigManager.GlobalConfigData.ReadyDelay;
			NextScreen = ScreenType.Play;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();
			

			MyGame.Manager.RenderManager.SetGridDelay((UInt16)(LevelConfigData.GridDelay * 2));

			MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Ready);

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

			UpdateTimer(gameTime);
			if (Timer >= readyDelay)
			{
				return (Int32) NextScreen;
			}

			// Target.
			DetectTarget(gameTime);

			// Score.
			UpdateScore(gameTime);

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();

			// Text data last!
			DrawTextCommon();
			MyGame.Manager.ScoreManager.DrawBlink();
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
}
