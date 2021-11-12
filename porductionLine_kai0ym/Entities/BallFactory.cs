using porductionLine_kai0ym.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace porductionLine_kai0ym.Entities
{
    class BallFactory:IToyFactory
    {
        public Abstractions.Toy CreateNew()
        {
            return new Toy();
        }

        
    }
}
