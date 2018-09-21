using System;
using WindowsGame.Common.Managers;
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

		[Test]
		public void EnemyCollideTargetTest()
		{
			Vector2 enemysPosition = new Vector2(100, 100);
			Vector2 targetPosition = new Vector2(40, 96);
			Boolean collide = CollisionManager.EnemyCollideTarget(enemysPosition, targetPosition);
			Assert.False(collide);
		}

		//[Test]
		//public void CheckEnemyCollisionExitTest()
		//{
		//    Assert.That(1, Is.EqualTo(1));
		//}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
