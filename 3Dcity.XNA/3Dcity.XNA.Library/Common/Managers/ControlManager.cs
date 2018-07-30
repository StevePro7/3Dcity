using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IControlManager 
	{
		void Initialize();
		Single MyConvert(Vector2 position);
		Single MyConvert(Vector2 position, Rectangle collision);
		Single MyConvert(Int32 posX, Int32 posY, Int32 rectX, Int32 rectY, Int32 rectW, Int32 rectH);
		Boolean Test(int x, int y);
	}

	public class ControlManager : IControlManager 
	{
		public void Initialize()
		{
		}

		public Single MyConvert(Vector2 position)
		{
			return MyConvert(position, Rectangle.Empty);
		}

		public Single MyConvert(Vector2 position, Rectangle collision)
		{
			Single value = 0.0f;
			//if (!collision.Contains((Int32)position.X, (Int32)position.Y))
			//{
			//    return value;
			//}

			//if (position.X >= collision.Left && position.X <= collision.Right)
			if (position.X < collision.Left || position.X <= collision.Right)
			{
				
			}
			Single width = collision.Width / 2.0f;
			Single dataX = position.X - width;

			value = dataX /= width;
			return value;
		}

		public Single MyConvert(Int32 posX, Int32 posY, Int32 rectX, Int32 rectY, Int32 rectW, Int32 rectH)
		{
			Vector2 position = Vector2.Zero;
			position.X = posX;
			position.Y = posY;

			Rectangle collision = Rectangle.Empty;
			collision.X = rectX;
			collision.Y = rectY;
			collision.Width = rectW;
			collision.Height = rectH;

			return MyConvert(position, collision);
		}

		public Boolean Test(int x, int y)
		{
			return x == 10;
		}

	}
}
