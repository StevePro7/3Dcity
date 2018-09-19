using System;
using System.Collections.Generic;
using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class LevelManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			LevelManager = MyGame.Manager.LevelManager;
			LevelManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadContentTest()
		{
			LevelManager.LoadContent();

			Assert.IsNotNull(LevelManager.LevelNames);

			foreach (var name in LevelManager.LevelNames)
			{
				Console.WriteLine(name);
			}
		}

		[TearDown]
		public void TearDown()
		{
			LevelManager = null;
		}
	}
}
