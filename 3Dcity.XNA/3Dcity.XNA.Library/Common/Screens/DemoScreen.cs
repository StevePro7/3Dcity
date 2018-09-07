using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
namespace WindowsGame.Common.Screens
{
	public class DemoScreen : BaseScreen, IScreen
	{
		private IList<Single> eventTimeList;
		private IList<String> eventTypeList;
		private IList<String> eventArgsList;

		private IList<EventType> eventTypeData;
		private IList<ValueType> eventArgsData;

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			//base.Draw();
		}

	}
}
