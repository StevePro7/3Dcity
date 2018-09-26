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
			base.SetUp();
		}

		//[Test]
		//public void EnemyCollideTargetTest()
		//{
		//    Vector2 enemysPosition = new Vector2(100, 100);
		//    Vector2 targetPosition = new Vector2(40, 96);
		//    Boolean collide = CollisionManager.EnemyCollideTarget(enemysPosition, targetPosition);
		//    Assert.False(collide);
		//}

		[Test]
		public void DetermineEnemySlotTest()
		{
			Vector2 position = Vector2.Zero;
			SByte index = Constants.INVALID_INDEX;

			position = new Vector2(156, 275);
			index = CollisionManager.DetermineEnemySlot(position);
			Assert.That(index, Is.EqualTo(Constants.INVALID_INDEX));

			position = new Vector2(157, 275);
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
