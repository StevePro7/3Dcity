using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ResumeScreen : BaseScreen, IScreen
	{
		private Vector2[] boundsPos;
		private Vector2 targetPos;
		private Vector2 enemysPos;

		private UInt16 delay;
		private Byte goal;

		public override void Initialize()
		{
			delay = 1000;
			goal = 100;

			int x = 200;
			int y = 100;
			int s = 120;
			int u = 80;
			int v = 80;
			boundsPos = new Vector2[4];
			boundsPos[0] = new Vector2(x - u, y - v);
			boundsPos[1] = new Vector2(x + s, y - v);
			boundsPos[2] = new Vector2(x - u, y + s);
			boundsPos[3] = new Vector2(x + s, y + s);


			enemysPos = new Vector2(x, y);
			MyGame.Manager.EnemyManager.EnemyList[0].SetPosition(enemysPos);
			MyGame.Manager.EnemyManager.EnemyList[0].Reset();

			targetPos = new Vector2(MyGame.Manager.ConfigManager.GlobalConfigData.TargetX, MyGame.Manager.ConfigManager.GlobalConfigData.TargetY);
			MyGame.Manager.BulletManager.BulletList[0].SetPosition(targetPos);
			MyGame.Manager.BulletManager.BulletList[0].Reset(0);
			MyGame.Manager.SpriteManager.LargeTarget.SetPosition(targetPos);
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			//base.Update(gameTime);
			//if (GamePause)
			//{
			//    return (Int32)CurrScreen;
			//}


			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			//base.Draw();

			for (int i = 0; i < 4; i++)
			{
				Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, boundsPos[i], MyGame.Manager.ImageManager.JoyButtonRectangles[0], Color.White);
			}

			MyGame.Manager.EnemyManager.EnemyList[0].Draw();
			MyGame.Manager.BulletManager.BulletList[0].Draw();

			if (MyGame.Manager.ConfigManager.GlobalConfigData.EnemyIndex > 0)
			{
				MyGame.Manager.SpriteManager.LargeTarget.Draw();
			}
		}

	}
}
