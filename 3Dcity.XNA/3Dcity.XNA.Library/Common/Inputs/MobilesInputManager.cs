using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Inputs.Types;		// TODO move to engine!
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Define.Inputs;
using WindowsGame.Define.Managers;

namespace WindowsGame.Common.Inputs
{
	public class MobilesInputManager : IInputManager
	{
		private readonly IJoystickInput joystickInput;
		private readonly ITouchScreenInput touchScreenInput;
		private readonly IControlManager controlManager;
		private readonly IResolutionManager resolutionManager;

		private Vector2 viewPortVector2;
		private Matrix invertTransformationMatrix;
		private Byte maxTouches;

		public MobilesInputManager(IJoystickInput joystickInput, ITouchScreenInput touchScreenInput, IControlManager controlManager, IResolutionManager resolutionManager)
		{
			this.joystickInput = joystickInput;
			this.touchScreenInput = touchScreenInput;
			this.controlManager = controlManager;
			this.resolutionManager = resolutionManager;
		}

		public void Initialize()
		{
			const GestureType gestureType = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;
			touchScreenInput.Initialize(gestureType);

			maxTouches = 0;
		}

		public void LoadContent()
		{
			viewPortVector2 = MyGame.Manager.ResolutionManager.ViewPortVector2;
			invertTransformationMatrix = MyGame.Manager.ResolutionManager.InvertTransformationMatrix;

			maxTouches = MyGame.Manager.ConfigManager.PlatformConfigData.MaxTouches;
		}

		public void Update(GameTime gameTime)
		{
			touchScreenInput.Update(gameTime);
		}

		public Vector2[] GetPositions()
		{
			return null;
			//return touchScreenInput.TouchPositionsX;	// TODO delete
		}

		public TouchLocationState[] GetStates()
		{
			return null;
			//return touchScreenInput.TouchStatesX;	// TODO delete
		}

		public Boolean[] GetStates2()
		{
			return null;
		}

		public Boolean Escape()
		{
			return false;
		}

		public Single Horizontal()
		{
			Single horz = 0.0f;

			// Touch.
			TouchCollection touchCollection = touchScreenInput.TouchCollection;
			Int32 count = touchCollection.Count;
			if (0 == count)
			{
				return horz;
			}

			Int32 loops = Math.Min(maxTouches, count);
			for (Byte index = 0; index < loops; index++)
			{
				TouchLocation touchLocation = touchCollection[index];

				TouchLocationState state = touchLocation.State;
				if (!(TouchLocationState.Pressed == state || TouchLocationState.Moved == state))
				{
					continue;
				}

				Vector2 position = GetTouchPosition(touchLocation.Position);
				Single temp = controlManager.CheckJoyPadHorz(position);
				if (Math.Abs(temp) > Single.Epsilon)
				{
					horz = temp;
				}
			}

			//foreach (TouchLocation touch in touchCollection)
			//{
			//    TouchLocationState state = touch.State;
			//    if (TouchLocationState.Pressed == state || TouchLocationState.Moved == state)
			//    {

			//    }
			//}

			return horz;
		}

		public Single Vertical()
		{
			Single vert = 0.0f;

			// Touch.
			TouchCollection touchCollection = touchScreenInput.TouchCollection;
			Int32 count = touchCollection.Count;
			if (0 == count)
			{
				return vert;
			}

			Int32 loops = Math.Min(maxTouches, count);
			for (Byte index = 0; index < loops; index++)
			{
				TouchLocation touchLocation = touchCollection[index];

				TouchLocationState state = touchLocation.State;
				if (!(TouchLocationState.Pressed == state || TouchLocationState.Moved == state))
				{
					continue;
				}

				Vector2 position = GetTouchPosition(touchLocation.Position);
				Single temp = controlManager.CheckJoyPadVert(position);
				if (Math.Abs(temp) > Single.Epsilon)
				{
					vert = temp;
				}
			}

			return vert;
		}

		private Vector2 GetTouchPosition(Vector2 touchPosition)
		{
			// http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering.
			Vector2 deltaPosition = touchPosition - viewPortVector2;
			return Vector2.Transform(deltaPosition, invertTransformationMatrix);
		}
 
	}
}