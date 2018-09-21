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
			Assert.IsNotNull(LevelManager.LevelRoman);
			Assert.That(LevelManager.LevelNames.Count, Is.EqualTo(LevelManager.LevelRoman.Count));

			Print(LevelManager.LevelNames);
			Print(LevelManager.LevelRoman);
		}

		private void Print(IList<String> lines)
		{
			foreach (var line in lines)
			{
				Console.WriteLine(line);
			}
		}

		[TearDown]
		public void TearDown()
		{
			LevelManager = null;
		}
	}
}
