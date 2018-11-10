using System;
using System.IO;

namespace WindowsGame.Define.Interfaces
{
	public interface IFileProxy
	{
		Stream GetStream(String path);
	}
}
