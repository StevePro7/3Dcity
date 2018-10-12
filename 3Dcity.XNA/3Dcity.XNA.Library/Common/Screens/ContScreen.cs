using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ContScreen : BaseScreenSelect, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();

			CursorPositions = new Vector2[2];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(14, 11);
			CursorPositions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);

			CalcFourBorders(255, 197, 345, 217);

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();
			NextScreen = CurrScreen;
			SelectType = 0;

			Killspace = MyGame.Manager.StateManager.KillSpace;
			MyGame.Manager.SpriteManager.KillEnemy.SetPosition(Killspace);

			MyGame.Manager.SoundManager.PlayMusic(SongType.ContMusic, true);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
				// If game over then leave things as they are...
				NextScreen = SelectType == 0 ? ScreenType.Resume : ScreenType.Over;
				if (ScreenType.Over == NextScreen)
				{
					return (Int32) NextScreen;
				}

				// Otherwise clear kill enemy and misses.
				MyGame.Manager.StateManager.SetKillSpace(Vector2.Zero);
				MyGame.Manager.ScoreManager.ResetMisses();
				
				// Edge case; check if this was last enemy in level...!
				Boolean noMoreEnemies = MyGame.Manager.EnemyManager.CheckEnemiesNone();
				if (noMoreEnemies)
				{
					NextScreen = ScreenType.Finish;
				}

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
				MyGame.Manager.SoundManager.StopMusic();
				return (Int32) CurrScreen;
			}

			DetectMove();
			if (0 != MoveValue)
			{
				SelectType = (Byte)(1 - SelectType);
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

			// Draw dead enemy on instant death only.
			if (Vector2.Zero != Killspace)
			{
				MyGame.Manager.SpriteManager.KillEnemy.Draw();
			}

			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawCursor(CursorPositions[SelectType]);
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
		}

	}
}
