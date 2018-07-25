using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Inputs.Types
{
	public interface ITouchScreenInput
	{
		void Initialize();
		void Initialize(Vector2 theViewPortVector2, Matrix theIvertTransformationMatrix);
		void Update(GameTime gameTime);

		Vector2[] TouchPosition { get; }
		TouchLocationState[] TouchState { get; }
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
		private IList<TouchLocation> touchLocationList;
		private const Byte MAX_TOUCHES = 4;

		public void Initialize()
		{
			Initialize(Vector2.Zero, Matrix.Identity);
		}
		public void Initialize(Vector2 theViewPortVector2, Matrix theIvertTransformationMatrix)
		{
			viewPortVector2 = theViewPortVector2;
			invertTransformationMatrix = theIvertTransformationMatrix;

			TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;
			touchLocationList = new List<TouchLocation>(MAX_TOUCHES);
		}

		public void Update(GameTime gameTime)
		{
			// Reset all touch information first.
			for (Byte index = 0; index < MAX_TOUCHES; index++)
			{
				TouchPosition[index] = Vector2.Zero;
				TouchState[index] = TouchLocationState.Invalid;
			}

			// Populate touch information accordingly.
			touchLocationList = GetTouchLocationList();

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

		private IList<TouchLocation> GetTouchLocationList()
		{
			touchLocationList.Clear();
			
			TouchCollection touchCollection = TouchPanel.GetState();
			if (0 == touchCollection.Count)
			{
				return touchLocationList;
			}

			for (Byte index = 0; index < touchCollection.Count; index++)
			{
				TouchLocation touchLocation = touchCollection[index];

				TouchPosition[index] = GetTouchPosition(touchLocation.Position);
				TouchState[index] = touchLocation.State;
			}

			return touchLocationList;
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

		public Vector2[] TouchPosition { get; private set; }
		public TouchLocationState[] TouchState { get; private set; }
		public Boolean Tap { get; private set; }
		public Boolean Hold { get; private set; }
		public Boolean DoubleTap { get; private set; }
		public Boolean HorizontalDrag { get; private set; }
		public Boolean VerticalDrag { get; private set; }
	}
}