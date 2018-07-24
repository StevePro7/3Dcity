using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Inputs
{
	public class WindowsInputFactory : IInputFactory
	{
		private readonly IJoystickInput joystickInput;

		public WindowsInputFactory(IJoystickInput joystickInput)
		{
			this.joystickInput = joystickInput;
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			joystickInput.Update(gameTime);
		}

		public Boolean Escape()
		{
			return false;
		}


		#region IInputFactory Members


		public float Horizontal()
		{
			return joystickInput.Horizontal();
		}

		public float Vertical()
		{
			return joystickInput.Vertical();
		}

		#endregion
	}
}
