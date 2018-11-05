using System;
using System.Collections.Generic;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
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

		[Test]
		public void CalcdEnemyDelaysTest()
		{
			IDictionary<Byte, UInt16> enemyDelays = new Dictionary<Byte, UInt16>(Constants.MAX_ENEMYS_TOTAL);

			const LevelType levelType = LevelType.Hard;
			const Byte levelNo = 13;
			const Byte levelIndex = levelNo - 1;

			MyGame.Manager.LevelManager.Initialize(CONTENT_ROOT);
			MyGame.Manager.LevelManager.LoadLevelConfigData(levelType, levelIndex);
			LevelConfigData levelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			MyGame.Manager.RandomManager.Initialize();

			DelayManager.LoadContent();
			DelayManager.ResetEnemyDelays(enemyDelays, levelConfigData);
			DelayManager.CalcdEnemyDelays(enemyDelays, levelConfigData);

			Console.WriteLine("Level:{0} Num:{1}", levelType, levelNo);
			foreach (UInt16 delay in enemyDelays.Values)
			{
				Console.WriteLine(delay);
			}
		}

		[TearDown]
		public void TearDown()
		{
			DelayManager = null;
		}

	}
}
