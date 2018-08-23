using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Managers
{
	[TestFixture]
	public class ScreenManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			ScreenManager = MyGame.Manager.ScreenManager;
		}

		[Test]
		public void LoadContentTest()
		{
			ScreenManager.LoadContent();
		}

		[TearDown]
		public void TearDown()
		{
			ScreenManager = null;
		}

	}
}
