using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2048
{
    public class Randomizer : IRandomizer
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        public Randomizer()
        {
            
        }
        public int GenerateValue(int max)
        {
            return random.Next(max+1);
        }
    }
}
