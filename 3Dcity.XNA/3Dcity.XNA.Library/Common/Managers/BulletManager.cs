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
		void Reset(Byte theBulletShoot, UInt16 frameDelay, UInt16 shootDelay);
		void Update(GameTime gameTime);
		
		void Fire(Vector2 position);
		void Shoot(Byte bulletIndex, Vector2 position);
		void Draw();

		SByte CheckBullets();

		IList<Bullet> BulletList { get; }
		//Bullet Bullet { get; }
		Boolean IsFiring { get; }
	}

	public class BulletManager : IBulletManager 
	{
		public const Byte MAX_BULLET_SHOOT = 10;

		private Byte maxBulletShoot;
		//private const Byte MAX_BULLET = 10;
		//private const Byte MAX_BULLET = 1;
		//private const Byte MAX_FRAMES = 6;
		//private UInt16 frameDelay;
		//private UInt16 shootDelay;
		//private Single shootTimer;

		public void Initialize()
		{
			BulletList = new List<Bullet>(MAX_BULLET_SHOOT);
			for (Byte index = 0; index < MAX_BULLET_SHOOT; index++)
			{
				Bullet bullet = new Bullet();
				bullet.SetID(index);
				bullet.Initialize(Constants.MAX_BULLET_FRAME);
				BulletList.Add(bullet);
			}

			maxBulletShoot = MAX_BULLET_SHOOT;
			//Bullet = new Bullet();
			//Bullet.Initialize(Vector2.Zero);
			//Bullet.Initialize(Constants.MAX_BULLET_FRAME, 750);		// TODO make the bullet delay configurable
			//IsFiring = false;
			//fireDelay = 5000;
			//fireTimer = 0;
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < MAX_BULLET_SHOOT; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.LoadContent(MyGame.Manager.ImageManager.BulletRectangles);
			}
		}

		public void Reset(Byte theBulletShoot, UInt16 frameDelay, UInt16 shootDelay)
		{
			maxBulletShoot = theBulletShoot;
			if (maxBulletShoot > MAX_BULLET_SHOOT)
			{
				maxBulletShoot = MAX_BULLET_SHOOT;
			}

			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.Reset(frameDelay);
			}
		}

		public void Update(GameTime gameTime)
		{
			//if (IsFiring)
			//{
			//    fireTimer += (Single)gameTime.ElapsedGameTime.TotalSeconds;
			//    if (fireTimer >= fireDelay)
			//    {
			//        fireTimer -= fireDelay;
			//        IsFiring = false;
			//    }
			//}

			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.Update(gameTime);
			}
		}

		

		public void Fire(Vector2 position)
		{
			IsFiring = true;
			//Bullet.SetPosition(position);
		}

		public void Shoot(Byte bulletIndex, Vector2 position)
		{
			Bullet bullet = BulletList[bulletIndex];
			bullet.Shoot(position);
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				if (bullet.IsFiring)
				{
					bullet.Draw();
				}
			}

			if (IsFiring)
			{
				//Bullet.Draw();
			}
		}

		public SByte CheckBullets()
		{
			SByte bulletIndex = Constants.INVALID_INDEX;
			for (Byte testerIndex = 0; testerIndex < maxBulletShoot; testerIndex++)
			{
				Bullet bullet = BulletList[testerIndex];
				if (!bullet.IsFiring)
				{
					bulletIndex = (SByte)testerIndex;
					break;
				}
			}

			return bulletIndex;
		}

		public IList<Bullet> BulletList { get; private set; }
		//public Bullet Bullet { get; private set; }
		public Boolean IsFiring { get; private set; }

		public UInt16 CheckDelay { get; private set; }
		public Single CheckTimer { get; private set; }
	}
}
