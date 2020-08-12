using System;
using System.Collections.Generic;
using System.Text;

namespace Wingcode.Base.Api
{
    public interface IObjectCloneable<E>
    {
        E CloneObject();
    }
}
