using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Inputs.Types;

namespace WindowsGame.Common.Inputs
{
	public class MobilesInputFactory : IInputFactory
	{
		private readonly ITouchScreenInput touchScreenInput;

		public MobilesInputFactory(ITouchScreenInput touchScreenInput)
		{
			this.touchScreenInput = touchScreenInput;
		}

		public void Initialize()
		{
			Vector2 viewPortVector2 = MyGame.Manager.ResolutionManager.ViewPortVector2;
			Matrix invertTransformationMatrix = MyGame.Manager.ResolutionManager.InvertTransformationMatrix;

			touchScreenInput.Initialize(viewPortVector2, invertTransformationMatrix);
		}

		public void Update(GameTime gameTime)
		{
		}

		public Boolean Escape()
		{
			return false;
		}


		#region IInputFactory Members


		public float Horizontal()
		{
			throw new NotImplementedException();
		}

		public float Vertical()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}