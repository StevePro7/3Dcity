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

		private UInt16 index;
		private Single delta;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			const Byte commandId = 0;

			eventTimeList = MyGame.Manager.CommandManager.CommandTimeList[commandId];
			eventTypeList = MyGame.Manager.CommandManager.CommandTypeList[commandId];
			eventArgsList = MyGame.Manager.CommandManager.CommandArgsList[commandId];

			index = 0;
			delta = 0.0f;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			MyGame.Manager.EventManager.ClearEvents();

			UpdateTimer(gameTime);

			if (index >= eventTimeList.Count)
			{
				return (Int32)CurrScreen;
			}

			Single eventTime = eventTimeList[index];
			Single timer = (Single)Math.Round(gameTime.ElapsedGameTime.TotalSeconds, 2);
			delta += timer;

			if (delta < eventTime)
			{
				return (Int32)CurrScreen;
			}

			// Process current event.
			delta -= eventTime;
			LoadNewEvents(index);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			//base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			MyGame.Manager.SpriteManager.Draw();

			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private void LoadNewEvents(UInt16 theIndex)
		{
			String eventTypeText = eventTypeList[theIndex];
			String eventArgsText = eventArgsList[theIndex];

			eventTypeData = MyGame.Manager.EventManager.DeserializeTypeText(eventTypeText);
			eventArgsData = MyGame.Manager.EventManager.DeserializeArgsText(eventArgsText);
		}

	}
}
