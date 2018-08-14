using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
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
	}

	public class RenderManager : IRenderManager
	{
		private Texture2D[] starImages, gridImages;
		private Vector2 starPosition, gridPosition;
		private UInt16 starTimer, starDelay;
		private UInt16 gridTimer, gridDelay;
		private Byte starIndex, gridIndex;
		private Byte MAX_GRID = 3;

		public void Initialize()
		{
			starDelay = MyGame.Manager.ConfigManager.GlobalConfigData.StarDelay;
			gridDelay = MyGame.Manager.ConfigManager.GlobalConfigData.GridDelay;
			starTimer = gridTimer = 0;
			starIndex = gridIndex = 0;

			starPosition = new Vector2(0, 80);
			gridPosition = new Vector2(0, 240);
		}

		public void LoadContent()
		{
			starImages = new Texture2D[2];
			starImages[0] = starImages[1] = Assets.Stars01Texture;
			if (0 != starDelay)
			{
				starImages[1] = Assets.Stars02Texture;
			}

			gridImages = new Texture2D[MAX_GRID];
			gridImages[0] = gridImages[1] = gridImages[2] = Assets.Foreground01Texture;
			if (0 != gridDelay)
			{
				gridImages[1] = Assets.Foreground02Texture;
				gridImages[2] = Assets.Foreground03Texture;
			}
		}

		public void UpdateStar(GameTime gameTime)
		{
			starTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (starTimer >= starDelay)
			{
				starTimer -= starDelay;
				starIndex = (Byte)(1 - starIndex);
			}
		}

		public void UpdateGrid(GameTime gameTime)
		{
			gridTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (gridTimer >= gridDelay)
			{
				gridTimer -= gridDelay;
				gridIndex++;
				if (gridIndex >= MAX_GRID)
				{
					gridIndex = 0;
				}
			}
		}

		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.BackgroundTexture, Vector2.Zero, Color.White);
			Engine.SpriteBatch.Draw(starImages[starIndex], starPosition, Color.White);
			Engine.SpriteBatch.Draw(gridImages[gridIndex], gridPosition, Color.White);
		}

	}
}
