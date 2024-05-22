using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelLayer
{
    public class BookModel
    {
      
        public string Title { get; set; }

        public long Originalprice { get; set; }
        public decimal Ratting { get; set; }
        public int NumberOfRattings { get; set; }

        public long Discountprice { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }

    }
}
