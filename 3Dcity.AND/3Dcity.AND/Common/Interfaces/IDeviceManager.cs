using System;

namespace WindowsGame.Common.Interfaces
{
	public interface IDeviceManager
	{
		void Initialize();
		void Abort();

		String BuildVersion { get; }
		Byte MaxPlayers { get; }
	}
}
