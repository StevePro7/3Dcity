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
			horz = joystickInput.Horizontal();
			if (Math.Abs(horz) > Single.Epsilon)
			{
				return horz;
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
			vert = joystickInput.Vertical();
			if (Math.Abs(vert) > Single.Epsilon)
			{
				return vert;
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

	}
}
