using System;

namespace WindowsGame.Common.Interfaces
{
	public interface IDeviceManager
	{
		void Initialize();
		void DrawTitle();
		void DrawTitle(String title);
		void SetMotors(Single leftMotor, Single rightMotor);
		void ResetMotors();
		void Abort();

		String BuildVersion { get; }
		Byte MaxPlayers { get; }
	}
}
