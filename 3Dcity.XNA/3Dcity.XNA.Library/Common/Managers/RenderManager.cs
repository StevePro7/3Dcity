using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface IRenderManager 
	{
		void Initialize();
		void LoadContent();
		void UpdateStar(GameTime gameTime);
		void UpdateGrid(GameTime gameTime);
		void Draw();
		void DrawTitle();
		void DrawTitle(Vector2 position);
		void DrawBottom();
	}

	public class RenderManager : IRenderManager
	{
		private Rectangle[] gridRectangles;
		private Rectangle[] starRectangles;
		private Rectangle backRectangle;
		private Vector2 backPosition;
		private Vector2 starPosition;
		private Vector2 gridPosition;
		private Vector2 titlePosition;
		private Vector2 bottomPosition;
		//private Vector2 origin;				// TODO delete
		private UInt16 starTimer, starDelay;
		private UInt16 gridTimer, gridDelay;
		private Single rotation;
		private Byte starIndex, gridIndex;

		public void Initialize()
		{
			starDelay = MyGame.Manager.ConfigManager.GlobalConfigData.StarDelay;
			gridDelay = MyGame.Manager.ConfigManager.GlobalConfigData.GridDelay;
			starTimer = gridTimer = 0;
			starIndex = gridIndex = 0;

			backPosition = new Vector2(0, 0 + Constants.GameOffsetY);
			starPosition = new Vector2(0, 80 + Constants.GameOffsetY);
			gridPosition = new Vector2(0, 240 + Constants.GameOffsetY);
			titlePosition = new Vector2((Constants.ScreenWide - 240) / 2.0f, (Constants.ScreenHigh - 160) / 2.0f + 94);

			const UInt16 gameHigh = Constants.ScreenHigh - (2 * Constants.GameOffsetY);
			const UInt16 bottHigh = gameHigh + Constants.GameOffsetY;
			bottomPosition = new Vector2(0, bottHigh + Constants.TargetSize);

			//origin = new Vector2(40, 0);		// TODO delete
			rotation = MathHelper.ToRadians(270);
		}

		public void LoadContent()
		{
			backRectangle = MyGame.Manager.ImageManager.BackRectangle;

			starRectangles = new Rectangle[Constants.MAX_STAR];
			starRectangles[0] = starRectangles[1] = MyGame.Manager.ImageManager.StarRectangles[0];
			if (0 != starDelay)
			{
				starRectangles[1] = MyGame.Manager.ImageManager.StarRectangles[1];
			}

			gridRectangles = new Rectangle[Constants.MAX_GRID];
			gridRectangles[0] = gridRectangles[1] = gridRectangles[2] = MyGame.Manager.ImageManager.GridRectangles[0];
			if (0 == gridDelay)
			{
				return;
			}

			gridRectangles[1] = MyGame.Manager.ImageManager.GridRectangles[1];
			gridRectangles[2] = MyGame.Manager.ImageManager.GridRectangles[2];
		}

		public void UpdateStar(GameTime gameTime)
		{
			starTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (starTimer < starDelay)
			{
				return;
			}

			starTimer -= starDelay;
			starIndex = (Byte)(1 - starIndex);
		}

		public void UpdateGrid(GameTime gameTime)
		{
			gridTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (gridTimer < gridDelay)
			{
				return;
			}

			gridTimer -= gridDelay;
			gridIndex++;
			if (gridIndex >= Constants.MAX_GRID)
			{
				gridIndex = 0;
			}
		}

		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, backPosition, backRectangle, Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, gridPosition, gridRectangles[gridIndex], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, starPosition, starRectangles[starIndex], Color.White);
		}

		public void DrawTitle()
		{
			DrawTitle(titlePosition);
		}
		public void DrawTitle(Vector2 position)
		{
			Engine.SpriteBatch.Draw(Assets.GameTitle, position, Color.White);
		}

		public void DrawBottom()
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, bottomPosition, MyGame.Manager.ImageManager.BottomRectangle, Color.White, rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
		}
	}
}
