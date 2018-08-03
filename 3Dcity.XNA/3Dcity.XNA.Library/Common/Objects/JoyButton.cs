using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame.Common.Objects
{
	public class JoyButton : BaseObject
	{
		public override void Draw()
		{
			base.Draw(Index);
		}
	}
}
