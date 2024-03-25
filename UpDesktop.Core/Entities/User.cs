using System;
using System.Collections.Generic;
using System.Text;

namespace UpDesktop.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public int? RequestId { get; set; }
        public Requests Request { get; set; }

        public IEnumerable<History> History { get; set; }
    }
}
