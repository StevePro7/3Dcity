using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame.Master.Inputs
{
	public interface IJoystickInput
	{
		void Initialize();
		void Initialize(Byte theMaxPlayers, GamePadDeadZone theGamePadDeadZone);
		void Update(GameTime gameTime);

		Boolean JoyHold(Buttons button);
		Boolean JoyPress(Buttons button);
		void SetMotors(Single leftMotor, Single rightMotor);
		void SetMotors(PlayerIndex playerIndex, Single leftMotor, Single rightMotor);
		void ResetMotors();
		void ResetMotors(PlayerIndex playerIndex);

		PlayerIndex CurrPlayerIndex { get; }
		GamePadState[] CurrGamePadState { get; }
		GamePadState[] PrevGamePadState { get; }
	}

	public class JoystickInput : IJoystickInput
	{
		private Byte maxPlayers;
		private GamePadDeadZone gamePadDeadZone;

		private const Byte MAX_PLAYERS = 4;

		// http://xona.com/2010/05/03.html
		// If not specified then IndependentAxes is the default.
		// However, often circular it's better for 4-way motion.
		public void Initialize()
		{
			Initialize(MAX_PLAYERS, GamePadDeadZone.Circular);
		}
		public void Initialize(Byte theMaxPlayers, GamePadDeadZone theGamePadDeadZone)
		{
			maxPlayers = theMaxPlayers;
			gamePadDeadZone = theGamePadDeadZone;

			CurrGamePadState = new GamePadState[maxPlayers];
			PrevGamePadState = new GamePadState[maxPlayers];
		}

		public void Update(GameTime gameTime)
		{
			for (Byte index = 0; index < maxPlayers; index++)
			{
				PlayerIndex playerIndex = (PlayerIndex)index;
				PrevGamePadState[index] = CurrGamePadState[index];
				CurrGamePadState[index] = GamePad.GetState(playerIndex, gamePadDeadZone);
			}
		}

		public Boolean JoyHold(Buttons button)
		{
			Byte index = (Byte)CurrPlayerIndex;
			return CurrGamePadState[index].IsButtonDown(button) && PrevGamePadState[index].IsButtonUp(button);

		}
		public Boolean JoyPress(Buttons button)
		{
			Byte index = (Byte)CurrPlayerIndex;
			return CurrGamePadState[index].IsButtonDown(button);
		}

		public void SetMotors(Single leftMotor, Single rightMotor)
		{
			SetMotors(CurrPlayerIndex, leftMotor, rightMotor);
		}

		public void SetMotors(PlayerIndex playerIndex, Single leftMotor, Single rightMotor)
		{
			Byte index = (Byte)playerIndex;
			if (!CurrGamePadState[index].IsConnected)
			{
				return;
			}

			GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
		}

		public void ResetMotors()
		{
			ResetMotors(CurrPlayerIndex);
		}

		public void ResetMotors(PlayerIndex playerIndex)
		{
			SetMotors(playerIndex, 0, 0);
		}

		public PlayerIndex CurrPlayerIndex { get; private set; }
		public GamePadState[] CurrGamePadState { get; private set; }
		public GamePadState[] PrevGamePadState { get; private set; }
	}
}
