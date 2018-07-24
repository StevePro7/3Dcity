using System;
using WindowsGame.Common.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IInputManager 
	{
		void Initialize();
		void Update(GameTime gameTime);

		Single Horizontal();
		Single Vertical();
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
			inputFactory.Update(gameTime);
		}

		public Boolean Escape()
		{
			return false;
		}


		#region IInputManager Members


		public float Horizontal()
		{
			return inputFactory.Horizontal();
		}

		public float Vertical()
		{
			return inputFactory.Vertical();
		}

		#endregion
	}
}
