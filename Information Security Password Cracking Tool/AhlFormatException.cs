using System;
using System.Collections.Generic;
using System.Text;

namespace Generator {
    class AhlFormatException : Exception {
        private static string baseMessage = "Password must be of format: length 2-5, ";
        public AhlFormatException(string message) : base(baseMessage + message) {
        }
    }
}
