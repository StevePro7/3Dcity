using System;
using System.IO;

namespace WindowsGame.Engine.Interfaces
{
	public interface IFileProxy
	{
		Stream GetStream(String path);
	}
}
