using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    public class PlayerInput
    {
        public string id { get; set; }
		public bool btnA { get; set; }
		public bool btnB { get; set; }
		public bool btnStick { get; set; }
		public float stickX { get; set; }
        public float stickY { get; set; }
    }
}
