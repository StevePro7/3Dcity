using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

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

			Print(LevelManager.LevelNames);
		}
		private static void Print(IEnumerable<string> lines)
		{
			foreach (var line in lines)
			{
				Console.WriteLine(line);
			}
		}

		[Test]
		public void LoadLevelConfigDataTest()
		{
			const LevelType levelType = LevelType.Easy;
			const Byte levelIndex = 0;

			LevelManager.LoadLevelConfigData(levelType, levelIndex);

			Assert.That(LevelManager.LevelConfigData, Is.Not.Null);

			// Ensure that enemy frame delay proportions work.
			LevelConfigData data = LevelManager.LevelConfigData;
			Byte sum = (Byte) (data.EnemySpeedNone + data.EnemySpeedWave + data.EnemySpeedFast);
			Assert.That(100, Is.EqualTo(sum));

			PrintData(LevelManager.LevelConfigData);
		}
		private static void PrintData(LevelConfigData data)
		{
			var fields = data.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				object obj = field.GetValue(data);
				Console.WriteLine(field.Name + "\t\t" + obj);
			}
		}

		[TearDown]
		public void TearDown()
		{
			LevelManager = null;
		}
	}
}
