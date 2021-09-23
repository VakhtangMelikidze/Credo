using System;
using System.Collections.Generic;
using System.Text;

namespace Credo.Domain.Models
{
    public partial class Log
    {
        public Guid Id { get; set; }
        public DateTime? LogDate { get; set; }
        public string Body { get; set; }
    }
}
