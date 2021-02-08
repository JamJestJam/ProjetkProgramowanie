using System;

namespace DBconnectShop.Addons {
    /// <summary>
    /// Wyjątek do logowania
    /// </summary>
    public class LoginException : Exception {
        private readonly string message;

        internal LoginException(string message) {
            this.message = message;
        }

        /// <summary>
        /// Treść błędu
        /// </summary>
        public override string Message => message;
    }
}
