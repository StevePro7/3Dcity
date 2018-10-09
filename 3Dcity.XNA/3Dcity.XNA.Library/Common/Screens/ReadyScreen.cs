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
			NextScreen = ScreenType.Play;

			readyDelay = MyGame.Manager.ConfigManager.GlobalConfigData.ReadyDelay;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			MyGame.Manager.RenderManager.SetGridDelay((UInt16)(LevelConfigData.GridDelay * 2));

			// TODO "Get Ready!"
			MyGame.Manager.SoundManager.PlaySoundEffect();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);
			if (Timer >= readyDelay)
			{
				return (Int32)NextScreen;
			}

			Boolean statusBar = MyGame.Manager.InputManager.StatusBar();
			if (statusBar)
			{
				return (Int32)NextScreen;
			}

			DetectTarget(gameTime);

			//MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
}
