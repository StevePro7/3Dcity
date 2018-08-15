using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ISpriteManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime, Single horz, Single vert);
		void Draw();

		// Properties.
		SmallTarget SmallTarget { get; }
		BigTarget BigTarget { get; }
	}

	public class SpriteManager : ISpriteManager
	{
		public void Initialize()
		{
			TheInit();
		}

		public void LoadContent()
		{
			BigTarget.LoadContent(MyGame.Manager.ImageManager.TargetBigRectangle);
			SmallTarget.LoadContent(MyGame.Manager.ImageManager.TargetSmallRectangle);
		}

		public void Update(GameTime gameTime, Single horz, Single vert)
		{
			BigTarget.Update(gameTime, horz, vert);
			SmallTarget.Update(gameTime, horz, vert);
		}

		public void Draw()
		{
			SmallTarget.Draw();
			BigTarget.Draw();
		}

		private void TheInit()
		{
			Vector2 stPos = new Vector2(80, 360);
			Rectangle stBounds = new Rectangle(30, 310, 100, 100);
			SmallTarget = new SmallTarget();
			SmallTarget.Initialize(stPos, stBounds);

			const Byte targetTop = 74;
			const Byte targetSize = 64;
			Vector2 bgPos = new Vector2((Constants.ScreenWide - 64) / 2.0f, 250);
			Rectangle bgBounds = new Rectangle(-2, targetTop, Constants.ScreenWide - targetSize + 2, Constants.ScreenHigh - targetTop - targetSize + 2);
			BigTarget = new BigTarget();
			BigTarget.Initialize(bgPos, Rectangle.Empty, bgBounds);
		}

		public SmallTarget SmallTarget { get; private set; }
		public BigTarget BigTarget { get; private set; }

	}
}
