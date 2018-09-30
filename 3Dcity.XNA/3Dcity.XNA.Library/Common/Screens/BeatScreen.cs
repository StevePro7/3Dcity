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

			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, Vector2.Zero, MyGame.Manager.ImageManager.TitleRectangle, Color.White);
		}

	}
}
