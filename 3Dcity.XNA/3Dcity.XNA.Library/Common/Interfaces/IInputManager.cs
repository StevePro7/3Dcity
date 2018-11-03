using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputManager
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);

		Boolean Back();
		Boolean Escape();

		Boolean Accelerate();
		Single LittleHorz();
		Single Horizontal();
		Single Vertical();
		Boolean Fire();
		Boolean Select();
		Boolean SelectJoystick();
		Boolean SelectWithout();
		Boolean GameState();
		Boolean GameSound();
		Boolean CenterPos();
		Boolean TitleMode();
		Boolean StatusBar();
		Boolean LeftsSide();
		Boolean RightSide();
		SByte Number();
	}
}
