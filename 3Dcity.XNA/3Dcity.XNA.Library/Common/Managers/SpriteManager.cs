using System;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

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
		//JoypadMove JoypadMove { get; }
	}

	public class SpriteManager : ISpriteManager
	{
		public void Initialize()
		{
			TheInit();
		}

		public void LoadContent()
		{
			//JoypadMove.LoadContent(Assets.JoypadTexture);

			SmallTarget.LoadContent(Assets.Target40Texture);
			BigTarget.LoadContent(Assets.Target64Texture);
		}

		public void Update(GameTime gameTime, Single horz, Single vert)
		{
			BigTarget.Update(gameTime, horz, vert);
			SmallTarget.Update(gameTime, horz, vert);
		}

		public void Draw()
		{
			//JoypadMove.Draw();

			SmallTarget.Draw();
			BigTarget.Draw();
		}

		private void TheInit()
		{
			//Vector2 jpPos = new Vector2(20, 300);
			////Rectangle jpColl = new Rectangle(0, 280, 200, 200);
			////Rectangle jpColl = new Rectangle(-200, 80, 600, 600);
			//Rectangle jpColl = new Rectangle(-100, 180, 400, 400);
			//Rectangle jpBndl = new Rectangle(0, 280, 200, 200);
			//JoypadMove = new JoypadMove();
			//JoypadMove.Initialize(jpPos, jpColl, jpBndl);

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

		//public JoypadMove JoypadMove { get; private set; }

		public SmallTarget SmallTarget { get; private set; }
		public BigTarget BigTarget { get; private set; }

	}
}
