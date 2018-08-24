using System;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
	{
		private Vector2[] boxPositions;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			boxPositions = GetBoxPositions();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.Draw();
			for (Byte index = 0; index < Constants.MAX_ENEMY; index++)
			{
				Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			}
			
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private Vector2[] GetBoxPositions()
		{
			boxPositions = new Vector2[Constants.MAX_ENEMY];
			boxPositions[0] = new Vector2(160 * 0, 80);
			boxPositions[1] = new Vector2(160 * 1, 80);
			boxPositions[2] = new Vector2(160 * 2, 80);
			boxPositions[3] = new Vector2(160 * 3, 80);
			boxPositions[4] = new Vector2(160 * 4, 80);

			const Byte offset = 190;
			boxPositions[5] = new Vector2(160 * 0 + offset, 280);
			boxPositions[6] = new Vector2(160 * 1 + offset, 280);
			boxPositions[7] = new Vector2(160 * 2 + offset, 280);

			return boxPositions;
		}

	}
}
