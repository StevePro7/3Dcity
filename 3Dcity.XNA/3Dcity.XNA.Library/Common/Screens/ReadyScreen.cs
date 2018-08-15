using System;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Master;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen 
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			MyGame.Manager.ExplosionManager.Update(gameTime);
			//MyGame.Manager.BulletManager.Update(gameTime);
			return (Int32)ScreenType.Ready;
		}

		public override void Draw()
		{
			base.Draw();

			MyGame.Manager.RenderManager.Draw();

			Engine.SpriteBatch.Draw(Assets.Enemy25Texture, new Vector2(50, 250), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy32Texture, new Vector2(100, 240), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy40Texture, new Vector2(150, 220), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy50Texture, new Vector2(200, 200), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy64Texture, new Vector2(300, 200), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy80Texture, new Vector2(400, 160), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy96Texture, new Vector2(500, 140), Color.White);
			Engine.SpriteBatch.Draw(Assets.Enemy120Texture, new Vector2(640, 120), Color.White);


			MyGame.Manager.ExplosionManager.Draw();
			//MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			MyGame.Manager.TextManager.Draw(TextDataList);
			
		}

	}
}
