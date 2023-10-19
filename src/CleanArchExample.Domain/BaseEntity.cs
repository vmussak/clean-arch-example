using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Domain
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Messages = new List<string>();
        }

        public bool IsValid { get; set; } = true;
        public List<string> Messages { get; set; }

        protected void AddMessage(string message)
        {
            Messages.Add(message);
        }

        protected void AddMessages(List<string> messages)
        {
            Messages.AddRange(messages);
        }

        public abstract bool Validate();
    }
}
