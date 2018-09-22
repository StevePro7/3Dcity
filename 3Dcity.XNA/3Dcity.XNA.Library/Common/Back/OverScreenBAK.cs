using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class OverScreen : BaseScreen, IScreen
	{
		private Vector2 enemysPos, enemysMid;
		private Vector2 targetPos, targetMid;
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
			targetPos = new Vector2(x - 20,  y - 8);
			//targetPos = new Vector2(data.TargetX, data.TargetY);

			//Process();


			enemysRect = MyGame.Manager.ImageManager.EnemyRectangles[7];
			targetRect = MyGame.Manager.ImageManager.TargetLargeRectangle;
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Boolean gameState = MyGame.Manager.InputManager.GameState();
			if (gameState)
			{
				Boolean collision = MyGame.Manager.CollisionManager.ColorCollision(enemysPos, targetPos);
				MyGame.Manager.Logger.Info(collision.ToString());
			}

			//if (GamePause)
			//{
			//    return (Int32)CurrScreen;
			//}

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
			Process();

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
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

		private void Process()
		{
			enemysMid = GetMidPoint(enemysPos, 120);
			targetMid = GetMidPoint(targetPos, 64);
			float dist = Vector2.Distance(enemysMid, targetMid);
			float diSq = Vector2.DistanceSquared(enemysMid, targetMid);

			String msg = String.Format("({0},{1})  ({2},{3})  {4}  {5}", enemysMid.X, enemysMid.Y, targetMid.X, targetMid.Y, dist,
				diSq);
			MyGame.Manager.Logger.Info(msg);
		}

		public Vector2 GetMidPoint(Vector2 pos, Single size)
		{
			Single half = size / 2.0f;
			return new Vector2(pos.X + half, pos.Y + half);
		}

	}
}
