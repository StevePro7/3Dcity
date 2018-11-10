using System;
using System.IO;
using WindowsGame.Define.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Define.Implementation
{
	public class ProdFileProxy : IFileProxy
	{
		public Stream GetStream(String path)
		{
			return TitleContainer.OpenStream(path);
		}
	}
}