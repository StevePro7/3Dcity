using System;
using WindowsGame.Common.Static;
using WindowsGame.Master.Factorys;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame.Common.Managers
{
	public interface ISoundManager 
	{
		void Initialize();
		void Initialize(Boolean playAudio);

		void GamePause(Boolean gamePause);
		void GameQuiet(Boolean gameQuiet);

		void PlayTitleMusic();
		void PlayMusic(Song song);
		void PauseMusic();
		void ResumeMusic();
		void StopMusic();

		void PlaySoundEffect();

		//void SetPlayAudio(Boolean playAudio);
		Boolean PlayAudio { get; }
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
			Initialize(MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio);
		}

		public void Initialize(Boolean playAudio)
		{
			PlayAudio = playAudio;
		}

		public void GamePause(Boolean gamePause)
		{
			if (gamePause)
			{
				PauseAllAudio();
			}
			else
			{
				Boolean gameQuiet = MyGame.Manager.StateManager.GameQuiet;
				if (!gameQuiet)
				{
					ResumeAllAudio();
				}
			}
		}

		public void GameQuiet(Boolean gameQuiet)
		{
			if (gameQuiet)
			{
				PauseAllAudio();
			}
			else
			{
				ResumeAllAudio();
			}

			SetPlayAudio(!gameQuiet);
		}

		//public void SetPlayAudio(Boolean playAudio)
		//{
		//    PlayAudio = playAudio;
		//    SetVolume();
		//}

		public void PlaySoundEffect()
		{
			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			if (!PlayAudio)
			{
				return;
			}
			SoundEffectType key = SoundEffectType.Funny;
			//SoundEffectType key = SoundEffectType.Cheat;
			SoundEffectInstance value = Assets.SoundEffectDictionary[key];
			soundFactory.PlaySoundEffect(value);
		}

		public void PlayTitleMusic()
		{
			PlayMusic(Assets.GameMusicSong);
		}

		public void PlayMusic(Song song)
		{
			if (null == song)
			{
				return;
			}

			SetVolume();
			soundFactory.PlayMusic(Assets.GameMusicSong);
		}

		public void PauseMusic()
		{
			soundFactory.PauseMusic();
		}

		public void ResumeMusic()
		{
			SetVolume();
			soundFactory.ResumeMusic();
		}

		public void StopMusic()
		{
			soundFactory.StopMusic();
		}


		private void SetPlayAudio(Boolean playAudio)
		{
			PlayAudio = playAudio;
			SetVolume();
		}

		private void PauseAllAudio()
		{
			PauseMusic();

			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.PauseSoundEffect(soundEffect);
			}
		}

		private void ResumeAllAudio()
		{
			ResumeMusic();

			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.ResumeSoundEffect(soundEffect);
			}
		}

		private void SetVolume()
		{
			if (PlayAudio)
			{
				soundFactory.SetMaxVolume();
			}
			else
			{
				soundFactory.SetMinVolume();
			}
		}

		public Boolean PlayAudio { get; private set; }
	}
}
