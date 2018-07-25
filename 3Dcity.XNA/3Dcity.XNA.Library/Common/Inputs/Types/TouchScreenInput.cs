using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Define.Managers;
using WindowsGame.Common;

namespace WindowsGame.Common.Inputs.Types
{
	public interface ITouchScreenInput
	{
		void Initialize();
		void Initialize(Vector2 viewPortVector2, Matrix invertTransformationMatrix);
		void Update(GameTime gameTime);

		Vector2 TouchPosition { get; }
		TouchLocationState TouchState { get; }
		Boolean Tap { get; }
		Boolean Hold { get; }
		Boolean DoubleTap { get; }
		Boolean HorizontalDrag { get; }
		Boolean VerticalDrag { get; }
	}

	public class TouchScreenInput : ITouchScreenInput
	{
		private Vector2 viewPortVector2;
		private Matrix invertTransformationMatrix;

		public void Initialize()
		{
			Initialize(Vector2.Zero, Matrix.Identity);
		}
		public void Initialize(Vector2 viewPortVector2, Matrix invertTransformationMatrix)
		{
			this.viewPortVector2 = viewPortVector2;
			this.invertTransformationMatrix = invertTransformationMatrix;

			TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;
		}

		public void Update(GameTime gameTime)
		{
			var location = GetTouchLocation();
			if (null != location)
			{
				TouchLocation touchLocation = (TouchLocation)location;
				//TouchPosition = touchLocation.Position;
				TouchPosition = GetTouchPosition(touchLocation.Position);
				TouchState = touchLocation.State;
			}
			else
			{
				TouchPosition = Vector2.Zero;
				TouchState = TouchLocationState.Invalid;
			}

			Tap = Hold = DoubleTap = HorizontalDrag = VerticalDrag = false;
			if (!TouchPanel.IsGestureAvailable)
			{
				return;
			}

			GestureSample gesture = TouchPanel.ReadGesture();
			Tap = gesture.GestureType == GestureType.Tap;
			Hold = gesture.GestureType == GestureType.Hold;
			DoubleTap = gesture.GestureType == GestureType.DoubleTap;
			HorizontalDrag = gesture.GestureType == GestureType.HorizontalDrag;
			VerticalDrag = gesture.GestureType == GestureType.VerticalDrag;
		}

		private static TouchLocation? GetTouchLocation()
		{
			TouchCollection touchCollection = TouchPanel.GetState();
			if (touchCollection.Count > 0)
			{
				return touchCollection[0];
			}

			return null;
		}

		private Vector2 GetTouchPosition(Vector2 touchPosition)
		{
			// http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering.
			Vector2 deltaPosition = touchPosition - viewPortVector2;
			return Vector2.Transform(deltaPosition, invertTransformationMatrix);
		}

		public Vector2 TouchPosition { get; private set; }
		public TouchLocationState TouchState { get; private set; }
		public Boolean Tap { get; private set; }
		public Boolean Hold { get; private set; }
		public Boolean DoubleTap { get; private set; }
		public Boolean HorizontalDrag { get; private set; }
		public Boolean VerticalDrag { get; private set; }
	}
}