using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;

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

			//return joystickInput.Horizontal();

			horz = joystickInput.Horizontal();
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
