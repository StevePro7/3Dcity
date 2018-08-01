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
		public void CheckPosInRectTest01()
		{
			const int posX = 100; const int posY = 300;
			const int collX = 0; const int collY = 280;
			const int collW = 200; const int collH = 200;

			Boolean posInRect = ControlManager.CheckPosInRect(posX, posY, collX, collY, collW, collH);

			Assert.That(true, Is.EqualTo(posInRect));
		}

		[Test]
		public void CheckPosInRectTest02()
		{
			const int posX = 300; const int posY = 300;
			const int collX = 0; const int collY = 280;
			const int collW = 200; const int collH = 200;

			Boolean posInRect = ControlManager.CheckPosInRect(posX, posY, collX, collY, collW, collH);

			Assert.That(false, Is.EqualTo(posInRect));
		}

		//[Test]
		//public void ClampPosInRectTest()
		//{
		//    const int posX = 250; const int posY = 200;
		//    const int boundX = 0; const int boundY = 280;
		//    const int boundW = 200; const int boundH = 200;

		//    int newX;
		//    int newY;
		//    ControlManager.ClampPosInRect(posX, posY, boundsX, boundsY, boundsW, boundsH, out newX, newY);

		//    Assert.That(200, Is.EqualTo(newX));
		//    Assert.That(280, Is.EqualTo(newY));
		//}

		[Test]
		public void MyConvertTest01()
		{
			const int posX = 100; const int posY = 100;
			const int collX = 0; const int collY = 280;
			const int collW = 200; const int collH = 200;

			Single value = ControlManager.MyConvert(posX, posY, collX, collY, collW, collH);

			Assert.That(0.0f, Is.EqualTo(value));
		}

		[Test]
		public void MyConvertTest02()
		{
			const int posX = 0; const int posY = 380;
			const int collX = 0; const int collY = 280;
			const int collW = 200; const int collH = 200;

			Single value = ControlManager.MyConvert(posX, posY, collX, collY, collW, collH);

			Assert.That(-1.0f, Is.EqualTo(value));
		}

		[Test]
		public void MyConvertTest03()
		{
			const int posX = 200; const int posY = 380;
			const int collX = 0; const int collY = 280;
			const int collW = 200; const int collH = 200;

			Single value = ControlManager.MyConvert(posX, posY, collX, collY, collW, collH);

			Assert.That(1.0f, Is.EqualTo(value));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
