using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Define;

namespace WindowsGame.Common.Objects
{
	public class BaseObject
	{
		private Texture2D[] textures;

		public virtual void Initialize(Vector2 position, Rectangle collision)
		{
			Initialize(position, collision, Rectangle.Empty);
		}

		public virtual void Initialize(Vector2 position, Rectangle collision, Rectangle bounds)
		{
			BaseX = (UInt16)(position.X);
			BaseY = (UInt16)(position.Y);
			Position = position;
			Collision = collision;
			Bounds = bounds;
			Index = 0;
		}

		public virtual void LoadContent(Texture2D theTexture)
		{
			Texture2D[] theTextures = new Texture2D[1] { theTexture };
			LoadContent(theTextures);
		}

		public virtual void LoadContent(Texture2D[] theTextures)
		{
			textures = theTextures;

			// Assume all textures in array are same size!
			UInt16 width = (UInt16)(theTextures[0].Width);
			UInt16 height = (UInt16)(theTextures[0].Height);
			SizeW = width;
			SizeH = height;

			Single midX = width / 2.0f + BaseX;
			Single midY = height / 2.0f + BaseY;
			Midpoint = new Vector2(midX, midY);
		}

		public void ToggleIndex()
		{
			Index = (Byte)(1 - Index);
		}

		public virtual void Draw()
		{
			Engine.SpriteBatch.Draw(textures[0], Position, Color.White);
		}
		public virtual void Draw(Byte index)
		{
			Engine.SpriteBatch.Draw(textures[index], Position, Color.White);
		}

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		public UInt16 SizeW { get; private set; }
		public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Midpoint { get; private set; }
		public Rectangle Collision { get; private set; }
		public Rectangle Bounds { get; private set; }
		protected Byte Index { get; private set; }
	}
}
