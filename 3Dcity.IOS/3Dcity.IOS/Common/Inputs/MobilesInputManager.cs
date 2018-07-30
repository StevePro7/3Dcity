using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Inputs.Types;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Define.Managers;

namespace WindowsGame.Common.Inputs
{
	public class MobilesInputManager : IInputManager
	{
		private readonly IJoystickInput joystickInput;
		private readonly ITouchScreenInput touchScreenInput;
		private readonly IControlManager controlManager;
		private readonly IResolutionManager resolutionManager;

		public MobilesInputManager(IJoystickInput joystickInput, ITouchScreenInput touchScreenInput, IControlManager controlManager, IResolutionManager resolutionManager)
		{
			this.joystickInput = joystickInput;
			this.touchScreenInput = touchScreenInput;
			this.controlManager = controlManager;
			this.resolutionManager = resolutionManager;
		}

		public void Initialize()
		{
			touchScreenInput.Initialize();
		}

		public void LoadContent()
		{
			touchScreenInput.LoadContent();
		}

		public void Update(GameTime gameTime)
		{
			touchScreenInput.Update(gameTime);
		}

		public Vector2[] GetPositions()
		{
			return touchScreenInput.TouchPositionsX;
		}

		public TouchLocationState[] GetStates()
		{
			return touchScreenInput.TouchStatesX;
		}

		public Boolean[] GetStates2()
		{
			return null;
		}

		public Boolean Escape()
		{
			return false;
		}

		public float Horizontal()
		{
			return 0.0f;
		}

		public float Vertical()
		{
			return 0.0f;
		}

	}
}