using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IEventManager
	{
		void Initialize();
		void ClearEvents();
		void ProcessEvents(GameTime gameTime);
		void ProcessEvent(EventType eventType, ValueType eventArgs);

		void AddLargeTargetMoveEvent(Vector2 position);
		void AddSmallTargetMoveEvent(Vector2 position);

		void Update(GameTime gameTime);
	}

	public class EventManager : IEventManager
	{
		private IList<Single> eventTimeList;
		private IList<String> eventTypeList;
		private IList<String> eventArgsList;

		private IList<EventType> eventTypeData;
		private IList<ValueType> eventArgsData;

		private Char[] delim;
		private Single delta;
		private Single timer;

		public void Initialize()
		{
			eventTypeData = new List<EventType>();
			eventArgsData = new List<ValueType>();

			delim = new[] { '|' };
			delta = 0.0f;
			timer = 0.0f;
		}

		public void ClearEvents()
		{
			eventTypeData.Clear();
			eventArgsData.Clear();
		}

		public void ProcessEvents(GameTime gameTime)
		{
			delta += (Single) gameTime.ElapsedGameTime.TotalSeconds;
			if (0 == eventTypeData.Count)
			{
				return;
			}

			Byte count = (Byte)(eventTypeData.Count);
			for (Byte index = 0; index < count; ++index)
			{
				EventType eventType = eventTypeData[index];
				ValueType eventArgs = eventArgsData[index];

				ProcessEvent(eventType, eventArgs);
			}

			delta = 0.0f;
		}

		public void ProcessEvent(EventType eventType, ValueType eventArgs)
		{
			switch (eventType)
			{
				case EventType.LargeTargetMove:
					LargeTargetMove(eventArgs);
					break;

				case EventType.SmallTargetMove:
					SmallTargetMove(eventArgs);
					break;
			}
		}

		public void AddLargeTargetMoveEvent(Vector2 position)
		{
			AddEvent(EventType.LargeTargetMove, position);
		}
		public void AddSmallTargetMoveEvent(Vector2 position)
		{
			AddEvent(EventType.SmallTargetMove, position);
		}

		private void AddEvent(EventType type, ValueType args)
		{
			eventTypeData.Add(type);
			eventArgsData.Add(args);
		}

		public void Update(GameTime gameTime)
		{
			delta = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			timer += delta;
			timer = (Single)Math.Round(timer, 2);
			//if (0 == eventTypeData.Count)
			//{
			//    return;
			//}

			MyGame.Manager.Logger.Info(timer.ToString());
		}

		private static void LargeTargetMove(ValueType eventArgs)
		{
			Vector2 position = (Vector2)eventArgs;
			MyGame.Manager.SpriteManager.SetPosition(SpriteType.LargeTarget, position);
		}

		private static void SmallTargetMove(ValueType eventArgs)
		{
			Vector2 position = (Vector2)eventArgs;
			MyGame.Manager.SpriteManager.SetPosition(SpriteType.SmallTarget, position);
		}

	}
}
