using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{

    interface IBasicPlayer
    {
        string ID { get; set; }
        Vector2 Position { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }

    class Player

    {
        public string ID;
		public Input Input;

        public Player(string uniqueID)
        {
            ID = uniqueID;
			Input = new Input();
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }


}