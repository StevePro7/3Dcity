using WindowsGame.Engine.Interfaces;
using WindowsGame.Engine.IoC;
using WindowsGame.Engine.Managers;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			//IoCContainer.Initialize<IGameManager, GameManager>();

			//IoCContainer.Initialize<IJoystickInput, JoystickInput>();
			//IoCContainer.Initialize<IKeyboardInput, KeyboardInput>();
			//IoCContainer.Initialize<IMouseScreenInput, MouseScreenInput>();
			//IoCContainer.Initialize<ITouchScreenInput, TouchScreenInput>();

//            IoCContainer.Initialize<IFileProxy, RealFileProxy>();
//            IoCContainer.Initialize<IFileManager, FileManager>();

//#if (WINDOWS && MOBILE)
//            IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, WindowsInputFactory>();
//            IoCContainer.Initialize<ILogger, Logger.Implementation.RealLogger>();
//#elif WINDOWS
//            IoCContainer.Initialize<IDeviceFactory, WindowsDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, WindowsInputFactory>();
//            IoCContainer.Initialize<ILogger, Logger.Implementation.RealLogger>();
//#endif
//#if !WINDOWS
//            IoCContainer.Initialize<IDeviceFactory, MobilesDeviceFactory>();
//            IoCContainer.Initialize<IInputFactory, MobilesInputFactory>();
//            IoCContainer.Initialize<ILogger, Library.Implementation.TestLogger>();
//#endif
		}
	}
}