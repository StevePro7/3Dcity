using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class OverScreen : BaseScreen, IScreen
	{
		private Vector2 enemysPos;
		private Vector2 targetPos;
		private Rectangle enemysRect;
		private Rectangle targetRect;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			GlobalConfigData data = MyGame.Manager.ConfigManager.GlobalConfigData;
			enemysPos = new Vector2(data.EnemysX, data.EnemysY);

			Single x = (120 - 64) / 2.0f + data.EnemysX;
			Single y = (120 - 64) / 2.0f + data.EnemysY;
			targetPos = new Vector2(x, y);
			//targetPos = new Vector2(data.TargetX, data.TargetY);

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

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			//MyGame.Manager.IconManager.DrawControls();
			//MyGame.Manager.ScoreManager.Draw();

			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemysPos, enemysRect, Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, targetPos, targetRect, Color.White);

			//// Sprite sheet #02.
			//MyGame.Manager.DebugManager.Draw();

			//MyGame.Manager.EnemyManager.Draw();
			//MyGame.Manager.SpriteManager.Draw();

			//MyGame.Manager.ExplosionManager.Draw();


			// Text data last!
			//MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
}
