using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Sprites
{
	public class BaseSprite
	{
		protected Rectangle[] rectangles;
		//private Byte maxFrames;
		//protected Byte frameIndex;
		//private UInt16 frameDelay;
		//private UInt16 frameTimer;

		public virtual void Initialize(Byte maxFrames)
		{
			MaxFrames = maxFrames;
			FrameIndex = 0;
			//frameDelay = theFrameDelay;
			//frameTimer = 0;
		}

		public virtual void Initialize(Vector2 position)
		{
			Initialize(position, Rectangle.Empty);
		}

		public virtual void Initialize(Vector2 position, Rectangle bounds)
		{
			Initialize(position, Rectangle.Empty, bounds);
		}

		public virtual void Initialize(Vector2 position, Rectangle collision, Rectangle bounds)
		{
			BaseX = (UInt16)(position.X);
			BaseY = (UInt16)(position.Y);
			Position = position;
			//Collision = collision;
			Bounds = bounds;

			// Default one frame;
			Initialize(1);
		}

		public virtual void LoadContent(Rectangle theRectangle)
		{
			Rectangle[] theRectangles = new Rectangle[1] { theRectangle };
			LoadContent(theRectangles);
		}

		public virtual void LoadContent(Rectangle[] theRectangles)
		{
			rectangles = theRectangles;

			// Assume all textures in array are same size!
			UInt16 width = (UInt16)(theRectangles[0].Width);
			UInt16 height = (UInt16)(theRectangles[0].Height);
			SizeW = width;
			SizeH = height;

			Single midX = width / 2.0f + BaseX;
			Single midY = height / 2.0f + BaseY;
			Midpoint = new Vector2(midX, midY);
		}

		public void SetPosition(Vector2 position)
		{
			Position = position;
		}

		public virtual void Update(GameTime gameTime)
		{
			//frameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			//if (frameTimer >= frameDelay)
			//{
			//    frameTimer -= frameDelay;
			//    frameIndex++;
			//    if (frameIndex >= maxFrames)
			//    {
			//        frameIndex = 0;
			//    }
			//}
		}

		public virtual void Draw()
		{
			Draw(FrameIndex);
		}

		protected virtual void Draw(Byte theFrameIndex)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, Position, rectangles[theFrameIndex], Color.White);
		}

		public Byte MaxFrames { get; protected set; }
		public Byte FrameIndex { get; protected set; }

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		public UInt16 SizeW { get; private set; }
		public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; protected set; }
		public Vector2 Midpoint { get; private set; }
		//public Rectangle Collision { get; private set; }
		public Rectangle Bounds { get; private set; }
	}
}
