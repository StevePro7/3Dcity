using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LevelScreen : BaseScreenSelect, IScreen
	{
		private readonly String[] cursorOptions = new string[3] { Globalize.CURSOR_LEFTS, "  ", Globalize.CURSOR_RIGHT };
		private Vector2 levelNamePosition;
		private Vector2 levelTextPosition;

		private Byte levelIndex;
		private Byte maximLevel;
		private String levelName;
		private String levelValu;

		public override void Initialize()
		{
			MyGame.Manager.DebugManager.Reset();
			base.Initialize();

			CursorPositions = new Vector2[1];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(16, 11);
			NextScreen = ScreenType.Load;
			PrevScreen = ScreenType.Diff;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			levelNamePosition = MyGame.Manager.TextManager.GetTextPosition(19, 11);
			levelTextPosition = MyGame.Manager.TextManager.GetTextPosition(12, 11);

			maximLevel = MyGame.Manager.LevelManager.MaximLevel;
			levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			PopulateLevelData(levelIndex);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return (Int32) PrevScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
				MyGame.Manager.LevelManager.SetLevelIndex(levelIndex);
				return (Int32) NextScreen;
			}
			if (Flag1)
			{
				return (Int32) CurrScreen;
			}

			UpdateFlag2(gameTime);
			if (IsMoving)
			{
				return (Int32) CurrScreen;
			}

			DetectFire();
			if (Flag1)
			{
				return (Int32) CurrScreen;
			}

			DetectMove();
			if (MoveValue < 0)
			{
				levelIndex--;
				if (levelIndex >= Byte.MaxValue)
				{
					levelIndex = (Byte)(maximLevel - 1);
				}

				PopulateLevelData(levelIndex);
			}
			if (MoveValue > 0)
			{
				levelIndex++;
				if (levelIndex >= maximLevel)
				{
					levelIndex = 0;
				}

				PopulateLevelData(levelIndex);
			}

			return (Int32) CurrScreen;
		}

		private void PopulateLevelData(Byte theLevelIndex)
		{
			MyGame.Manager.LevelManager.SetLevelIndex(theLevelIndex);

			levelName = MyGame.Manager.LevelManager.LevelName;
			levelValu = MyGame.Manager.LevelManager.LevelValu;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();
			MyGame.Manager.RenderManager.DrawTitle();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.SpriteManager.DrawCursor();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawText(cursorOptions[MoveIndex], CursorPositions[0]);
			MyGame.Manager.TextManager.DrawText(levelName, levelNamePosition);
			MyGame.Manager.TextManager.DrawText(levelValu, levelTextPosition);
			MyGame.Manager.TextManager.DrawInstruct();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
