using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LevelScreen : BaseScreen, IScreen
	{
		private Vector2 cursorPosition;
		private Vector2 levelNamePosition;
		private Vector2 levelTextPosition;

		private IList<String> levelNames;
		private Byte levelIndex;
		private String levelName;
		private String levelText;

		private Byte iconIndex, moveIndex;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			levelNames = MyGame.Manager.LevelManager.LevelNames;
			levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			PopulateLevelData(levelIndex);

			cursorPosition = MyGame.Manager.TextManager.GetTextPosition(16, 11);
			levelNamePosition = MyGame.Manager.TextManager.GetTextPosition(19, 11);
			levelTextPosition = MyGame.Manager.TextManager.GetTextPosition(12, 11);

			iconIndex = 0;
			moveIndex = 1;

			flag1 = flag2 = false;
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.DrawCursor();

			// Individual texture.
			MyGame.Manager.RenderManager.DrawTitle();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);

			MyGame.Manager.TextManager.DrawText(levelName, levelNamePosition);
			MyGame.Manager.TextManager.DrawText(levelText, levelTextPosition);
		}

		private void PopulateLevelData(Byte theLevelIndex)
		{
			levelName = levelNames[theLevelIndex];
			levelText = String.Format("[{0}]", (theLevelIndex + 1).ToString().PadLeft(2, '0'));
		}

	}
}
