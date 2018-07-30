using System;
using WindowsGame.Define.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using IJoystickInput = WindowsGame.Common.Inputs.Types.IJoystickInput;

namespace WindowsGame.Common.Inputs
{
	public class DesktopInputManager : IInputManager
	{
		private readonly IJoystickInput joystickInput;
		private readonly IMouseScreenInput mouseScreenInput;
		private readonly IKeyboardInput keyboardInput;
		private readonly IControlManager controlManager;

		public DesktopInputManager(IJoystickInput joystickInput, IKeyboardInput keyboardInput, IMouseScreenInput mouseScreenInput, IControlManager controlManager)
		{
			this.joystickInput = joystickInput;
			this.keyboardInput = keyboardInput;
			this.mouseScreenInput = mouseScreenInput;
			this.controlManager = controlManager;
		}

		public void Initialize()
		{
			mouseScreenInput.Initialize();
		}

		public void LoadContent()
		{
			mouseScreenInput.LoadContent();
		}

		public void Update(GameTime gameTime)
		{
			joystickInput.Update(gameTime);
			keyboardInput.Update(gameTime);
			mouseScreenInput.Update(gameTime);
		}

		public Vector2 Steve01()
		{
			//if (!mouseScreenInput.LeftButtonHold())
			if (!mouseScreenInput.LeftButtonPress())
			{
				return Vector2.Zero;
			}

			Rectangle collision = new Rectangle(0, 280, 200, 200);
			Vector2 position = mouseScreenInput.MosuePosition;

			Boolean contains = position.X >= collision.Left &&
								position.X <= collision.Right &
								position.Y >= collision.Top &&
								position.Y <= collision.Bottom;

			if (!contains)
			{
				return Vector2.Zero;
			}

			return position;
		}
		public Single Steve02()
		{
			//if (!mouseScreenInput.LeftButtonHold())
			if (!mouseScreenInput.LeftButtonPress())
			{
				return 0.0f;
			}

			//Rectangle collision = new Rectangle(0, 280, 200, 200);
			//Rectangle collisionOT = new Rectangle(-200, 80, 600, 600);
			Rectangle collisionOT = new Rectangle(-100, 180, 400, 400);
			Vector2 position = mouseScreenInput.MosuePosition;

			Boolean contains = position.X >= collisionOT.Left &&
								position.X <= collisionOT.Right &
								position.Y >= collisionOT.Top &&
								position.Y <= collisionOT.Bottom;

			if (!contains)
			{
				return 0.0f;
			}

			if (position.X < 0) { position.X = 0; }
			if (position.X > 200) { position.X = 200; }
			if (position.Y < 280) { position.Y = 280; }
			if (position.Y > 480) { position.Y = 480; }

			//Single width = collision.Width / 2.0f;
			const Single width = 200 / 2.0f;
			Single dataX = position.X - width;

			Single value = dataX /= width;
			return value;
		}
		public Single Steve03()
		{
			//if (!mouseScreenInput.LeftButtonHold())
			if (!mouseScreenInput.LeftButtonPress())
			{
				return 0.0f;
			}

			//Rectangle collision = new Rectangle(0, 280, 200, 200);
			//Rectangle collisionOT = new Rectangle(-200, 80, 600, 600);
			Rectangle collisionOT = new Rectangle(-100, 180, 400, 400);
			Vector2 position = mouseScreenInput.MosuePosition;

			Boolean contains = position.X >= collisionOT.Left &&
								position.X <= collisionOT.Right &
								position.Y >= collisionOT.Top &&
								position.Y <= collisionOT.Bottom;

			if (!contains)
			{
				return 0.0f;
			}

			if (position.X < 0) { position.X = 0; }
			if (position.X > 200) { position.X = 200; }
			if (position.Y < 280) { position.Y = 280; }
			if (position.Y > 480) { position.Y = 480; }

			//Single height = collision.Height / 2.0f;
			const Single height = 200 / 2.0f;
			//Single dataY = position.Y - collision.Top - height;
			Single dataY = position.Y - 280 - height;

			Single value = dataY /= height;
			return value;
		}

		public Vector2[] GetPositions()
		{
			Boolean test = controlManager.Test(mouseScreenInput.CurrMouseX, mouseScreenInput.CurrMouseY);

			return null;
		}

		public TouchLocationState[] GetStates()
		{
			return null;
		}

		public Boolean[] GetStates2()
		{
			return null;
		}

		public Boolean Escape()
		{
			return keyboardInput.KeyHold(Keys.Escape) || joystickInput.JoyHold(Buttons.Back);
		}

		public float Horizontal()
		{
			Single horz = 0.0f;

			horz = Steve02();
			if (Math.Abs(horz) > Single.Epsilon)
			{
				return horz;
			}

			horz = joystickInput.Horizontal();
			//if (Math.Abs(horz) > Single.Epsilon)//0.001f)
			if (Math.Abs(horz) > 0.001f)
			{
				return horz;
			}

			if (keyboardInput.KeyPress(Keys.Left))
			{
				horz = -1.0f;
			}
			if (keyboardInput.KeyPress(Keys.Right))
			{
				horz = 1.0f;
			}

			return horz;

			//float horz = 0.0f;
			//if (!mouseScreenInput.ButtonMove())
			//{
			//    return horz;
			//}
			//if (mouseScreenInput.CurrMouseX < 0 || mouseScreenInput.CurrMouseX > 200.0f)
			//{
			//    return horz;
			//}

			//return mouseScreenInput.CurrMouseX;
		}

		public float Vertical()
		{
			float vert = 0.0f;

			vert = Steve03();
			if (Math.Abs(vert) > Single.Epsilon)
			{
				return vert;
			}

			vert = joystickInput.Vertical();
			if (Math.Abs(vert) > 0.001f)
			{
				return vert;
			}

			if (keyboardInput.KeyPress(Keys.Up))
			{
				vert = -1.0f;
			}
			if (keyboardInput.KeyPress(Keys.Down))
			{
				vert = 1.0f;
			}

			return vert;

			//if (!mouseScreenInput.ButtonMove())
			//{
			//    return vert;
			//}
			//if (mouseScreenInput.CurrMouseY < 280.0f || mouseScreenInput.CurrMouseY > 480.0f)
			//{
			//    return vert;
			//}

			//return mouseScreenInput.CurrMouseY;
		}

	}
}
