using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		private Texture2D[] textures;
		private Rectangle[] rectangles;
		private Vector2[] positions;
		private int ex, ey;
		private Byte index;
		private Single delay;
		private Vector2 pos1, pos2, pos3, pos4;

		public override void Initialize()
		{
			base.Initialize();

			rectangles = MyGame.Manager.ImageManager.EnemyRectangles;
			ex = 100;
			ey = 100;
			positions = GetPositions(ex, ey);
			//textures = GetTextures();
			index = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyIndex;
			delay = 800.0f;
			//flag = false;
			//start = 32.0f;
			//stopX = 128.0f;
		}

		public override void LoadContent()
		{
			pos1 = new Vector2(0, 0);
			pos2 = new Vector2(0, 80);
			pos3 = new Vector2(0, 240);
			pos4 = new Vector2(0, 480);
			
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			UpdateTimer(gameTime);
			if (Timer > delay)
			{
				Timer = 0;
				index++;
				if (index >= Constants.MAX_ENEMY)
				{
					index = 0;
				}
			}

			return (Int32)ScreenType.Title;
		}

		public override void Draw()
		{
			base.Draw();

			Rectangle rectangle = rectangles[index];
			var position = positions[index];
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, position, rectangle, Color.White);

			//Engine.SpriteBatch.Draw(textures[index], positions[index], Color.White);
			//Engine.SpriteBatch.Draw(texture, position, Color.White);
		}

		//private Texture2D[] GetTextures()
		//{
		//    textures = new Texture2D[max];
		//    textures[0] = Assets.Enemy25Texture;
		//    textures[1] = Assets.Enemy32Texture;
		//    textures[2] = Assets.Enemy40Texture;
		//    textures[3] = Assets.Enemy50Texture;
		//    textures[4] = Assets.Enemy64Texture;
		//    textures[5] = Assets.Enemy80Texture;
		//    textures[6] = Assets.Enemy96Texture;
		//    textures[7] = Assets.Enemy120Texture;
		//    return textures;
		//}
		private Vector2[] GetPositions(int sx, int sy)
		{
			positions = positions = new Vector2[Constants.MAX_ENEMY];
			positions[0] = GetPosition(25, sx, sy);
			positions[1] = GetPosition(32, sx, sy);
			positions[2] = GetPosition(40, sx, sy);
			positions[3] = GetPosition(50, sx, sy);
			positions[4] = GetPosition(64, sx, sy);
			positions[5] = GetPosition(80, sx, sy);
			positions[6] = GetPosition(96, sx, sy);
			positions[7] = GetPosition(120, sx, sy);
			return positions;
		}
		private static Vector2 GetPosition(Byte off, int tx, int ty)
		{
			return new Vector2((120 - off) / 2.0f + tx, (120 - off) / 2.0f + ty);
		}

	}
}
