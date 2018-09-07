using System;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		private Rectangle[] rectangles;
		private Vector2[] positions;
		private int ex, ey;
		private Byte index;
		private Single delay;
		private Vector2 pos1, pos2, pos3, pos4;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			rectangles = MyGame.Manager.ImageManager.EnemyRectangles;
			ex = 100;
			ey = 100;
			positions = GetPositions(ex, ey);
			index = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyIndex;
			delay = 1200.0f;
		}

		public override void LoadContent()
		{
			pos1 = new Vector2(0, 0);
			pos2 = new Vector2(0, 80);
			pos3 = new Vector2(0, 240);
			pos4 = new Vector2(0, 480);

			//MyGame.Manager.SoundManager.PlayMusic();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();

			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
				MyGame.Manager.BulletManager.Fire(position);
			}

			Byte myIndex = Convert.ToByte(fire);
			MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, myIndex);

			// Enemy first.
			UpdateTimer(gameTime);
			if (Timer > delay)
			{
				Timer = 0;
				index++;
				if (index >= Constants.MAX_ENEMY)
				{
					index = 0;
				}
			}

			// Then bullet and target second.
			MyGame.Manager.BulletManager.Update(gameTime);


			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.TextManager.Draw(TextDataList);

			// Sprite sheet #02.

			// Enemy first.
			Rectangle rectangle = rectangles[index];
			var position = positions[index];
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, position, rectangle, Color.White);


			// Then bullet and target second.
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();
		}

		private Vector2[] GetPositions(int sx, int sy)
		{
			positions = positions = new Vector2[Constants.MAX_ENEMY];
			positions[0] = GetPosition(25, sx, sy);
			positions[1] = GetPosition(32, sx, sy);
			positions[2] = GetPosition(40, sx, sy);
			positions[3] = GetPosition(50, sx, sy);
			positions[4] = GetPosition(64, sx, sy);
			positions[5] = GetPosition(80, sx, sy);
			positions[6] = GetPosition(96, sx, sy);
			positions[7] = GetPosition(120, sx, sy);
			return positions;
		}
		private static Vector2 GetPosition(Byte off, int tx, int ty)
		{
			return new Vector2((120 - off) / 2.0f + tx, (120 - off) / 2.0f + ty);
		}

	}
}
