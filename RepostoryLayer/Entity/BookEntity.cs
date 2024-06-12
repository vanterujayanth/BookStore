using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepostoryLayer.Entity
{
    public class BookEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }


        public int Id { get; set; }

        public string Title { get; set; }

        public long Originalprice { get; set; }
        public decimal Ratting {  get; set; }
       public int NumberOfRattings {  get; set; }

        public long Discountprice { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }

        //[JsonIgnore]
        //public virtual UserEntity userid { get; set; }

        

    }
}
