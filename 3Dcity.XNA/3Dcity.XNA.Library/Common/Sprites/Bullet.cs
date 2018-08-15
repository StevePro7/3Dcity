﻿using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class Bullet : BaseSprite
	{
		public override void Initialize(Vector2 position)
		{
			base.Initialize(position);
			BulletType = BulletType.Idle;
		}

		public void SetPosition(Vector2 position)
		{
			Position = position;
		}

		public BulletType BulletType { get; private set; }
	}

}