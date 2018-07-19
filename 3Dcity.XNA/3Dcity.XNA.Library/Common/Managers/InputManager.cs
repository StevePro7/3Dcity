using System;
using WindowsGame.Common.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IInputManager 
	{
		void Initialize();
		void Update(GameTime gameTime);

		Boolean Escape();
	}

	public class InputManager : IInputManager 
	{
		private readonly IInputFactory inputFactory;

		public InputManager(IInputFactory inputFactory)
		{
			this.inputFactory = inputFactory;
		}

		public void Initialize()
		{
			inputFactory.Initialize();
		}

		public void Update(GameTime gameTime)
		{
		}

		public Boolean Escape()
		{
			return false;
		}

	}
}
