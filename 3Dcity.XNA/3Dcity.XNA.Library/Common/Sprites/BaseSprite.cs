using System;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Sprites
{
	public class BaseSprite
	{
		private Texture2D texture;
		private Rectangle[] frameRects;
		private Byte maxFrames;
		private Byte frameIndex;
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
			Collision = collision;
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

		public virtual void LoadContent(Texture2D theTexture)
		{
			UInt16 width = (UInt16)(theTexture.Width);
			UInt16 height = (UInt16)(theTexture.Height);

			LoadContent(theTexture, width, height);
		}

		protected virtual void LoadContent(Texture2D theTexture, UInt16 width, UInt16 height)
		{
			texture = theTexture;

			UInt16 sizeW = (UInt16)(texture.Width / maxFrames);
			UInt16 sizeH = (UInt16)(texture.Height);

			frameRects = new Rectangle[maxFrames];
			for (Byte index = 0; index < maxFrames; index++)
			{
				frameRects[index] = new Rectangle(sizeW * index, 0, sizeW, sizeH);
			}

			//Single midX = width / 2.0f + BaseX;
			//Single midY = height / 2.0f + BaseY;
			//Midpoint = new Vector2(midX, midY);
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
			//Engine.SpriteBatch.Draw(texture, Position, Color.White);
			Engine.SpriteBatch.Draw(texture, Position, frameRects[frameIndex], Color.White);
		}

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		//public UInt16 SizeW { get; private set; }
		//public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; protected set; }
		//public Vector2 Midpoint { get; private set; }
		public Rectangle Collision { get; private set; }
		public Rectangle Bounds { get; private set; }
	}
}
