using System;
using WindowsGame.Master.Objects;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IScoreManager 
	{
		void Initialize();
		void LoadContent();
		void Reset();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class ScoreManager : IScoreManager
	{
		private TextData textData;
		private UInt16 scoreDelay;
		private UInt16 scoreTimer;
		private Boolean scoreFlag;
		private Boolean scoreBlink;

		public void Initialize()
		{
			scoreDelay = 800;
		}

		public void LoadContent()
		{
			Vector2 scorePosition = MyGame.Manager.TextManager.GetTextPosition(4, 1);
			textData = new TextData(scorePosition, "1UP", Color.Yellow);
			scoreBlink = MyGame.Manager.ConfigManager.GlobalConfigData.ScoreBlink;
		}

		public void Reset()
		{
			scoreTimer = 0;
			scoreFlag = true;
		}

		public void Update(GameTime gameTime)
		{
			if (!scoreBlink)
			{
				return;
			}

			scoreTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (scoreTimer >= scoreDelay)
			{
				scoreTimer -= scoreDelay;
				scoreFlag = !scoreFlag;
			}
		}

		public void Draw()
		{
			if (scoreFlag)
			{
				MyGame.Manager.TextManager.Draw(textData);
			}
		}

	}
}
