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
		SByte CheckBullets();
		void Update(GameTime gameTime);
		
		//void Fire(Vector2 position);
		void Shoot(Byte bulletIndex, Vector2 position);
		void Draw();

		IList<Bullet> BulletList { get; }
		//Bullet Bullet { get; }
		Boolean CanShoot { get; }
		Boolean IsFiring { get; }

		UInt16 ShootDelay { get; }
		Single ShootTimer { get; }
	}

	public class BulletManager : IBulletManager 
	{
//		public const Byte MAX_BULLET_SHOOT = 10;

		private Byte maxBulletShoot;
		//private const Byte MAX_BULLET = 10;
		//private const Byte MAX_BULLET = 1;
		//private const Byte MAX_FRAMES = 6;
		//private UInt16 frameDelay;
		//private UInt16 shootDelay;
		//private Single shootTimer;

		public void Initialize()
		{
			BulletList = new List<Bullet>(Constants.MAX_BULLET_SHOOT);
			for (Byte index = 0; index < Constants.MAX_BULLET_SHOOT; index++)
			{
				Bullet bullet = new Bullet();
				bullet.SetID(index);
				bullet.Initialize(Constants.MAX_BULLET_FRAME);
				BulletList.Add(bullet);
			}

			maxBulletShoot = Constants.MAX_BULLET_SHOOT;
			//Bullet = new Bullet();
			//Bullet.Initialize(Vector2.Zero);
			//Bullet.Initialize(Constants.MAX_BULLET_FRAME, 750);		// TODO make the bullet delay configurable
			//IsFiring = false;
			//fireDelay = 5000;
			//fireTimer = 0;
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_BULLET_SHOOT; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.LoadContent(MyGame.Manager.ImageManager.BulletRectangles);
			}
		}

		public void Reset(Byte theBulletShoot, UInt16 frameDelay, UInt16 shootDelay)
		{
			maxBulletShoot = theBulletShoot;
			if (maxBulletShoot > Constants.MAX_BULLET_SHOOT)
			{
				maxBulletShoot = Constants.MAX_BULLET_SHOOT;
			}

			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.Reset(frameDelay);
			}

			ShootDelay = shootDelay;
			ShootTimer = 0;
			CanShoot = true;
		}

		public SByte CheckBullets()
		{
			SByte bulletIndex = Constants.INVALID_INDEX;
			if (!CanShoot)
			{
				return bulletIndex;
			}

			for (Byte testerIndex = 0; testerIndex < maxBulletShoot; testerIndex++)
			{
				Bullet bullet = BulletList[testerIndex];
				if (!bullet.IsFiring)
				{
					CanShoot = false;
					bulletIndex = (SByte)testerIndex;
					break;
				}
			}

			return bulletIndex;
		}

		public void Update(GameTime gameTime)
		{
			if (!CanShoot)
			{
				ShootTimer += (Single)gameTime.ElapsedGameTime.Milliseconds;
				if (ShootTimer >= ShootDelay)
				{
					CanShoot = true;
					ShootTimer = 0;
				}
			}
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
				if (bullet.IsFiring)
				{
					bullet.Update(gameTime);
				}
			}
		}

		//public void Fire(Vector2 position)
		//{
		//    //IsFiring = true;
		//    //Bullet.SetPosition(position);
		//}

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
		}

		public IList<Bullet> BulletList { get; private set; }
		//public Bullet Bullet { get; private set; }
		public Boolean CanShoot { get; private set; }
		public Boolean IsFiring { get; private set; }

		public UInt16 ShootDelay { get; private set; }
		public Single ShootTimer { get; private set; }
	}
}
