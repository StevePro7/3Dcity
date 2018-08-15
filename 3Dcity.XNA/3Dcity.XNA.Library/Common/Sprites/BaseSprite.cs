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
		private Byte maxFrames;
		protected Byte frameIndex;
		private UInt16 frameDelay;
		private UInt16 frameTimer;

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
			Initialize(1, 0);
		}

		public virtual void Initialize(Byte theMaxFrames, UInt16 theFrameDelay)
		{
			maxFrames = theMaxFrames;
			frameDelay = theFrameDelay;
			frameTimer = 0;
			frameIndex = 0;
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

		public virtual void Update(GameTime gameTime)
		{
			frameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (frameTimer >= frameDelay)
			{
				frameTimer -= frameDelay;
				frameIndex++;
				if (frameIndex >= maxFrames)
				{
					frameIndex = 0;
				}
			}
		}

		public virtual void Draw()
		{
			Draw(frameIndex);
		}

		protected virtual void Draw(Byte index)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, Position, rectangles[index], Color.White);
		}

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
