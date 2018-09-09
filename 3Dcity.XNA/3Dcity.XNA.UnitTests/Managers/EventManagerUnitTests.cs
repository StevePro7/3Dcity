using System;
using System.Collections.Generic;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
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
		public void SerializeTypeDataNoneTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That(String.Empty, Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeDataOnceTest()
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
		public void SerializeTypeDataTwiceTest()
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

		[Test]
		public void SerializeTypeArgsNoneTest()
		{
			// Arrange.
			IList<ValueType> eventTypeArgs = new List<ValueType>();

			// Act.
			String result = EventManager.SerializeArgsData(eventTypeArgs);

			// Assert.
			Assert.That(String.Empty, Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeArgsOnceTest()
		{
			// Arrange.
			IList<ValueType> eventTypeArgs = new List<ValueType>();
			eventTypeArgs.Add(new Vector2(372.2f, 250));

			// Act.
			String result = EventManager.SerializeArgsData(eventTypeArgs);

			// Assert.
			Assert.That("372.2:250", Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeArgsTwiceTest()
		{
			// Arrange.
			IList<ValueType> eventTypeArgs = new List<ValueType>();
			eventTypeArgs.Add(new Vector2(372.2f, 250));
			eventTypeArgs.Add(new Vector2(87.5f, 360));

			// Act.
			String result = EventManager.SerializeArgsData(eventTypeArgs);

			// Assert.
			Assert.That("372.2:250|87.5:360", Is.EqualTo(result));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
