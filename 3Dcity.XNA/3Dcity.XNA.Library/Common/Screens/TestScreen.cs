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
			for (Byte index = 0; index < Constants.MAX_ENEMY_SPAWN; index++)
			{
				Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			}

			Vector2 enemyPos = new Vector2(800-120, 80);
			Rectangle enemyRect = MyGame.Manager.ImageManager.EnemyRectangles[7];
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemyPos, enemyRect, Color.White);

			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private Vector2[] GetBoxPositions()
		{
			const Single hi = 80 + Constants.GameOffsetY;
			const Single lo = 280 + Constants.GameOffsetY;

			boxPositions = new Vector2[Constants.MAX_ENEMY_SPAWN];
			boxPositions[0] = new Vector2(160 * 0, hi);
			boxPositions[1] = new Vector2(160 * 1, hi);
			boxPositions[2] = new Vector2(160 * 2, hi);
			boxPositions[3] = new Vector2(160 * 3, hi);
			boxPositions[4] = new Vector2(160 * 4, hi);

			const Byte offset = 190;
			boxPositions[5] = new Vector2(160 * 0 + offset, lo);
			boxPositions[6] = new Vector2(160 * 1 + offset, lo);
			boxPositions[7] = new Vector2(160 * 2 + offset, lo);

			return boxPositions;
		}

	}
}
