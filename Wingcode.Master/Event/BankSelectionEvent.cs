﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Master.Event
{
    internal class BankSelectionEvent : PubSubEvent<Bank>
    {
    }
}
