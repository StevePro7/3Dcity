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
		//Boolean CheckJoyPadMove(Vector2 position);

		Boolean CheckPosInRect(Vector2 position, Rectangle collision);
		Vector2 ClampPosInRect(Vector2 position, Rectangle bounds);
		Single CalcJoyPadPosn(Single space, Single coord, Single bound);
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

		public Single CheckJoyPadHorz(Vector2 position)
		{
			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joyPadCollision);
			if (!contains)
			{
				return 0.0f;
			}

			// Step 02. clamp position.
			position = ClampPosInRect(position, joyPadBounds);

			// Step 03. calcd value.
			return CalcJoyPadPosn(joyPadBounds.Width, position.X, joyPadBounds.Left);
		}

		public Single CheckJoyPadVert(Vector2 position)
		{
			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joyPadCollision);
			if (!contains)
			{
				return 0.0f;
			}

			// Step 02. clamp position.
			position = ClampPosInRect(position, joyPadBounds);

			// Step 03. calcd value.
			return CalcJoyPadPosn(joyPadBounds.Height, position.Y, joyPadBounds.Top);
		}

		public Boolean CheckPosInRect(Vector2 position, Rectangle collision)
		{
			return position.X >= collision.Left && 
				position.X <= collision.Right && 
				position.Y >= collision.Top && 
				position.Y <= collision.Bottom;
		}

		public Vector2 ClampPosInRect(Vector2 position, Rectangle bounds)
		{
			if (position.X < bounds.Left)
			{
				position.X = bounds.Left;
			}
			if (position.X > bounds.Right)
			{
				position.X = bounds.Right;
			}
			if (position.Y < bounds.Top)
			{
				position.Y = bounds.Top;
			}
			if (position.Y > bounds.Bottom)
			{
				position.Y = bounds.Bottom;
			}

			return position;
		}

		public Single CalcJoyPadPosn(Single space, Single coord, Single bound)
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

	}
}
