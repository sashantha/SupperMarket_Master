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
    public abstract class BaseModel<E> :/* BindableBase, INotifyDataErrorInfo,*/ IObjectCloneable<E> where E : class
    {

        //private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        //public bool HasErrors => (_errors.Count > 0);

        //public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public E CloneObject()
        {
            return (E)MemberwiseClone();
        }

        //public IEnumerable GetErrors(string propertyName)
        //{
        //    if (_errors.ContainsKey(propertyName))
        //        return _errors[propertyName];
        //    else
        //        return null;
        //}

        //protected override bool SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        //{
        //    bool r = base.SetProperty<T>(ref member, val, propertyName);
        //    //ValidateProperty(propertyName, val);
        //    return r;
        //}

        //private void ValidateProperty<T>(string propertyName, T value)
        //{
        //    var results = new List<ValidationResult>();

        //    //ValidationContext context = new ValidationContext(this); 
        //    //context.MemberName = propertyName;
        //    //Validator.TryValidateProperty(value, context, results);

        //    if (results.Any())
        //    {
        //        //_errors[propertyName] = results.Select(c => c.ErrorMessage).ToList(); 
        //    }
        //    else
        //    {
        //        _errors.Remove(propertyName);
        //    }

        //    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        //}
    }
}
