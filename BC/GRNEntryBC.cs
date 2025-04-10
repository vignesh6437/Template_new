using DAC;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC
{
  public  class GRNEntryBC
    {
        public ResponseGRNEntry GRNEntryPageloadBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GRNEntryPageloadDAL(request);
            return response;
        }
        public ResponseGRNEntry GRNEntryInsertBC(RequestGRNEntry request)
        {

            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAC = new WMSDAL();
            response= DAC.GRNEntryInsertDAL(request);
            return response;

        }
        public ResponseGRNEntry GRNEntryUpdateBC(RequestGRNEntry request)
        {

            ResponseGRNEntry response = new ResponseGRNEntry();
             WMSDAL DAC = new WMSDAL();
            response= DAC.GRNEntryUpdateDAL(request);
            return response;

        }
        public ResponseGRNEntry GRNEntryEditDtsBC(RequestGRNEntry request)
        {
            ResponseGRNEntry response = new ResponseGRNEntry();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GRNEntryEditDtsDAL(request);
            return response;
        }
    }
}
