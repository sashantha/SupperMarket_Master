using PropertyChanged;
using System.ComponentModel;
using Wingcode.Base.Api;

namespace Wingcode.Base.DataModel
{

    [AddINotifyPropertyChangedInterface]
    public abstract class ModelBase<E> : INotifyPropertyChanged, IObjectCloneable<E> where E : class
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public E CloneObject()
        {
            return (E)MemberwiseClone();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
