using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Screens
{
	public class BeatScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			//MyGame.Manager.RenderManager.UpdateStar(gameTime);
			//MyGame.Manager.RenderManager.UpdateGrid(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();

			const UInt16 gameHigh = Constants.ScreenHigh - (2 * Constants.GameOffsetY);
			const Byte offset = 16;
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, new Vector2((Constants.ScreenWide - 224) / 2.0f, gameHigh + Constants.GameOffsetY - offset), MyGame.Manager.ImageManager.TitleRectangle, Color.White);

//			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, Vector2.Zero, MyGame.Manager.ImageManager.BottomRectangle, Color.White);
			//const UInt16 bottHigh = gameHigh + Constants.GameOffsetY;
			//Vector2 bottPos = new Vector2(0, bottHigh + Constants.TargetSize);
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, bottPos, MyGame.Manager.ImageManager.BottomRectangle, Color.White, MathHelper.ToRadians(270), Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, Vector2.Zero, MyGame.Manager.ImageManager.TitleRectangle, Color.White);

			MyGame.Manager.RenderManager.DrawBottom();
		}

	}
}
