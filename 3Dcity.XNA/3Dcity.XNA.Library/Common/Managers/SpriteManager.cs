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
		void Reset();

		void SetMovement(Single horz, Single vert);
		void SetPosition(SpriteType type, Vector2 position);
		void Update(GameTime gameTime);

		void Draw();
		void DrawCursor();

		// Properties.
		SmallTarget SmallTarget { get; }
		LargeTarget LargeTarget { get; }
	}

	public class SpriteManager : ISpriteManager
	{
		private Vector2 smallPosition;
		private Vector2 largePosition;
		private Single targetHorz, targetVert;

		public void Initialize()
		{
			smallPosition = new Vector2(80, 360 + Constants.GameOffsetY);
			largePosition = new Vector2((Constants.ScreenWide - 64) / 2.0f, 250 + Constants.GameOffsetY);
			TheInit();
		}

		public void LoadContent()
		{
			LargeTarget.LoadContent(MyGame.Manager.ImageManager.TargetLargeRectangle);
			SmallTarget.LoadContent(MyGame.Manager.ImageManager.TargetSmallRectangle);
		}

		public void Reset()
		{
			SmallTarget.SetPosition(smallPosition);
			LargeTarget.SetPosition(largePosition);
		}

		public void SetMovement(Single horz, Single vert)
		{
			targetHorz = horz;
			targetVert = vert;
		}

		public void SetPosition(SpriteType type, Vector2 position)
		{
			switch (type)
			{
				case SpriteType.LargeTarget:
					LargeTarget.SetPosition(position);
					break;

				case SpriteType.SmallTarget:
					SmallTarget.SetPosition(position);
					break;
			}
		}

		public void Update(GameTime gameTime)
		{
			LargeTarget.Update(gameTime, targetHorz, targetVert);
			SmallTarget.Update(gameTime, targetHorz, targetVert);
		}

		public void Draw()
		{
			SmallTarget.Draw();
			LargeTarget.Draw();
		}

		public void DrawCursor()
		{
			SmallTarget.Draw();
		}

		private void TheInit()
		{
			//Vector2 stPos = new Vector2(80, 360 + Constants.GameOffsetY);
			Rectangle stBounds = new Rectangle(30, 310 + Constants.GameOffsetY, 100, 100);
			SmallTarget = new SmallTarget();
			SmallTarget.Initialize(smallPosition, stBounds);

			const Byte targetTop = 74;
			const Byte targetSize = 64;
			//Vector2 bgPos = new Vector2((Constants.ScreenWide - 64) / 2.0f, 250 + Constants.GameOffsetY);
			Rectangle bgBounds = new Rectangle(-2, targetTop + Constants.GameOffsetY, Constants.ScreenWide - targetSize + 2, Constants.ScreenHigh - (2 * Constants.GameOffsetY) - targetTop - targetSize + 2);
			LargeTarget = new LargeTarget();
			LargeTarget.Initialize(largePosition, Rectangle.Empty, bgBounds);
		}

		public SmallTarget SmallTarget { get; private set; }
		public LargeTarget LargeTarget { get; private set; }

	}
}
