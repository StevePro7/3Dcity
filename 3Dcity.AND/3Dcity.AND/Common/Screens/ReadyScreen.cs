using System;
using WindowsGame.Common.Static;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Define;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen 
	{
		private Vector2[] positions;
		private TouchLocationState[] states;
		private Vector2[,] matrix;

		private const Byte MAX_X = 2;
		private const Byte MAX_Y = 10;

		public override void Initialize()
		{
			matrix = GetMatrix();
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public Int32 Update(GameTime gameTime)
		{
			positions = MyGame.Manager.InputManager.GetPositions();
			states = MyGame.Manager.InputManager.GetStates();
			return (Int32)ScreenType.Ready;
		}

		public override void Draw()
		{
			// TODO delegate this to device manager??
			Engine.Game.Window.Title = GetType().Name;// Globalize.GAME_TITLE;

			if (null != positions)
			{
				int max = positions.Length;
				for (var i = 0; i < max; i++)
				{
					string text = String.Format("({0}, {1})", positions[i].X, positions[i].Y);
					Engine.SpriteBatch.DrawString(Assets.EmulogicFont, text, matrix[0, i], Color.Yellow);
				}
			}

			if (null != states)
			{
				int max = states.Length;
				for (var i = 0; i < max; i++)
				{
					string text = states[i].ToString();
					Engine.SpriteBatch.DrawString(Assets.EmulogicFont, text, matrix[1, i], Color.White);
				}
			}

			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, "HELLO", matrix[0, 0], Color.Yellow);
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, "0", matrix[0, 0] Color.Yellow );

			base.Draw();
		}

		private Vector2[,] GetMatrix()
		{
			matrix = new Vector2[MAX_X, MAX_Y];
			for (Byte x = 0; x < MAX_X; x++)
			{
				for (Byte y = 0; y < MAX_Y; y++)
				{
					matrix[x, y] = new Vector2((x * 450) + 20, (y * 30) + 20);
				}
			}

			return matrix;
		}

	}
}
