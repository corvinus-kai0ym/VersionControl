using porductionLine_kai0ym.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace porductionLine_kai0ym.Entities
{
    class CarFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
