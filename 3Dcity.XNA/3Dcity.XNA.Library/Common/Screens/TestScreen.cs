using System;
using WindowsGame.Common.Sprites;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreen, IScreen
	{
		private Vector2[] boxPositions;
		private SByte number;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			number = Constants.INVALID_INDEX;
		}

		public override void LoadContent()
		{
			boxPositions = GetBoxPositions();
			MyGame.Manager.EnemyManager.Reset(1);
			MyGame.Manager.ExplosionManager.Reset(8, 1000);
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);

			number = MyGame.Manager.InputManager.Number();
			if (Constants.INVALID_INDEX != number)
			{
				number = 0;
				Byte explodeIndex = (Byte) number;
				Explosion explosion = MyGame.Manager.ExplosionManager.ExplosionList[explodeIndex];
				if (!explosion.IsExploding)
				{
					Vector2 position = GetPosition(explodeIndex);
					Byte diff = (Byte)(number % 2);
					ExplodeType explodeType = (ExplodeType)diff;
					MyGame.Manager.ExplosionManager.LoadContent(explodeIndex, explodeType);
					MyGame.Manager.ExplosionManager.Explode(explodeIndex, position);
				}

				//MyGame.Manager.Logger.Info(number.ToString());
			}

			MyGame.Manager.ExplosionManager.Update(gameTime);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.Draw();
			for (Byte index = 0; index < Constants.MAX_ENEMY_SPAWN; index++)
			{
				Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			}

			Vector2 enemyPos = new Vector2(350-120, 280 + 80);
			Rectangle enemyRect = MyGame.Manager.ImageManager.EnemyRectangles[3];
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemyPos, enemyRect, Color.White);

			MyGame.Manager.ExplosionManager.Draw();

			//if (Constants.INVALID_INDEX != number)
			//{
			//    // BIG
			//    //Vector2 bombsPos = new Vector2(enemyPos.X - 20, enemyPos.Y - 20);

			//    // SMALL
			//    Vector2 bombsPos = new Vector2(enemyPos.X + 20, enemyPos.Y + 20);

			//    Rectangle bombsRect = MyGame.Manager.ImageManager.ExplodeRectangles[0][(Byte) number];
			//    Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, bombsPos, bombsRect, Color.White);
			//}

			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private static Vector2 GetPosition(Byte theNumber)
		{
			if (0 == theNumber)
			{
				return new Vector2(150, 100);
			}

			return Vector2.Zero;
		}

		private Vector2[] GetBoxPositions()
		{
			const Single hi = 80 + Constants.GameOffsetY;
			const Single lo = 280 + Constants.GameOffsetY;

			boxPositions = new Vector2[Constants.MAX_ENEMY_SPAWN];
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
