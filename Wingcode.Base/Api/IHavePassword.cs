using System.Security;

namespace Wingcode.Base.Api
{
    /// <summary>
    /// An interface for a class that acn provide a secure password
    /// </summary>
    public interface IHavePassword
    {
        /// <summary>
        /// The Secure Password
        /// </summary>
        SecureString SecurePassword { get; }
    }
}
