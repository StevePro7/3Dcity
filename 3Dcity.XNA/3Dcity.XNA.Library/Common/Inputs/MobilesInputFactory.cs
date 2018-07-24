using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Inputs
{
	public class MobilesInputFactory : IInputFactory
	{
		public MobilesInputFactory()
		{
		}

		public void Initialize()
		{
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