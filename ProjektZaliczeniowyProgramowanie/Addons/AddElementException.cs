using System;

namespace DBconnectShop.Addons {
    /// <summary>
    /// Błąd dodania do bazy danych
    /// </summary>
    public class AddElementException : Exception {
        private readonly string message;

        internal AddElementException(string message) {
            this.message = message;
        }

        public override string Message => message;
    }
}
