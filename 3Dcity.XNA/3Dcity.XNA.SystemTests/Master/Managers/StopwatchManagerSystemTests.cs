﻿using System;
using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Master.Managers
{
	[TestFixture]
	public class StopwatchManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			StopwatchManager = MyGame.Manager.StopwatchManager;
			StopwatchManager.Initialize();
		}

		[Test]
		public void StopwatchTest()
		{
			StopwatchManager.Start();
			System.Threading.Thread.Sleep(2000);
			StopwatchManager.Stop();
			Int64 time = StopwatchManager.ElapsedMilliseconds;
			Assert.That((Int64)2000, Is.EqualTo(time));
		}

		[TearDown]
		public void TearDown()
		{
			StopwatchManager = null;
		}

	}
}
