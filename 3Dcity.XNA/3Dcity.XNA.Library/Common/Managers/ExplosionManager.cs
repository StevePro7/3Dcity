using System;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IExplosionManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();

		Explosion Explosion { get; }
	}

	public class ExplosionManager : IExplosionManager 
	{
		public void Initialize()
		{
			Explosion = new Explosion();
			Explosion.Initialize(new Vector2(400, 320));
			Explosion.Initialize(Constants.MAX_EXPLODE_FRAME, 100);		// TODO make the bullet delay configurable
		}

		public void LoadContent()
		{
			Explosion.LoadContent(MyGame.Manager.ImageManager.ExplodeRectangles[0]);
		}

		public void Update(GameTime gameTime)
		{
			Explosion.Update(gameTime);
		}

		public void Draw()
		{
			Explosion.Draw();
		}

		public Explosion Explosion { get; private set; }

	}
}
