using System;

namespace WindowsGame.Common.Interfaces
{
	public interface IDeviceManager
	{
		void Initialize();
		void SetMotors(Single leftMotor, Single rightMotor);
		void ResetMotors();
		void Abort();

		String BuildVersion { get; }
		Byte MaxPlayers { get; }
	}
}
