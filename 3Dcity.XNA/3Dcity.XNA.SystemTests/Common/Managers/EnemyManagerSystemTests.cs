using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class EnemyManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			EnemyManager = MyGame.Manager.EnemyManager;
			EnemyManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadGlobalConfigDataTest()
		{
			EnemyManager.LoadEnemyWaves();
			Assert.That(EnemyManager.EnemyWaves, Is.Not.Null);
			Assert.That(EnemyManager.EnemyWaves.Count, Is.EqualTo(360));
		}

		[TearDown]
		public void TearDown()
		{
			EnemyManager = null;
		}

	}
}
