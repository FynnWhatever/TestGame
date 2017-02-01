using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    class Player

    {
        public string ID;
        public string buttonInput = "";
        public Texture2D PlayerTexture;
        public Vector2 Position;
        private SpriteFont PlayerFont;
        public bool Active;
        public int Health;
        public int Width
        {
            get { return PlayerTexture.Width; }
        }
        public int Height
        {
            get { return PlayerTexture.Height; }
        }

        public void Initialize(string uniqueID,Texture2D texture, Vector2 position, SpriteFont font)
        {
            PlayerTexture = texture;
            Position = position;
            Active = true;
            Health = 100;
            ID = uniqueID;
            PlayerFont = font;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            string outy = ID + ": " + buttonInput;
            spriteBatch.DrawString(PlayerFont, outy, Position, Color.Blue);
        }
    }


}