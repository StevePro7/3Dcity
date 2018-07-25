using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Inputs
{
	public class WindowsInputFactory : IInputFactory
	{
		private readonly IJoystickInput joystickInput;
		private readonly IMouseScreenInput mouseScreenInput;

		public WindowsInputFactory(IJoystickInput joystickInput, IMouseScreenInput mouseScreenInput)
		{
			this.joystickInput = joystickInput;
			this.mouseScreenInput = mouseScreenInput;
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			joystickInput.Update(gameTime);
			mouseScreenInput.Update(gameTime);
		}

		public Boolean Escape()
		{
			return false;
		}


		#region IInputFactory Members


		public float Horizontal()
		{
			//return joystickInput.Horizontal();

			float horz = 0.0f;
			if (!mouseScreenInput.ButtonMove())
			{
				return horz;
			}
			if (mouseScreenInput.CurrMouseX < 0 || mouseScreenInput.CurrMouseX > 200.0f)
			{
				return horz;
			}

			return mouseScreenInput.CurrMouseX;
		}

		public float Vertical()
		{
			//return joystickInput.Vertical();

			float vert = 0.0f;
			if (!mouseScreenInput.ButtonMove())
			{
				return vert;
			}
			if (mouseScreenInput.CurrMouseY < 280.0f || mouseScreenInput.CurrMouseY > 480.0f)
			{
				return vert;
			}

			return mouseScreenInput.CurrMouseY;
		}

		#endregion
	}
}
