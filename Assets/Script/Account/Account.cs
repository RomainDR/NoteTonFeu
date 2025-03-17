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

        public Account(string mail, string password, string name, bool autoLogin)
        {
            this.mail = mail;
            this.password = password;
            this.name = name;
            this.autoLogin = autoLogin;
        }

        public override string ToString()
        {
            return "Mail: "+ mail+"\nPassword: "+password+"\nName: "+name;
        }
    }
}