using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class LargeTarget : BaseSprite
	{
		private const Single PIXEL = 200.0f;		// TODO tweak this constant
		private const Single RATIO = 1.05f;			// TODO tweak this acceleration
		private const Single MAXIM = 2.5f;			// TODO tweak maximum acceleration

		private Single accX;
		private Single accY;

		public LargeTarget() : base()
		{
			accX = 1.0f;
			accY = 1.0f;
		}

		public void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;

			// Tolerance
			if (Math.Abs(horz) < Constants.GeneralTolerance)
			{
				horz = 0.0f;
			}
			if (Math.Abs(vert) < Constants.GeneralTolerance)
			{
				vert= 0.0f;
			}


			if (Math.Abs(horz) < Single.Epsilon && Math.Abs(vert) < Single.Epsilon)
			{
				accX = 1.0f;
				accY = 1.0f;
			}
			else if (!(Math.Abs(horz) < Single.Epsilon) && Math.Abs(vert) < Single.Epsilon)
			{
				accX *= RATIO;
				accY = 1.0f;
			}
			else if (Math.Abs(horz) < Single.Epsilon && (!(Math.Abs(vert) < Single.Epsilon)))
			{
				accX = 1.0f;
				accY *= RATIO;
			}
			else
			{
				accX *= RATIO;
				accY *= RATIO;
			}

			if (accX > MAXIM)
			{
				accX = MAXIM;
			}
			if (accY > MAXIM)
			{
				accY = MAXIM;
			}

			Single delta = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			Single moveX = horz * delta * PIXEL * accX;
			Single moveY = vert * delta * PIXEL * accY;

			position.X += moveX;
			position.Y += moveY;

			if (position.X <= Bounds.Left)
			{
				position.X = Bounds.Left;
				accX = 1.0f;
			}
			if (position.X >= Bounds.Right)
			{
				position.X = Bounds.Right;
				accX = 1.0f;
			}

			if (position.Y <= Bounds.Top)
			{
				position.Y = Bounds.Top;
				accY = 1.0f;
			}
			if (position.Y >= Bounds.Bottom)
			{
				position.Y = Bounds.Bottom;
				accY = 1.0f;
			}

			Position = position;
		}
	}
}
