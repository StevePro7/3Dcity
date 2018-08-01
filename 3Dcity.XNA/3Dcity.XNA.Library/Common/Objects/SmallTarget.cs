using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Objects
{
	public class SmallTarget : BaseSprite
	{
		//private const Single ratio = 0.05f;
		private const Single ratio = 0.15f;			// TODO make configurable to tweak "speed" in which small target moves

		public override void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;
			if (Math.Abs(horz) < 0.001f && Math.Abs(vert) < 0.001f)
			{
				position.X = BaseX;
				position.Y = BaseY;
			}
			//if (Math.Abs(vert) < 0.001f)
			//{
			//    position.Y = BaseY;
			//}

			Single val1 = horz * 100.0f;
			Single val2 = val1 / 2.0f;
			//Single val3 = val2 + BaseX;
			Single val3 = val2 * ratio;

			Single val5 = vert * 100.0f;
			Single val6 = val5 / 2.0f;
			//Single val7 = val6 + BaseY;
			Single val7 = val6 * ratio;

			//position.X = val3;
			//position.Y = val7;

			position.X += val3;
			position.Y += val7;

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

		//private Texture2D[] textureList;
		//public virtual void Initialize(UInt16 baseX, UInt16 baseY, UInt16 size, UInt16 boundX, UInt16 boundY, UInt16 rect)
		//{
		//    Initialize(baseX, baseY, size, size, boundX, boundY, rect, rect);
		//}

		//public virtual void Initialize(UInt16 baseX, UInt16 baseY, UInt16 sizeW, UInt16 sizeH, UInt16 boundX, UInt16 boundY, UInt16 boundW, UInt16 boundH)
		//{
		//    BaseX = baseX;
		//    BaseY = baseY;
		//    Position = new Vector2(baseX, baseY);

		//    Single midX = sizeW / 2.0f;
		//    Single midY = sizeH / 2.0f;
		//    Midpoint = new Vector2(baseX + midX, baseY + midY);

		//    Collision = new Rectangle(collX, collY, collW, collH);
		//}

		//public virtual void LoadContent(Texture2D theTexture)
		//{
		//    texture = theTexture;
		//}

		//public UInt16 BaseX { get; private set; }
		//public UInt16 BaseY { get; private set; }
		//public Vector2 Position { get; private set; }
		//public Vector2 Midpoint { get; private set; }
		//public Rectangle Bounds { get; private set; }
	}
}
