using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCGridDemo.Model
{
    public class Book
    {
        public int Index { get; set; }
        public int Position { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Imprint { get; set; }
        public string PublisherGroup { get; set; }
        public long Volume { get; set; }
        public int BindingTypeId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
