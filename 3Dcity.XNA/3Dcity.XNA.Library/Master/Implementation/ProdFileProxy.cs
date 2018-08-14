using System;
using System.IO;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Master.Implementation
{
	public class ProdFileProxy : IFileProxy
	{
		public Stream GetStream(String path)
		{
			return TitleContainer.OpenStream(path);
		}
	}
}