using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class QuitScreen : BaseScreenSelect, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();

			CursorPositions = new Vector2[2];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(14, 11);
			CursorPositions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();
			base.LoadContent();
			SelectType = 1;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
				ScreenType screenType = SelectType == 0 ? ScreenType.Over : ScreenType.Resume;
				return (Int32)screenType;
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
			return (Int32)CurrScreen;
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


			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawCursor(CursorPositions[SelectType]);
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
		}

	}
}
