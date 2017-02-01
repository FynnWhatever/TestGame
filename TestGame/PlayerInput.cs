using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class PlayerInput
    {
        public string id { get; set; }
        public Game1.PlayerAction action { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
