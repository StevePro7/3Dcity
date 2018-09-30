using System;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class CollisionManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			CollisionManager = new CollisionManager();
			CollisionManager.Initialize(String.Empty);
			base.SetUp();
		}

		[Test]
		public void BoxesCollisionOnceTest()
		{
			// Arrange.
			const Byte radius = 64;
			Vector2 enemysPosition = new Vector2(354, 141);
			Vector2 targetPosition = new Vector2(368, 168);
			Boolean collide = false;

			// Act.
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);

			// Assert.
			Assert.True(collide);
		}

		[Test]
		public void BoxesCollisionManyTest()
		{
			const Byte radius = 64;
			Vector2 enemysPosition = new Vector2(100, 100);
			Vector2 targetPosition = Vector2.Zero;
			Boolean collide = false;

			// Left.
			targetPosition = new Vector2(30, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(36, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);

			// Top.
			targetPosition = new Vector2(120, 32);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(36, 36);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);

			// Right.
			targetPosition = new Vector2(230, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(220, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);

			// Down.
			targetPosition = new Vector2(100, 224);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(120, 200);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);
		}

		//[Test]
		public void DetermineEnemySlotTest()
		{
			Vector2 position = Vector2.Zero;
			SByte index = Constants.INVALID_INDEX;

			position = new Vector2(156, 275);
			index = CollisionManager.DetermineEnemySlot(position);
			Assert.That(index, Is.EqualTo(Constants.INVALID_INDEX));

			position = new Vector2(157, 175);
			index = CollisionManager.DetermineEnemySlot(position);
			Assert.That(index, Is.EqualTo(1));

			//Vector2 position = new Vector2(156 + (4 * 160), 275);
			//Vector2 position = new Vector2(156 + (4 * 160), 277);
			
			//Assert.That(Constants.INVALID_INDEX, Is.EqualTo(index));
			
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
