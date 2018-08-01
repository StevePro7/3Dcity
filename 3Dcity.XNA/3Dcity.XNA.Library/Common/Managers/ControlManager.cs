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

		Boolean CheckPosInRect(Vector2 position, Rectangle collision);
		Boolean CheckPosInRect(Int32 posX, Int32 posY, Int32 collX, Int32 collY, Int32 collW, Int32 collH);

		//void ClampPosInRect(Int32 posX, Int32 posY, Int32 boundX, Int32 boundY, Int32 boundW, Int32 boundH, out Int32 newX, out Int32 newY);
		//Vector2 ClampPosInRect(Vector2 position, Rectangle bounds);

		Single MyConvert(Vector2 position);
		Single MyConvert(Vector2 position, Rectangle collision);
		Single MyConvert(Int32 posX, Int32 posY, Int32 collX, Int32 collY, Int32 collW, Int32 collH);
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

		public Boolean CheckPosInRect(Vector2 position, Rectangle collision)
		{
			return position.X >= collision.Left && 
				position.X <= collision.Right && 
				position.Y >= collision.Top && 
				position.Y <= collision.Bottom;
		}

		public Boolean CheckPosInRect(Int32 posX, Int32 posY, Int32 collX, Int32 collY, Int32 collW, Int32 collH)
		{
			Vector2 position = Vector2.Zero;
			position.X = posX;
			position.Y = posY;

			Rectangle collision = Rectangle.Empty;
			collision.X = collX;
			collision.Y = collY;
			collision.Width = collW;
			collision.Height = collH;

			return CheckPosInRect(position, collision);
		}

		//public void ClampPosInRect(Int32 posX, Int32 posY, Int32 boundX, Int32 boundY, Int32 boundW, Int32 boundH, out Int32 newX, out Int32 newY)
		//{
		//    Vector2 position = Vector2.Zero;
		//    position.X = posX;
		//    position.Y = posY;

		//    Rectangle bounds = Rectangle.Empty;
		//    bounds.X = boundX;
		//    bounds.Y = boundY;
		//    bounds.Width = boundW;
		//    bounds.Height = boundH;

		//    ClampPosInRect(position, bounds, newX, newY);
		//}

		//public Vector2 ClampPosInRect(Vector2 position, Rectangle bounds)
		//{
		//    Int32
		//}

		public Single MyConvert(Vector2 position)
		{
			return MyConvert(position, Rectangle.Empty);
		}

		private Boolean CheckJoyPadColl(Vector2 position)
		{
			return false;
		}

		public Single CheckJoyPadHorz(Vector2 position)
		{
			Single value = 0.0f;

			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joyPadCollision);
			//Boolean contains = position.X >= joyPadCollision.Left &&
			//                    position.X <= joyPadCollision.Right &
			//                    position.Y >= joyPadCollision.Top &&
			//                    position.Y <= joyPadCollision.Bottom;

			if (!contains)
			{
				return value;
			}

			// Step 02. clamp position.
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

			// Step 03. calcd value.
			Single space = joyPadBounds.Width;
			Single coord = position.X;
			Single bound = joyPadBounds.Left;

			value = CalcdJoyPadPosn(space, coord, bound);
			//Single halve = space / 2.0f;
			//Single calcd = coord - bound - halve;

			//if (Math.Abs(halve) > Single.Epsilon)
			//{
			//    value = calcd / halve;
			//}

			return value;
		}

		public Single CheckJoyPadVert(Vector2 position)
		{
			Single value = 0.0f;

			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joyPadCollision);
			//Boolean contains = position.X >= joyPadCollision.Left &&
			//                    position.X <= joyPadCollision.Right &
			//                    position.Y >= joyPadCollision.Top &&
			//                    position.Y <= joyPadCollision.Bottom;

			if (!contains)
			{
				return value;
			}

			// Step 02. clamp position.
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

			// Step 03. calcd value.
			Single space = joyPadBounds.Height;
			Single coord = position.Y;
			Single bound = joyPadBounds.Top;

			return CalcdJoyPadPosn(space, coord, bound);
		}

		private Single CalcdJoyPadPosn(Single space, Single coord, Single bound)
		{
			Single value = 0.0f;

			Single halve = space / 2.0f;
			Single calcd = coord - bound - halve;

			if (Math.Abs(halve) > Single.Epsilon)
			{
				value = calcd / halve;
			}

			return value;
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

		public Single MyConvert(Int32 posX, Int32 posY, Int32 collX, Int32 collY, Int32 collW, Int32 collH)
		{
			Vector2 position = Vector2.Zero;
			position.X = posX;
			position.Y = posY;

			Rectangle collision = Rectangle.Empty;
			collision.X = collX;
			collision.Y = collY;
			collision.Width = collW;
			collision.Height = collH;

			return MyConvert(position, collision);
		}

		public Boolean Test(int x, int y)
		{
			return x == 10;
		}

	}
}
