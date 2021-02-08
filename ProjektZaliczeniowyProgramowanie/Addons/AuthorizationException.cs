using System;

namespace DBconnectShop.Addons {
    public class AuthorizationException : Exception {
        private readonly string message = "Wystąpił problem z autoryzacją";

        public AuthorizationException() { }

        public override string Message => message;
    }
}
