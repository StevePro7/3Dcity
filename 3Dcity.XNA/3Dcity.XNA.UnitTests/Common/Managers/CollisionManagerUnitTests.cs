using WindowsGame.Common.Managers;
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
		public void CheckEnemyCollisionExitTest()
		{
			Assert.That(1, Is.EqualTo(1));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
