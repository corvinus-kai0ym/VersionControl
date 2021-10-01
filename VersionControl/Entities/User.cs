using System;
using System.Collections.Generic;
using System.Text;

namespace VersionControl.Entities
{
    class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
       
        public string FullName { get; set; }
       

    
    }
}
