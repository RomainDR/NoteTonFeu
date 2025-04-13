using System;

namespace Script.Account
{
    [Serializable]
    public class Account
    {
        public string mail { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public bool autoLogin { get; set; }

        /**
         * Create an object of account. Password must be hashed !
         */
        public Account(string mail, string hashedPassword, string name, bool autoLogin = false)
        {
            this.mail = mail;
            this.password = hashedPassword;
            this.name = name;
            this.autoLogin = autoLogin;
        }

        public override string ToString()
        {
            return "Mail: "+ mail+"\nPassword: "+password+"\nName: "+name;
        }
    }
}