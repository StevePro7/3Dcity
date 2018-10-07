using System;
using WindowsGame.Common.Data;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ResumeScreen : BaseScreen, IScreen
	{
		private LevelType levelType;
		private Byte levelIndex;
		private LevelConfigData levelConfigData;
		private Boolean invincibile;
		private Boolean checkLevelComplete;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			UpdateGrid = false;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			// Load the configuration for level type + index.
			levelType = MyGame.Manager.LevelManager.LevelType;
			levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(levelType, levelIndex);
			levelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			Boolean isGodMode = MyGame.Manager.StateManager.IsGodMode;
			invincibile = isGodMode || levelConfigData.BonudLevel;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			
			
			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
