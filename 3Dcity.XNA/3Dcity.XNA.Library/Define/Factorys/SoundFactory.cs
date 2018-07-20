using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MediaPlayerX = Microsoft.Xna.Framework.Media.MediaPlayer;
using System;


namespace WindowsGame.Define.Factorys
{
	public interface ISoundFactory
	{
		void Initialize();
		void Initialize(Boolean thePlayMusic, Boolean thePlaySound);

		void StartMusic(Song song);
		void StartMusic(Song song, Boolean isRepeating);
		void StopMusic();
		void PauseMusic();
		void ResumeMusic();

		void PlaySoundEffect(SoundEffectInstance soundEffect);
	}

	public class SoundFactory : ISoundFactory
	{
		private Boolean playSound;
		private Boolean playMusic;

		public void Initialize()
		{
			Initialize(true, true);
		}

		public void Initialize(Boolean thePlayMusic, Boolean thePlaySound)
		{
			playMusic = thePlayMusic;
			playSound = thePlaySound;
		}

		public void StartMusic(Song song)
		{
			StartMusic(song, true);
		}

		public void StartMusic(Song song, Boolean isRepeating)
		{
			if (!playMusic)
			{
				return;
			}

			if (MediaState.Playing == MediaPlayerX.State)
			{
				return;
			}

			MediaPlayerX.Play(song);
			MediaPlayerX.IsRepeating = isRepeating;
		}

		public void PauseMusic()
		{
			if (!playMusic)
			{
				return;
			}

			if (MediaState.Playing == MediaPlayerX.State)
			{
				MediaPlayerX.Pause();
			}
		}

		public void ResumeMusic()
		{
			if (!playMusic)
			{
				return;
			}

			if (MediaState.Paused == MediaPlayerX.State)
			{
				MediaPlayerX.Resume();
			}
		}

		public void StopMusic()
		{
			if (!playMusic)
			{
				return;
			}

			if (MediaState.Playing == MediaPlayerX.State)
			{
				MediaPlayerX.Stop();
			}
		}

		public void PlaySoundEffect(SoundEffectInstance soundEffect)
		{
			if (!playSound)
			{
				return;
			}

			soundEffect.Play();
		}

	}
}
