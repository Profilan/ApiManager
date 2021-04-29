using ApiManager.Logic.Common;

namespace ApiManager.Logic.Models
{
    public class Status : ValueObject<Status>
    {
        public int Code { get; set; }
        public string Description { get; set; }

        private Status() { }

        public Status(int code, string description)
        {
            Code = code;
            Description = description;
        }

        protected override bool EqualsCore(Status other)
        {
            return Code == other.Code
                && Description == other.Description;
        }
    }
}
