using System;

namespace DBconnectShop.Addons {
    /// <summary>
    /// Błąd autoryzacji
    /// </summary>
    public class AuthorizationException : Exception {
        private readonly string message = "Wystąpił problem z autoryzacją";

        internal AuthorizationException() { }

        public override string Message => message;
    }
}
