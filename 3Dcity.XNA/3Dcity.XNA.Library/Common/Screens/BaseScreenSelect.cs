using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public class BaseScreenSelect : BaseScreen
	{
		protected Vector2[] CursorPositions { get; set; }
		protected Vector2 SpritePosition { get; private set; }

		protected UInt16 SelectDelay { get; private set; }
		protected UInt16 SelectType { get; set; }

		protected Byte IconIndex;
		protected Byte MoveIndex;
		protected Boolean Flag1;
		protected Boolean Flag2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			SelectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;
			UpdateGrid = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			IconIndex = 0;
			MoveIndex = 1;

			Vector2 spritePosition = SpritePosition;
			spritePosition = MyGame.Manager.SpriteManager.SmallTarget.Position;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;

			Flag1 = Flag2 = false;
		}

		protected void DrawSheet01()
		{
			MyGame.Manager.IconManager.DrawControls();
		}

		protected void DrawSheet02()
		{
			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.DrawCursor();
		}

		protected void DrawText()
		{
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}