using System;

namespace WindowsGame.Common.Interfaces
{
	public interface IDeviceManager
	{
		void Initialize();

		String BuildVersion { get; }
	}
}
