using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Master.Inputs;

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
			if (joystickInput.JoyHold(Buttons.Start))
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

		public SByte Number()
		{
			if (keyboardInput.KeyPress(Keys.D1))
			{
				return 0;
			}
			if (keyboardInput.KeyPress(Keys.D2))
			{
				return 1;
			}
			if (keyboardInput.KeyPress(Keys.D3))
			{
				return 2;
			}
			if (keyboardInput.KeyPress(Keys.D4))
			{
				return 3;
			}
			if (keyboardInput.KeyPress(Keys.D5))
			{
				return 4;
			}
			if (keyboardInput.KeyPress(Keys.D6))
			{
				return 5;
			}
			if (keyboardInput.KeyPress(Keys.D7) || keyboardInput.KeyPress(Keys.Q))
			{
				return 6;
			}
			if (keyboardInput.KeyPress(Keys.D8) || keyboardInput.KeyPress(Keys.W))
			{
				return 7;
			}
			if (keyboardInput.KeyPress(Keys.E))
			{
				return 8;
			}
			if (keyboardInput.KeyPress(Keys.R))
			{
				return 9;
			}
			if (keyboardInput.KeyPress(Keys.T))
			{
				return 10;
			}
			if (keyboardInput.KeyPress(Keys.Y))
			{
				return 11;
			}

			return Constants.INVALID_INDEX;
		}

	}
}
