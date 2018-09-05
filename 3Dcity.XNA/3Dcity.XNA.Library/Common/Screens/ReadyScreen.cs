﻿using System;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;
using Microsoft.Xna.Framework;
using WindowsGame.Master;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreen, IScreen 
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			//MyGame.Manager.ExplosionManager.Update(gameTime);
			//MyGame.Manager.BulletManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			base.Draw();

			MyGame.Manager.RenderManager.Draw();

			//MyGame.Manager.ExplosionManager.Draw();
			//MyGame.Manager.BulletManager.Draw();
			//MyGame.Manager.SpriteManager.Draw();

			MyGame.Manager.TextManager.Draw(TextDataList);
			
		}

	}
}
