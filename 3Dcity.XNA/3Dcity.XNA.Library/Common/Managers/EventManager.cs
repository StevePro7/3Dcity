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
		void AddLargeTargetMoveEvent(Vector2 position);
		void AddSmallTargetMoveEvent(Vector2 position);
		void Update(GameTime gameTime);
	}

	public class EventManager : IEventManager
	{
		private IList<EventType> eventTypeData;
		private IList<ValueType> eventArgsData;

		private Single delta;
		private Single timer;

		public void Initialize()
		{
			eventTypeData = new List<EventType>();
			eventArgsData = new List<ValueType>();

			delta = 0.0f;
			timer = 0.0f;
		}

		public void ClearEvents()
		{
		}

		public void AddLargeTargetMoveEvent(Vector2 position)
		{
			AddEvent(EventType.LargeTargetMove, position);
		}
		public void AddSmallTargetMoveEvent(Vector2 position)
		{
			AddEvent(EventType.SmallTargetMove, position);
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

		private void AddEvent(EventType type, ValueType args)
		{
			eventTypeData.Add(type);
			eventArgsData.Add(args);
		}

	}
}
