using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame.Common.Inputs.Types
{
	public interface IJoystickInput
	{
		void Initialize();
		void Update(GameTime gameTime);

		Single Horizontal();
		Single Vertical();

		//Single Rotate();
		//Boolean JoyHold(Buttons button);
		//Boolean JoyMove(Buttons button);

		//void SetMotors(Single leftMotor, Single rightMotor);
		//void ResetMotors();
	}

	public class JoystickInput : IJoystickInput
	{
		private GamePadState currGamePadState;
		private GamePadState prevGamePadState;

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			// http://xona.com/2010/05/03.html.
			prevGamePadState = currGamePadState;
			currGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
		}

		public Single Horizontal()
		{
			return currGamePadState.ThumbSticks.Left.X;
		}

		public Single Vertical()
		{
			return currGamePadState.ThumbSticks.Left.Y;
		}

	}
}
