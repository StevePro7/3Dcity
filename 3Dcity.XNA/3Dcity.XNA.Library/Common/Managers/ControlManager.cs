using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IControlManager 
	{
		void Initialize();
		Boolean Test(int x, int y);
	}

	public class ControlManager : IControlManager 
	{
		public void Initialize()
		{
		}

		public Boolean Test(int x, int y)
		{
			return x == 10;
		}

	}
}
