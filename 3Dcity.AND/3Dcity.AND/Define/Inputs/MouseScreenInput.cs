using System;
using WindowsGame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame.Define.Inputs
{
	public interface IMouseInput
	{
		// Methods.
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);

		Boolean LeftButtonPress();
		Boolean LeftButtonHold();
		Boolean RightButtonPress();
		Boolean RightButtonHold();

		// Properties.
		Int32 CurrMouseX { get; }
		Int32 CurrMouseY { get; }
		Vector2 MosuePosition { get; }
		//ButtonState currLeftButtonState { get; }
		//ButtonState prevLeftButtonState { get; }
		//ButtonState currRightButtonState { get; }
		//ButtonState prevRightButtonState { get; }
	}

	public class MouseScreenInput : IMouseInput
	{
		private ButtonState currLeftButtonState;
		private ButtonState prevLeftButtonState;
		private ButtonState currRightButtonState;
		private ButtonState prevRightButtonState;
		private Byte maxTouches;

		public void Initialize()
		{
			CurrMouseX = 0;
			CurrMouseY = 0;
			MosuePosition = Vector2.Zero;
			maxTouches = 0;
		}

		public void LoadContent()
		{
			maxTouches = MyGame.Manager.ConfigManager.PlatformConfigData.MaxTouches;
		}

		public void Update(GameTime gameTime)
		{
			prevLeftButtonState = currLeftButtonState;
			prevRightButtonState = currRightButtonState;

			MouseState mouseState = Mouse.GetState();
			CurrMouseX = mouseState.X;
			CurrMouseY = mouseState.Y;

			Vector2 mousePosition = Vector2.Zero;
			mousePosition.X = CurrMouseX;
			mousePosition.Y = CurrMouseY;
			MosuePosition = mousePosition;

			currLeftButtonState = mouseState.LeftButton;
			currRightButtonState = mouseState.RightButton;
		}

		public Boolean LeftButtonPress()
		{
			return ButtonPress(currLeftButtonState);
			//return ButtonState.Pressed == currLeftButtonState;
		}
		public Boolean LeftButtonHold()
		{
			return ButtonHold(currLeftButtonState, prevLeftButtonState);
			//return ButtonState.Pressed == currLeftButtonState && ButtonState.Released == prevLeftButtonState;
		}

		public Boolean RightButtonPress()
		{
			return ButtonPress(currRightButtonState);
			//return ButtonState.Pressed == currRightButtonState;
		}
		public Boolean RightButtonHold()
		{
			return ButtonHold(currRightButtonState, prevRightButtonState);
			//return ButtonState.Pressed == currRightButtonState && ButtonState.Released == prevRightButtonState;
		}

		private static Boolean ButtonPress(ButtonState buttonState)
		{
			return ButtonState.Pressed == buttonState;
		}
		private static Boolean ButtonHold(ButtonState currButtonState, ButtonState prevButtonState)
		{
			return ButtonState.Pressed == currButtonState && ButtonState.Released == prevButtonState;
		}

		// Properties.
		public Int32 CurrMouseX { get; private set; }
		public Int32 CurrMouseY { get; private set; }
		public Vector2 MosuePosition { get; private set; }
		//public ButtonState currLeftButtonState { get; private set; }
		//public ButtonState prevLeftButtonState { get; private set; }
		//public ButtonState currRightButtonState { get; private set; }
		//public ButtonState prevRightButtonState { get; private set; }
	}
}
