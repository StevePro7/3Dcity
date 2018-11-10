using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Objects
{
	public class BigTarget : BaseSprite
	{
		private const Single PIXEL = 200.0f;

		public override void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;

			Single delta = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			Single moveX = horz * delta * PIXEL;
			Single moveY = vert * delta * PIXEL;

			position.X += moveX;
			position.Y += moveY;

			if (position.X <= Bounds.Left)
			{
				position.X = Bounds.Left;
			}
			if (position.X >= Bounds.Right)
			{
				position.X = Bounds.Right;
			}

			if (position.Y <= Bounds.Top)
			{
				position.Y = Bounds.Top;
			}
			if (position.Y >= Bounds.Bottom)
			{
				position.Y = Bounds.Bottom;
			}

			Position = position;
		}
	}
}
