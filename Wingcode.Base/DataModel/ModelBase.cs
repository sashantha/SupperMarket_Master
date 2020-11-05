using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Wingcode.Base.Api;

namespace Wingcode.Base.DataModel
{
    public abstract class ModelBase<E> : IObjectCloneable<E> where E : class
    {

        public E CloneObject()
        {
            return (E)MemberwiseClone();
        }
    }
}
