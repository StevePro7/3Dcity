using System;
using System.Collections.Generic;
using WindowsGame.Common.Static;
using WindowsGame.Define.Objects;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreen
	{
		protected UInt16 Timer { get; set; }
		protected IList<TextData> TextDataList { get; private set; }

		public virtual void Initialize()
		{
		}

		public virtual void LoadContent()
		{
			Timer = 0;
		}

		public void UpdateTimer(GameTime gameTime)
		{
			Timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
		}

		public virtual void Draw()
		{
		}

		protected void LoadTextData()
		{
			LoadTextData(GetType().Name);
		}
		protected void LoadTextData(Byte suffix)
		{
			String name = String.Format("{0}{1}", GetType().Name, suffix);
			LoadTextData(name);
		}

		private void LoadTextData(String screen)
		{
			TextDataList = MyGame.Manager.TextManager.LoadTextData(screen);
		}

	}
}