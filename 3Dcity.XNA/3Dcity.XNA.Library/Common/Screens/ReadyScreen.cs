using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

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
			MyGame.Manager.ScoreManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Move target unconditionally.
			//Single horz = MyGame.Manager.InputManager.Horizontal();
			//Single vert = MyGame.Manager.InputManager.Vertical();
			//MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			//MyGame.Manager.SpriteManager.Update(gameTime);

			for (Int16 x = -2; x <= 736; x++)
			{
				//const UInt16 x = 157;

				////Vector2 pos1 = new Vector2(x - 28, 84);
				Vector2 pos1 = new Vector2(x - 0, 384);
				MyGame.Manager.SpriteManager.LargeTarget.SetPosition(pos1);

				//Boolean fire = MyGame.Manager.InputManager.Fire();
				//if (fire)
				{
					Vector2 pos2 = MyGame.Manager.SpriteManager.LargeTarget.Position;
					Vector2 pos3 = new Vector2(pos2.X + 28, pos2.Y + 28);
					SByte index = MyGame.Manager.CollisionManager.DetermineEnemySlot(pos2);

					String msg = String.Format("({0},{1})  [({2},{3}]] => {4}", pos2.X, pos2.Y, pos3.X, pos3.Y, index);
					//MyGame.Manager.Logger.Info(msg);
					Console.WriteLine(msg);
				}

			}


			//const UInt16 x = 200;

			////Vector2 pos1 = new Vector2(x - 28, 84);
			//Vector2 pos1 = new Vector2(x - 0, 280 - 4 - 28);
			//MyGame.Manager.SpriteManager.LargeTarget.SetPosition(pos1);

			////Boolean fire = MyGame.Manager.InputManager.Fire();
			////if (fire)
			//{
			//    Vector2 pos2 = MyGame.Manager.SpriteManager.LargeTarget.Position;
			//    Vector2 pos3 = new Vector2(pos2.X + 28, pos2.Y + 28);
			//    SByte index = MyGame.Manager.CollisionManager.DetermineEnemySlot(pos2);

			//    String msg = String.Format("({0},{1})  [({2},{3}]] => {4}", pos2.X, pos2.Y, pos3.X, pos3.Y, index);
			//    MyGame.Manager.Logger.Info(msg);
			//    //Console.WriteLine(msg);
			//}


			Engine.Game.Exit();

			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			//Vector2 pos = new Vector2(200, 276);
			//Vector2 pos = new Vector2(100, 100);
			//MyGame.Manager.SpriteManager.LargeTarget.SetPosition(pos);
			MyGame.Manager.SpriteManager.Draw();
			//DrawBullet2(pos);
			//DrawShip(pos);
			//DrawBullet(pos);
			DrawBullet2(MyGame.Manager.SpriteManager.LargeTarget.Position);


			//MyGame.Manager.LevelManager.DrawLevelOrb();

			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(-2 + 28, 74 + 28), MyGame.Manager.ImageManager.EnemyRectangles[7], Color.White);
			//DrawShip(new Vector2(-2 + 28, 74 + 28));
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(-2, 74), MyGame.Manager.ImageManager.BulletRectangles[5], Color.White);

			//const Byte b = 2;
			//DrawShip(new Vector2(160 -120 - 4, 80 + 4));
			//DrawShip(new Vector2(160 + 4, 80 + 4));
			//DrawBullet(new Vector2(160 - 4, 80 - 4));

			//DrawShip(new Vector2(b * 160 - 120 - 4, 280 - 120 - 4));
			//DrawShip(new Vector2(b * 160 + 4, 280 - 120 - 4));

			//DrawShip(new Vector2(b * 160 - 120 - 4, 280 + 4));
			//DrawShip(new Vector2(b * 160 + 4, 280 + 4));

			//DrawBullet(new Vector2(b * 160 - 4, 280 - 4));

			// Individual texture.
			MyGame.Manager.DebugManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			//MyGame.Manager.LevelManager.DrawLevelRoman();
			MyGame.Manager.ScoreManager.Draw();
		}

		private void DrawBullet(Vector2 pos)
		{
			Vector2 pos2 = pos;
			pos2.X -= 28;
			pos2.Y -= 28;
			pos = pos2;
			Rectangle rect = MyGame.Manager.ImageManager.BulletRectangles[5];
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, pos, rect, Color.White);
			DrawEntity(pos, rect);
		}
		private void DrawBullet2(Vector2 pos)
		{
			Rectangle rect = MyGame.Manager.ImageManager.BulletRectangles[5];
			DrawEntity(pos, rect);
		}
		private void DrawShip(Vector2 pos)
		{
			Rectangle rect = MyGame.Manager.ImageManager.EnemyRectangles[7];
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, pos, rect, Color.White);
			DrawEntity(pos, rect);
		}

		private void DrawEntity(Vector2 pos, Rectangle rect)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, pos, rect, Color.White);
		}
	}
}
