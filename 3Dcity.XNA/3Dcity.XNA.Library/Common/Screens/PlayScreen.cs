using System;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Define;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public Int32 Update(GameTime gameTime)
		{
			//var positions = MyGame.Manager.InputManager.GetPositions();

			//MyGame.Manager.RenderManager.UpdateStar(gameTime);
			//MyGame.Manager.RenderManager.UpdateGrid(gameTime);

			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			//if (Math.Abs(horz) > 0.4)
			//{
			MyGame.Manager.SpriteManager.Update(gameTime, horz, vert);
			//}

			return (Int32)ScreenType.Play;
		}

		public override void Draw()
		{
			// TODO delegate this to device manager??
			Engine.Game.Window.Title = GetType().Name;// Globalize.GAME_TITLE;

			//Engine.SpriteBatch.Draw(Assets.Target80Texture, new Vector2(100, 100), Color.White);
			//MyGame.Manager.RenderManager.Draw();

			MyGame.Manager.SpriteManager.Draw();
			base.Draw();
		}

	}
}
