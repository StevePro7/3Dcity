using System;
using System.Collections.Generic;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IBulletManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Fire(Vector2 position);
		void Draw();

		IList<Bullet> BulletList { get; }
		Bullet Bullet { get; }
		Boolean IsFiring { get; }
	}

	public class BulletManager : IBulletManager 
	{
		private const Byte MAX_BULLET = 10;
		private const Byte MAX_FRAMES = 6;
		private Single fireDelay;
		private Single fireTimer;

		public void Initialize()
		{
			BulletList = new List<Bullet>(MAX_BULLET);
			for (Byte index = 0; index < MAX_BULLET; index++)
			{
				Bullet bullet = new Bullet();
				BulletList.Add(bullet);
			}

			Bullet = new Bullet();
			Bullet.Initialize(Vector2.Zero);
			Bullet.Initialize(MAX_FRAMES, 750);		// TODO make the bullet delay configurable
			IsFiring = false;
			fireDelay = 5000;
			fireTimer = 0;
		}

		public void LoadContent()
		{
			Bullet.LoadContent(MyGame.Manager.ImageManager.BulletRectangles);
		}

		public void Update(GameTime gameTime)
		{
			if (IsFiring)
			{
				fireTimer += (Single)gameTime.ElapsedGameTime.TotalSeconds;
				if (fireTimer >= fireDelay)
				{
					fireTimer -= fireDelay;
					IsFiring = false;
				}
			}

			Bullet.Update(gameTime);
		}

		public void Fire(Vector2 position)
		{
			IsFiring = true;
			Bullet.SetPosition(position);
		}

		public void Draw()
		{
			if (IsFiring)
			{
				Bullet.Draw();
			}
		}

		public IList<Bullet> BulletList { get; private set; }
		public Bullet Bullet { get; private set; }
		public Boolean IsFiring { get; private set; }
	}
}
