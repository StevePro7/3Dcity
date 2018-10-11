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

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = new Vector2[2];
			BackedPositions[0] = new Vector2(290, 197 + Constants.GameOffsetY);
			BackedPositions[1] = new Vector2(290, 217 + Constants.GameOffsetY);

			deathPosition = MyGame.Manager.TextManager.GetTextPosition(15, 11);
			deathText = Constants.MAX_MISSES == MyGame.Manager.ScoreManager.MissesTotal
				? Globalize.DEAD_OPTION1
				: Globalize.DEAD_OPTION2;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
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
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);
			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.TextManager.DrawText(deathText, deathPosition);
		}

	}
}
