﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputManager
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);

		Boolean Escape();

		Single Horizontal();
		Single Vertical();
		Boolean Fire();
		Boolean GameState();
		Boolean GameSound();
		SByte Number();
	}
}
