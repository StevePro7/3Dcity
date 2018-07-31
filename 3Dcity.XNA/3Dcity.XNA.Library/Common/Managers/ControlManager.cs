using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IControlManager 
	{
		void Initialize();
		void LoadContent();

		Single CheckJoyPadHorz(Vector2 position);
		Single CheckJoyPadVert(Vector2 position);
		Boolean CheckJoyPadMove(Vector2 position);

		Single MyConvert(Vector2 position);
		Single MyConvert(Vector2 position, Rectangle collision);
		Single MyConvert(Int32 posX, Int32 posY, Int32 rectX, Int32 rectY, Int32 rectW, Int32 rectH);
		Boolean Test(int x, int y);
	}

	public class ControlManager : IControlManager 
	{
		private Rectangle joyPadCollision;
		private Rectangle joyPadBounds;

		public void Initialize()
		{
		}

		public void LoadContent()
		{
			joyPadCollision = MyGame.Manager.SpriteManager.JoypadMove.Collision;
			joyPadBounds = MyGame.Manager.SpriteManager.JoypadMove.Bounds;
		}

		public Single MyConvert(Vector2 position)
		{
			return MyConvert(position, Rectangle.Empty);
		}

		public Single CheckJoyPadHorz(Vector2 position)
		{
			Boolean contains = position.X >= joyPadCollision.Left &&
								position.X <= joyPadCollision.Right &
								position.Y >= joyPadCollision.Top &&
								position.Y <= joyPadCollision.Bottom;

			if (!contains)
			{
				return 0.0f;
			}

			if (position.X < joyPadBounds.Left)
			{
				position.X = joyPadBounds.Left; 
			}
			if (position.X > joyPadBounds.Right)
			{
				position.X = joyPadBounds.Right; 
			}
			if (position.Y < joyPadBounds.Top)
			{ 
				position.Y = joyPadBounds.Top; 
			}
			if (position.Y > joyPadBounds.Bottom)
			{ 
				position.Y = joyPadBounds.Bottom; 
			}

			return 0.0f;
		}
		public Single CheckJoyPadVert(Vector2 position)
		{
			return 0.0f;
		}
		public Boolean CheckJoyPadMove(Vector2 position)
		{
			return false;
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
