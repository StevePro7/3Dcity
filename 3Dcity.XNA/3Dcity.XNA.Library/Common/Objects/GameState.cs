using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame.Common.Objects
{
	public class GameState : BaseObject
	{
		private Byte index;

		public void ToggleIndex()
		{
			index = (Byte)(1 - index);
		}

	}
}
