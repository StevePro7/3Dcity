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
		private Single timer;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			const Byte commandId = 1;

			eventTimeList = MyGame.Manager.CommandManager.CommandTimeList[commandId];
			eventTypeList = MyGame.Manager.CommandManager.CommandTypeList[commandId];
			eventArgsList = MyGame.Manager.CommandManager.CommandArgsList[commandId];

			index = 0;
			delta = 0.0f;
			timer = 0.0f;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			if (index >= eventTimeList.Count)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);

			MyGame.Manager.EventManager.ClearEvents();

			Single eventTime = eventTimeList[index];
			//Single timer = (Single)Math.Round(gameTime.ElapsedGameTime.TotalSeconds, 2);
			timer = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			delta += timer;
			delta = (Single)Math.Round(delta, 2);

			if (delta >= eventTime)
			{
				// Process current event.
				delta -= eventTime;
				LoadNewEvents(index);

				Byte count = (Byte)(eventTypeData.Count);
				for (Byte delim = 0; delim < count; ++delim)
				{
					EventType eventType = eventTypeData[delim];
					ValueType eventArgs = eventArgsData[delim];

					MyGame.Manager.EventManager.ProcessEvent(eventType, eventArgs);
				}

				index++;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();
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
