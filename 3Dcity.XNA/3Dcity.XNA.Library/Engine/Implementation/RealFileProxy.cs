using System;
using System.IO;
using WindowsGame.Engine.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Engine.Implementation
{
	public class RealFileProxy : IFileProxy
	{
		public Stream GetStream(String path)
		{
			return TitleContainer.OpenStream(path);
		}
	}
}