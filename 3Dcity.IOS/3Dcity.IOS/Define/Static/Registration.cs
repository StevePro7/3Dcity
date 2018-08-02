using WindowsGame.Common.Inputs.Types;
using WindowsGame.Define.Factorys;
using WindowsGame.Define.Implementation;
using WindowsGame.Define.Inputs;
using WindowsGame.Define.Interfaces;
using WindowsGame.Define.IoC;
using WindowsGame.Define.Managers;
//using IJoystickInput = WindowsGame.Define.Inputs.IJoystickInput;
//using JoystickInput = WindowsGame.Define.Inputs.JoystickInput;
using ITouchScreenInput = WindowsGame.Define.Inputs.ITouchScreenInput;
using TouchScreenInput = WindowsGame.Define.Inputs.TouchScreenInput;

namespace WindowsGame.Define.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Factorys.
			IoCContainer.Initialize<IContentFactory, ContentFactory>();
			IoCContainer.Initialize<ISoundFactory, SoundFactory>();
			IoCContainer.Initialize<IStorageFactory, StorageFactory>();

			// Inputs.
			//IoCContainer.Initialize<IJoystickInput, JoystickInput>();		// TODO move to engine!
			IoCContainer.Initialize<IKeyboardInput, KeyboardInput>();
			IoCContainer.Initialize<IMouseScreenInput, MouseScreenInput>();
			IoCContainer.Initialize<ITouchScreenInput, TouchScreenInput>();

			// Managers.
			IoCContainer.Initialize<IRandomManager, RandomManager>();
			IoCContainer.Initialize<IResolutionManager, ResolutionManager>();

			IoCContainer.Initialize<IFileProxy, ProdFileProxy>();
			IoCContainer.Initialize<IFileManager, FileManager>();
		}

	}
}
