using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.common.Enums
{
    public enum PartialStateEnum
    {
        [Description("standart")]
        standart = 0,
        [Description("none")]
        none = 1,
        [Description("custom")]
        custom = 2
    }
}
