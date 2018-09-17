using System;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LevelScreen : BaseScreen, IScreen
	{
		private Vector2[] boxPositions;
		private Rectangle[] enemyRects;
		private Vector2 enemyPos;
		private Byte enemyFrame;
		private Rectangle bulletRect;
		private Vector2 bulletPos;
		private Boolean fire;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			boxPositions = GetBoxPositions();
			//MyGame.Manager.BulletManager.Reset(1, 200, 100);

			enemyPos = new Vector2(MyGame.Manager.ConfigManager.GlobalConfigData.EnemysX, MyGame.Manager.ConfigManager.GlobalConfigData.EnemysY);
			enemyRects = MyGame.Manager.ImageManager.EnemyRectangles;
			enemyFrame = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyFrame;

			bulletRect = MyGame.Manager.ImageManager.BulletRectangles[5];
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			fire = MyGame.Manager.InputManager.Fire();
			bulletPos = MyGame.Manager.SpriteManager.LargeTarget.Position;

			SByte number = MyGame.Manager.InputManager.Number();
			if (Constants.INVALID_INDEX != number)
			{
				MyGame.Manager.Logger.Info(bulletPos.ToString());
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			//MyGame.Manager.ScoreManager.Draw();

			// Sprite sheet #02.

			// Enemy
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemyPos, enemyRects[enemyFrame], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(36, 152), enemyRects[7], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(160, 152), enemyRects[7], Color.White);
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(190, 280), enemyRects[7], Color.White);

			// Coll box.
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			}

			// Bullet.
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, bulletPos, bulletRect, Color.White);

			//MyGame.Manager.EnemyManager.Draw();
			//MyGame.Manager.BulletManager.Draw();


			// Target.
			if (fire)
			{
				MyGame.Manager.SpriteManager.Draw();
			}

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private Vector2[] GetBoxPositions()
		{
			const Single hi = 80 + Constants.GameOffsetY;
			const Single lo = 280 + Constants.GameOffsetY;

			boxPositions = new Vector2[Constants.MAX_ENEMYS_SPAWN];
			boxPositions[0] = new Vector2(160 * 0, hi);
			boxPositions[1] = new Vector2(160 * 1, hi);
			boxPositions[2] = new Vector2(160 * 2, hi);
			boxPositions[3] = new Vector2(160 * 3, hi);
			boxPositions[4] = new Vector2(160 * 4, hi);

			const Byte offset = 190;
			boxPositions[5] = new Vector2(160 * 0 + offset, lo);
			boxPositions[6] = new Vector2(160 * 1 + offset, lo);
			boxPositions[7] = new Vector2(160 * 2 + offset, lo);

			return boxPositions;
		}

	}
}
