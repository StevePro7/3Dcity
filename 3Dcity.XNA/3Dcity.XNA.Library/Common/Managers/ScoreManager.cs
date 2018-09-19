using System;
using WindowsGame.Common.Static;
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

		// Properties.
		Byte MissesTotal { get; }
		Byte ScoreAvoid { get; }
		Byte ScoreKills { get; }
	}

	public class ScoreManager : IScoreManager
	{
		private TextData[] missTextData;
		private TextData textData;
		private UInt16 scoreDelay;
		private UInt16 scoreTimer;
		private Boolean scoreFlag;
		private Boolean scoreBlink;

		public void Initialize()
		{
			// TODO config
			scoreDelay = 800;
		}

		public void LoadContent()
		{
			missTextData = GetMissTextDataList();
			Vector2 scorePosition = MyGame.Manager.TextManager.GetTextPosition(4, 1);
			textData = new TextData(scorePosition, Globalize.PLAYER_FLASH, Color.Yellow);
			scoreBlink = MyGame.Manager.ConfigManager.GlobalConfigData.ScoreBlink;
		}

		public void Reset()
		{
			MissesTotal = 4;
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
			for (Byte index = 0; index < MissesTotal; index++)
			{
				MyGame.Manager.TextManager.Draw(missTextData[index]);
			}

			if (scoreFlag)
			{
				MyGame.Manager.TextManager.Draw(textData);
			}
		}

		private TextData[] GetMissTextDataList()
		{
			TextData[] data = new TextData[Constants.MAX_MISSES];
			for (Byte index = 0; index < Constants.MAX_MISSES; index++)
			{
				data[index] = GetMissTextData((Byte)(11 - index));
			}

			return data;
		}

		private static TextData GetMissTextData(Byte y)
		{
			Vector2 position = MyGame.Manager.TextManager.GetTextPosition(39, (SByte)y);
			return new TextData(position, Globalize.MISS_TEXT);
		}

		public Byte MissesTotal { get; private set; }
		public Byte ScoreAvoid { get; private set; }
		public Byte ScoreKills { get; private set; }
	}
}
