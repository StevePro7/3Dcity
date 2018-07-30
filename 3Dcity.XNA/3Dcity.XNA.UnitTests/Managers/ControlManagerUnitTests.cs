using System;
using WindowsGame.Common.Managers;
using NUnit.Framework;

namespace WindowsGame.UnitTests.Managers
{
	[TestFixture]
	public class ControlManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			ControlManager = new ControlManager();
			base.SetUp();
		}

		[Test]
		public void MyConvertTest01()
		{
			const int posX = 100; const int posY = 100;
			const int rectX = 0; const int rectY = 280;
			const int rectW = 200; const int rectH = 200;

			Single value = ControlManager.MyConvert(posX, posY, rectX, rectY, rectW, rectH);

			Assert.That(0.0f, Is.EqualTo(value));
		}

		[Test]
		public void MyConvertTest02()
		{
			const int posX = 0; const int posY = 380;
			const int rectX = 0; const int rectY = 280;
			const int rectW = 200; const int rectH = 200;

			Single value = ControlManager.MyConvert(posX, posY, rectX, rectY, rectW, rectH);

			Assert.That(-1.0f, Is.EqualTo(value));
		}

		[Test]
		public void MyConvertTest03()
		{
			const int posX = 199; const int posY = 380;
			const int rectX = 0; const int rectY = 280;
			const int rectW = 200; const int rectH = 200;

			Single value = ControlManager.MyConvert(posX, posY, rectX, rectY, rectW, rectH);

			Assert.That(1.0f, Is.EqualTo(value));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
