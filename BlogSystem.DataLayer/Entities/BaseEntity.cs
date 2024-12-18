using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_System.DataLayer.Entities
{
    public class BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }=DateTime.Now;
        public bool IsDelete { get; set; }
    }
}
