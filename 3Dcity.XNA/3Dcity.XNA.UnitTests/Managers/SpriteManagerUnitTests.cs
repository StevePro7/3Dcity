using WindowsGame.Common.Managers;
using NUnit.Framework;
namespace WindowsGame.UnitTests.Managers
{
	[TestFixture]
	public class SpriteManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			ControlManager = new ControlManager();
			base.SetUp();
		}

		[Test]
		public void MyTest()
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
