using System;

namespace Split
{
    public class Account {
        public string Id { get; set; }
        public string Hash { get; set; }
        public string Mail { get; set; }

        public Account() { }

        public Account(string id, string hash, string mail) {
            Id=id;
            Hash=hash;
            Mail=mail;
        }
    }
}