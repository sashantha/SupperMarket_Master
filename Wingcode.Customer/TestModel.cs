using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;

namespace Wingcode.Customer
{
    public class TestModel : BaseModel<TestModel>
    {

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set{ SetProperty(ref _lastName, value); }
        }

    }
}
