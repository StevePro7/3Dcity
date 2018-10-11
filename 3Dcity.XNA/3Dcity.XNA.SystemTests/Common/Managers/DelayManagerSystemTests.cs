using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class DelayManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			DelayManager = MyGame.Manager.DelayManager;
			DelayManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadContentTest()
		{
			DelayManager.LoadContent();
			Assert.That(DelayManager.DelayWaves, Is.Not.Null);
			Assert.That(DelayManager.DelayWaves.Count, Is.EqualTo(360));
		}

		[TearDown]
		public void TearDown()
		{
			DelayManager = null;
		}

	}
}
