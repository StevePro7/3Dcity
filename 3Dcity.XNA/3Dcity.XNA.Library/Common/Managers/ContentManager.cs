using WindowsGame.Define.Factorys;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Managers
{
	public interface IContentManager 
	{
		void Initialize();
		void LoadContent();
	}

	public class ContentManager : IContentManager 
	{
		private readonly IContentFactory contentFactory;

		public ContentManager(IContentFactory contentFactory)
		{
			this.contentFactory = contentFactory;
		}

		public void Initialize()
		{
		}

		public void LoadContent()
		{
			Texture2D graphic = contentFactory.LoadTexture("Emulogic");
		}

	}
}
