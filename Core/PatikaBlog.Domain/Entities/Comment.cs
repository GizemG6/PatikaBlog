using PatikaBlog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaBlog.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public int BlogId { get; set; }

        public AppUser User { get; set; }
        public Blog Blog { get; set; }
    }
}
