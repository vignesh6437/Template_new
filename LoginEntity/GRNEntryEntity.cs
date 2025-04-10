using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GRNEntryEntity
    {
        public string AUTOID { get; set; }
        public string USERCODE { get; set; }
        public string DATE { get; set; }
        public string GRNNO { get; set; }
    }
    public class GRNEntryDetailEntity
    {
        public string GRNNO { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public string QUANTITY { get; set; }
        public string Remove { get; set; }


    }
    public class RequestGRNEntry
    {
        public GRNEntryEntity ReqGRNEntry { get; set; }
        public List<GRNEntryDetailEntity> requestGRNEntrydetails { get; set; }
    }
    public class ResponseGRNEntry
    {
        public DataTable JS_Supplierdetails { get; set; }
        public DataTable RecordStatus { get; set; }
        public DataTable JS_Itemdetails { get; set; }
        public DataTable BomHeader { get; set; }
        public DataTable BomDetails { get; set; }
        public bool result { get; set; }
        public List<ErrorItem> ErrorContainer { get; set; }
    }
}

