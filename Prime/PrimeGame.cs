using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Prime
{
    public abstract class PrimeGame : Game
    {
		// The spritebatch that draws tem all
		private SpriteBatch drawer;

		// Monogame Required stuff
		GraphicsDeviceManager graphics;

		// The currently active scene
		private Scene activeScene;
		public Scene ActiveScene
		{
			get
			{
				return activeScene;
			}
			set
			{
				activeScene = value;
				activeScene.Initialize();
				activeScene.Game = this;
				return;
			}
		}

		private ViewportAdapter viewPortAdapter;
		public ViewportAdapter ViewportAdapter
		{
			get
			{
				return viewPortAdapter;
			}
		}

        public PrimeGame(Scene s)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			activeScene = s;
			activeScene.Game = this;
			activeScene.Content = new ContentManager(Content.ServiceProvider);
			activeScene.Content.RootDirectory = "Content";

			base.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

			this.viewPortAdapter = new BoxingViewportAdapter(this.Window, this.graphics, 1280, 720);

			// Initialize the first scene
			activeScene.Initialize();
        }

        protected override void LoadContent()
        {
            drawer = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			Time.GameTime = gameTime;
			Input.Update();

			activeScene.Update();
		}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(activeScene.ClearColor);

			Time.GameTime = gameTime;

			drawer.Begin(transformMatrix: activeScene.Cam.GetViewMatrix(), samplerState: SamplerState.PointClamp);

			activeScene.Draw(drawer);

			drawer.End();

            base.Draw(gameTime);
        }
    }
}
