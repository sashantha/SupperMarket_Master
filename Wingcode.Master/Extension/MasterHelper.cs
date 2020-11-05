using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Master.Extension
{
    internal static class MasterHelper
    {

        public static Branch CreateNewBranch(this Branch branch) 
        {
            return branch = new Branch() 
            {
                id = 0,
                code = string.Empty,
                companyName = string.Empty,
                address = string.Empty,
                contact = string.Empty
            };
        }

        public static bool IsValidBranch(this Branch b) 
        {
            return (!string.IsNullOrEmpty(b.name) 
                && !string.IsNullOrEmpty(b.companyName) 
                && !string.IsNullOrEmpty(b.contact));
        }

        public static Bank CreateNewBank(this Bank bank)
        {
            return bank = new Bank() 
            {
                id = 0,
                name = string.Empty
            };
        }

        public static bool IsValidBank(this Bank b)
        {
            return (!string.IsNullOrEmpty(b.name));
        }

        public static BranchAccount CreateNewBranchAccount(this BranchAccount account)
        {
            return account = new BranchAccount() 
            {
                id = 0,
                accountNo = string.Empty
            };
        }

        public static bool IsValidBranchAccount(this BranchAccount account)
        {
            return (!string.IsNullOrEmpty(account.accountNo));
        }

        public static Unit CreateNewUnit(this Unit measure)
        {
            return measure = new Unit()
            {
                id = 0,
                unitName = string.Empty,
                abbreviation = string.Empty,
                unitType = string.Empty
            };
        }

        public static bool IsValidUnit(this Unit measure)
        {
            return (!string.IsNullOrEmpty(measure.unitName));
        }      
    }
}
