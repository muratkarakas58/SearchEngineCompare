using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineCompare.Entity
{
    public class Search
    {

        public Guid SearchId { get; set; }
        public Guid SearchCompareId { get; set; }
        public string SearchPhrase { get; set; }
        public int PhraseTypeId { get; set; }
        public int SearchEngineId { get; set; }
        public int MeasuredParameterId { get; set; }
        public double MeasuredParameterPerformance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
