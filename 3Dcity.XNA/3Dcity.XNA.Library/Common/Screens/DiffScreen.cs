using System;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DiffScreen : BaseScreen, IScreen
	{
		private Vector2[] cursorPositions;
		private LevelType levelType;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			cursorPositions = GetCursorPositions();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.DrawCursor();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private static Vector2[] GetCursorPositions()
		{
			Vector2[] positions = new Vector2[2];
			positions[0] = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			positions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);
			return positions;
		}

	}
}
