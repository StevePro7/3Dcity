using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Define.Inputs;

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
			joystickInput.Initialize();
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

		public Vector2[] GetPositions()
		{
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

		public Single Horizontal()
		{
			Single horz = 0.0f;

			// Mouse.
			if (mouseScreenInput.LeftButtonPress())
			{
				horz = controlManager.CheckJoyPadHorz(mouseScreenInput.MosuePosition);
				if (Math.Abs(horz) > Single.Epsilon)
				{
					return horz;
				}
			}

			// Joystick.
			Byte index = (Byte)joystickInput.CurrPlayerIndex;
			Single floatX = joystickInput.CurrGamePadState[index].ThumbSticks.Left.X;

			if (floatX < -Constants.JoystickTolerance || floatX > Constants.JoystickTolerance)
			{
				return floatX;
			}
			if (joystickInput.CurrGamePadState[index].IsButtonDown(Buttons.DPadLeft))
			{
				return -1.0f;
			}
			if (joystickInput.CurrGamePadState[index].IsButtonDown(Buttons.DPadRight))
			{
				return 1.0f;
			}

			// Keyboard.
			if (keyboardInput.KeyPress(Keys.Left))
			{
				horz = -1.0f;
			}
			if (keyboardInput.KeyPress(Keys.Right))
			{
				horz = 1.0f;
			}

			return horz;
		}

		public Single Vertical()
		{
			Single vert = 0.0f;

			// Mouse.
			if (mouseScreenInput.LeftButtonPress())
			{
				vert = controlManager.CheckJoyPadVert(mouseScreenInput.MosuePosition);
				if (Math.Abs(vert) > Single.Epsilon)
				{
					return vert;
				}
			}

			// Joystick.
			Byte index = (Byte)joystickInput.CurrPlayerIndex;
			Single floatY = joystickInput.CurrGamePadState[index].ThumbSticks.Left.Y;

			if (floatY < -Constants.JoystickTolerance || floatY > Constants.JoystickTolerance)
			{
				return -floatY;
			}
			if (joystickInput.CurrGamePadState[index].IsButtonDown(Buttons.DPadUp))
			{
				return -1.0f;
			}
			if (joystickInput.CurrGamePadState[index].IsButtonDown(Buttons.DPadDown))
			{
				return 1.0f;
			}

			// Keyboard.
			if (keyboardInput.KeyPress(Keys.Up))
			{
				vert = -1.0f;
			}
			if (keyboardInput.KeyPress(Keys.Down))
			{
				vert = 1.0f;
			}

			return vert;
		}

		public Boolean Fire()
		{
			// Mouse.
			if (mouseScreenInput.RightButtonPress())
			{
				//Boolean fire = controlManager.CheckJoyPadFire(mouseScreenInput.MosuePosition);
				//if (fire)
				//{
					return true;
				//}
			}

			// Joystick.
			if (joystickInput.JoyPress(Buttons.A))
			{
				return true;
			}

			// Keyboard.
			if (keyboardInput.KeyPress(Keys.Space))
			{
				return true;
			}

			return false;
		}

		public Boolean GameState()
		{
			// Mouse.
			if (mouseScreenInput.LeftButtonHold())
			{
				Boolean test = controlManager.CheckGameState(mouseScreenInput.MosuePosition);
				if (test)
				{
					return true;
				}
			}

			// Joystick.
			if (joystickInput.JoyHold(Buttons.X))
			{
				return true;
			}

			// Keyboard.
			if (keyboardInput.KeyHold(Keys.Enter))
			{
				return true;
			}

			return false;
		}

		public Boolean GameSound()
		{
			// Mouse.
			if (mouseScreenInput.LeftButtonHold())
			{
				Boolean test = controlManager.CheckGameSound(mouseScreenInput.MosuePosition);
				if (test)
				{
					return true;
				}
			}

			// Joystick.
			if (joystickInput.JoyHold(Buttons.Y))
			{
				return true;
			}

			// Keyboard.
			if (keyboardInput.KeyHold(Keys.S))
			{
				return true;
			}

			return false;
		}

	}
}
