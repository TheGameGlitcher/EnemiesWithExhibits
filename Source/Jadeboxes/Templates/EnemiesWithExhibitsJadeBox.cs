using LBoL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnemiesWithExhibits.Source.Jadeboxes.Templates
{
    public class EnemiesWithExhibitsJadeBox : JadeBox
    {
        public virtual int Value4 { get; protected set; } = 0;

        public virtual int Value5 { get; protected set; } = 0;
    }
}
