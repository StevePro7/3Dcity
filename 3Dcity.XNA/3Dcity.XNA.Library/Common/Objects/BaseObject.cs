using System;
using WindowsGame.Define;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Objects
{
	public class BaseObject
	{
		private Texture2D texture;

		public virtual void Initialize(Vector2 position, Rectangle collision)
		{
			BaseX = (UInt16)(position.X);
			BaseY = (UInt16)(position.Y);
			Position = position;
			Collision = collision;
		}

		//public virtual void Initialize(UInt16 baseX, UInt16 baseY, UInt16 size, UInt16 collX, UInt16 collY, UInt16 rect)
		//{
		//    Initialize(baseX, baseY, size, size, collX, collY, rect, rect);
		//}

		//public virtual void Initialize(UInt16 baseX, UInt16 baseY, UInt16 sizeW, UInt16 sizeH, UInt16 collX, UInt16 collY, UInt16 collW, UInt16 collH)
		//{
		//    BaseX = baseX;
		//    BaseY = baseY;
		//    SizeW = sizeW;
		//    SizeH = sizeH;
		//    Position = new Vector2(baseX, baseY);

		//    Single midX = sizeW / 2.0f;
		//    Single midY = sizeH / 2.0f;
		//    Midpoint = new Vector2(baseX + midX, baseY + midY);

		//    Collision = new Rectangle(collX, collY, collW, collH);
		//}

		public virtual void LoadContent(Texture2D theTexture)
		{
			UInt16 width = (UInt16)(theTexture.Width);
			UInt16 height = (UInt16)(theTexture.Height);

			LoadContent(theTexture, width, height);
		}

		protected virtual void LoadContent(Texture2D theTexture, UInt16 width, UInt16 height)
		{
			texture = theTexture;

			SizeW = (UInt16)(texture.Width);
			SizeH = (UInt16)(texture.Height);

			Single midX = width / 2.0f + BaseX;
			Single midY = height / 2.0f + BaseY;
			Midpoint = new Vector2(midX, midY);
		}

		public virtual void Draw()
		{
			Engine.SpriteBatch.Draw(texture, Position, Color.White);
		}

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		public UInt16 SizeW { get; private set; }
		public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Midpoint { get; private set; }
		public Rectangle Collision { get; private set; }
	}
}
