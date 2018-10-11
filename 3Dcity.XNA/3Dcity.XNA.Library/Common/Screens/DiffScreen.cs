using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DiffScreen : BaseScreenSelect, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();

			CursorPositions = new Vector2[2];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			CursorPositions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);
			NextScreen = ScreenType.Level;
			PrevScreen = ScreenType.Title;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();
			SelectType = (Byte)MyGame.Manager.LevelManager.LevelType;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Check to go back first.
			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				return (Int32)PrevScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
				MyGame.Manager.LevelManager.SetLevelType((LevelType)SelectType);
				return (Int32)NextScreen;
			}
			if (Flag1)
			{
				return (Int32)CurrScreen;
			}

			UpdateFlag2(gameTime);
			if (IsMoving)
			{
				return (Int32)CurrScreen;
			}

			DetectFire();
			if (Flag1)
			{
				return (Int32)CurrScreen;
			}

			DetectMove();
			if (0 != MoveValue)
			{
				SelectType = (Byte)(1 - SelectType);
				MyGame.Manager.LevelManager.SetLevelType((LevelType)SelectType);
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();
			MyGame.Manager.RenderManager.DrawTitle();

			// Sprite sheet #02.
			DrawSheet02();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawCursor(CursorPositions[SelectType]);
			MyGame.Manager.TextManager.DrawInstruct();
		}

	}
}
