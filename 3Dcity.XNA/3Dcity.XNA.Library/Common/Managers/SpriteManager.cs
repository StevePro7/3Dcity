using System;
using WindowsGame.Common.Objects;
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
		JoypadMove JoypadMove { get; }
	}

	public class SpriteManager : ISpriteManager
	{
		//private UInt16 jp1x, jp1y, jp2x, jp2y, jpyPsize;
		//private UInt16 cl1x, cl1y, collSize;
		//private UInt16 collSize;

		public void Initialize()
		{
			TheInit();
		}

		public void LoadContent()
		{
			Collision1200.LoadContent(Assets.SteveProTexture200);
			//Collision2200.LoadContent(Assets.SteveProTexture200);
			JoypadMove.LoadContent(Assets.JoypadTexture);
			JoypadMove2.LoadContent(Assets.JoypadTexture);
			SmallTarget.LoadContent(Assets.Target40Texture);
			SmallTarget2.LoadContent(Assets.Target40Texture);
			BigTarget.LoadContent(Assets.Target64Texture);
		}

		public void Update(GameTime gameTime, Single horz, Single vert)
		{
			BigTarget.Update(gameTime, horz, vert);

			SmallTarget.Update(gameTime, horz, vert);
			SmallTarget2.Update(gameTime, horz, vert);
		}

		public void Draw()
		{
			//Collision1200.Draw();
			//Collision2200.Draw();
			JoypadMove.Draw();
			//JoypadMove2.Draw();
			SmallTarget.Draw();
			//SmallTarget2.Draw();
			BigTarget.Draw();
		}

		private void TheInit()
		{
			//collSize = 200;
			//jpyPsize = 160;
			//jp1x = 20; jp1y = 20; jp2x = 20;
			//jp2y = (UInt16) (Constants.ScreenHigh - collSize + 20);

			//UInt16 baseX = 20;///MyGame.Manager.ConfigManager.GlobalConfigData.JoypadX;
			//UInt16 baseY = 20;//MyGame.Manager.ConfigManager.GlobalConfigData.JoypadY;

			// TODO JoyPadMove collision confirm!
			Vector2 jpPos = new Vector2(20, 300);
			//Rectangle jpColl = new Rectangle(0, 280, 200, 200);
			//Rectangle jpColl = new Rectangle(-200, 80, 600, 600);
			Rectangle jpColl = new Rectangle(-100, 180, 400, 400);
			Rectangle jpBndl = new Rectangle(0, 280, 200, 200);
			JoypadMove = new JoypadMove();
			JoypadMove.Initialize(jpPos, jpColl, jpBndl);
			//JoypadMove.Initialize(jp1x, jp1y, jpyPsize, 0, 0, collSize);

			Vector2 jpPos2 = new Vector2(20, 20);
			Rectangle jpColl2 = new Rectangle(0, 0, 200, 200);
			JoypadMove2 = new JoypadMove();
			JoypadMove2.Initialize(jpPos2, jpColl2);

			//UInt16 high = (UInt16) (Constants.ScreenHigh - collSize);
			//UInt16 wide = (UInt16) (Constants.ScreenWide - collSize);

			Collision1200 = new Collision200();
			Collision1200.Initialize(new Vector2(0, 280), jpColl);

			//Collision2200 = new Collision200();
			//Collision2200.Initialize(Vector2.Zero, jpColl2);

			Vector2 stPos = new Vector2(80, 360);
			Rectangle stBounds = new Rectangle(30, 310, 100, 100);
			SmallTarget = new SmallTarget();
			SmallTarget.Initialize(stPos, stBounds);
			//SmallTarget.Initialize(stPos, Rectangle.Empty, stBounds);

			Vector2 stPos2 = new Vector2(80, 80);
			Rectangle stBounds2 = new Rectangle(30, 30, 100, 100);
			SmallTarget2 = new SmallTarget();
			SmallTarget2.Initialize(stPos2, stBounds2);
			//SmallTarget2.Initialize(stPos2, Rectangle.Empty, stBounds2);

			const Byte targetTop = 80;
			const Byte targetSize = 64;
			Vector2 bgPos = new Vector2(400, 240);
			Rectangle bgBounds = new Rectangle(0, targetTop, 800 - targetSize, 480 - targetTop - targetSize);
			BigTarget = new BigTarget();
			BigTarget.Initialize(bgPos, Rectangle.Empty, bgBounds);
		}

		public Collision200 Collision1200 { get; private set; }
		//public Collision200 Collision2200 { get; private set; }
		public JoypadMove JoypadMove { get; private set; }
		public JoypadMove JoypadMove2 { get; private set; }
		public SmallTarget SmallTarget { get; private set; }
		public SmallTarget SmallTarget2 { get; private set; }
		public BigTarget BigTarget { get; private set; }

	}
}
