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

		public Boolean Back()
		{
			// Mouse.
			if (mouseScreenInput.LeftButtonHold())
			{
				Boolean test = controlManager.CheckStatusBar(mouseScreenInput.MousePosition);
				if (test)
				{
					return true;
				}
			}

			// Joystick.
			if (joystickInput.JoyHold(Buttons.Back) || joystickInput.JoyHold(Buttons.B))
			{
				return true;
			}

			// Keyboard.
			if (keyboardInput.KeyHold(Keys.B))
			{
				return true;
			}
			if (keyboardInput.KeyHold(Keys.Escape))
			{
				return true;
			}

			return false;
		}

		public Boolean Escape()
		{
			return keyboardInput.KeyHold(Keys.Escape) || joystickInput.JoyHold(Buttons.Back) || joystickInput.JoyHold(Buttons.B);
		}

		public Single LittleHorz()
		{
			Single horz = 0.0f;

			// Mouse.
			if (mouseScreenInput.LeftButtonPress())
			{
				horz = controlManager.CheckJoyPadTiny(mouseScreenInput.MousePosition);
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


		public Single Horizontal()
		{
			Single horz = 0.0f;

			// Mouse.
			if (mouseScreenInput.LeftButtonPress())
			{
				horz = controlManager.CheckJoyPadHorz(mouseScreenInput.MousePosition);
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
				vert = controlManager.CheckJoyPadVert(mouseScreenInput.MousePosition);
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
				return true;
			}
			if (mouseScreenInput.LeftButtonPress())
			{
				Boolean test = controlManager.CheckJoyPadFire(mouseScreenInput.MousePosition);
				if (test)
				{
					return true;
				}
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
				Boolean test = controlManager.CheckGameState(mouseScreenInput.MousePosition);
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
				Boolean test = controlManager.CheckGameSound(mouseScreenInput.MousePosition);
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

		public Boolean CenterPos()
		{
			// Mouse.
			if (mouseScreenInput.LeftButtonHold())
			{
				Boolean test = controlManager.CheckCenterPos(mouseScreenInput.MousePosition);
				if (test)
				{
					return true;
				}
			}

			// Joystick.
			if (joystickInput.JoyHold(Buttons.A) || joystickInput.JoyHold(Buttons.Start))
			{
				return true;
			}

			// Keyboard.
			if (keyboardInput.KeyHold(Keys.Enter) || keyboardInput.KeyHold(Keys.Space))
			{
				return true;
			}

			return false;
		}

		public Boolean TitleMode()
		{
			// Mouse.
			if (mouseScreenInput.LeftButtonHold())
			{
				Boolean test = controlManager.CheckTitleMode(mouseScreenInput.MousePosition);
				if (test)
				{
					return true;
				}
			}

			return false;
		}

		public Boolean StatusBar()
		{
			// Mouse.
			if (mouseScreenInput.LeftButtonHold())
			{
				Boolean test = controlManager.CheckStatusBar(mouseScreenInput.MousePosition);
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
			if (keyboardInput.KeyHold(Keys.F1))
			{
				return true;
			}

			return false;
		}

		public Boolean LeftsSide()
		{
			// Mouse.
			if (!mouseScreenInput.LeftButtonHold())
			{
				return false;
			}
			Boolean test = controlManager.CheckLeftsSide(mouseScreenInput.MousePosition);
			if (test)
			{
				return true;
			}

			return false;
		}

		public Boolean RightSide()
		{
			// Mouse.
			if (!mouseScreenInput.LeftButtonHold())
			{
				return false;
			}
			Boolean test = controlManager.CheckRightSide(mouseScreenInput.MousePosition);
			if (test)
			{
				return true;
			}

			return false;
		}

		public SByte Number()
		{
			if (keyboardInput.KeyHold(Keys.D1))
			{
				return 0;
			}
			if (keyboardInput.KeyHold(Keys.D2))
			{
				return 1;
			}
			if (keyboardInput.KeyHold(Keys.D3))
			{
				return 2;
			}
			if (keyboardInput.KeyHold(Keys.D4))
			{
				return 3;
			}
			if (keyboardInput.KeyHold(Keys.D5))
			{
				return 4;
			}
			if (keyboardInput.KeyHold(Keys.D6))
			{
				return 5;
			}
			if (keyboardInput.KeyHold(Keys.D7))// || keyboardInput.KeyHold(Keys.Q))
			{
				return 6;
			}
			if (keyboardInput.KeyHold(Keys.D8))// || keyboardInput.KeyHold(Keys.W))
			{
				return 7;
			}
			//if (keyboardInput.KeyHold(Keys.E))
			//{
			//    return 8;
			//}
			//if (keyboardInput.KeyHold(Keys.R))
			//{
			//    return 9;
			//}
			//if (keyboardInput.KeyHold(Keys.T))
			//{
			//    return 10;
			//}
			//if (keyboardInput.KeyHold(Keys.Y))
			//{
			//    return 11;
			//}

			return Constants.INVALID_INDEX;
		}

	}
}
