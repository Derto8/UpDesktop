using System;
using System.Collections.Generic;
using System.Text;

namespace UpDesktop.Core.Entities
{
    public class History
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RequestId { get; set; }
        public Requests Request { get; set; }

        public string Comment { get; set; }

        public string TimeSpent { get; set; }
    }
}
