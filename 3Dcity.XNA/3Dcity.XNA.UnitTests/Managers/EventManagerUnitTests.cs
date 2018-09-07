using System;
using System.Collections.Generic;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using NUnit.Framework;

namespace WindowsGame.UnitTests.Managers
{
	[TestFixture]
	public class EventManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			EventManager = new EventManager();
			EventManager.Initialize();
			base.SetUp();
		}

		[Test]
		public void SerializeTypeNoneTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That(String.Empty, Is.EqualTo(result));
		}

		[Test]
		public void SerializeTypeOnceTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();
			eventTypeData.Add(EventType.LargeTargetMove);

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That("00", Is.EqualTo(result));
		}

		[Test]
		public void SerializeTypeDataTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();
			eventTypeData.Add(EventType.LargeTargetMove);
			eventTypeData.Add(EventType.SmallTargetMove);

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That("00|01", Is.EqualTo(result));
		}

		

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
