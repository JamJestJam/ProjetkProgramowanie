using System;

namespace DBconnectShop.Addons {
    public class AddElementException : Exception {
        private readonly string message;

        public AddElementException(string message) {
            this.message = message;
        }

        public override string Message => message;
    }
}
