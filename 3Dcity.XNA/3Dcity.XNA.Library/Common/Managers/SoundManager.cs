using System;
using Microsoft.Xna.Framework;
using WindowsGame.Define.Factorys;

namespace WindowsGame.Common.Managers
{
	public interface ISoundManager 
	{
		void Initialize();
	}

	public class SoundManager : ISoundManager 
	{
		private readonly ISoundFactory soundFactory;

		public SoundManager(ISoundFactory soundFactory)
		{
			this.soundFactory = soundFactory;
		}

		public void Initialize()
		{
		}

	}
}
