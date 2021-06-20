using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineCompare.Models.Concrete
{
    public class SearchAndInsertDbRequest
    {
        public string SearchPhrase { get; set; }
        public int SearchEngineId { get; set; }
        public Guid SearchCompareId { get; set; } 
    }
}
