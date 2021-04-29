
using System;

namespace ApiManager.Logic.Models
{
    public class ActionHttpHeader
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }

        public virtual TaskReceiver Action { get; set; }

        public ActionHttpHeader() { }

        public ActionHttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
