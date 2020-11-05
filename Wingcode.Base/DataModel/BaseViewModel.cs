using Prism.Mvvm;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wingcode.Base.Expressions;

namespace Wingcode.Base.DataModel
{
    public class BaseViewModel : BindableBase
    {

        #region Sync Event

        public delegate void SynchronizeHandler();

        public event SynchronizeHandler SyncEditro;
        #endregion

        #region Command Helpers

        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (indicating the function is already running) the action isn't run
        /// If the flag is false(indicating the fuction isn't already running) the action is run
        /// Onthe action is finished(or crashed) the flag is then reset to false
        /// </summary>
        /// <param name="updatingFlag"> The boolean flag dfinin if the cmommand is already running</param>
        /// <param name="action">Action to run</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            //check if the flag property is true
            if (updatingFlag.GetPropertyValue())
                return;

            //set the property flag to true
            updatingFlag.SetPropertyValue(true);

            try
            {
                //run passed in action
                await action();
            }
            finally
            {
                //set flag back to false after finish
                updatingFlag.SetPropertyValue(false);
            }
        }

        protected void RizeSyncEvent() 
        {
            SyncEditro?.Invoke();
        }
        #endregion
    }
}
