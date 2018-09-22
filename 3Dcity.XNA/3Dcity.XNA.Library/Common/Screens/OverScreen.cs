using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class OverScreen : BaseScreen, IScreen
	{
		private Vector2 enemysPos;
		private Vector2 targetPos;
		private Rectangle enemysRect;
		private Rectangle targetRect;
		private Boolean collision;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			collision = false;
		}

		public override void LoadContent()
		{
			GlobalConfigData data = MyGame.Manager.ConfigManager.GlobalConfigData;
			enemysPos = new Vector2(data.EnemysX, data.EnemysY);

			Single x = (120 - 64) / 2.0f + data.EnemysX;
			Single y = (120 - 64) / 2.0f + data.EnemysY;
			targetPos = new Vector2(x - 0,  y - 0);

			enemysRect = MyGame.Manager.ImageManager.EnemyRectangles[7];
			targetRect = MyGame.Manager.ImageManager.TargetLargeRectangle;
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}


			// LOG if color collision detection or not...
			Boolean test = MyGame.Manager.InputManager.Fire();
			if (test)
			{
				collision = MyGame.Manager.CollisionManager.ColorCollision(enemysPos, targetPos);
				MyGame.Manager.Logger.Info(collision.ToString());
			}


			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			if (0 == horz && 0 == vert)
			{
				return (Int32)CurrScreen;
			}

			Vector2 tempPos = targetPos;
			tempPos.X += horz;
			tempPos.Y += vert;
			targetPos = tempPos;
			MyGame.Manager.SpriteManager.LargeTarget.SetPosition(targetPos);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemysPos, enemysRect, Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, targetPos, targetRect, Color.White);

			// Text data last!
			//MyGame.Manager.TextManager.Draw(TextDataList);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, collision.ToString(), Vector2.Zero, Color.White);
		}

	}
}
