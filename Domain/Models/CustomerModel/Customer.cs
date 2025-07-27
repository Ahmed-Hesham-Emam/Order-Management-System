using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.CustomerModel
    {
    public class Customer : BaseEntity<Guid>
        {
        //public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Order> Orders { get; set; }


        public Guid UserId { get; set; }
        }
    }
