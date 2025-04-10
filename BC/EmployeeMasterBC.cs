using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAC;
using Entity;

namespace BC
{
    public  class EmployeeMasterBC
    {

        public ResponseEmployeeMaster EmployeeMasterPageLoadBC()
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EmployeeMasterPageLoadDAL();

            return response;
        }
        public ResponseEmployeeMaster EmployeeMasterInsertBC(RequestEmployeeMaster request)
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.InsertEmployeeMasterDAL(request);
            }
            return response;

        }
        public ResponseEmployeeMaster EmployeeMasterEditBC(RequestEmployeeMaster request)
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            WMSDAL DAL = new WMSDAL();
            response = DAL.EmployeeMasterEditDAL(request);

            return response;
        }

        public ResponseEmployeeMaster EmployeeMasterUpdateBC(RequestEmployeeMaster request)
        {
            ResponseEmployeeMaster response = new ResponseEmployeeMaster();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                WMSDAL DAL = new WMSDAL();
                response = DAL.UpdateEmployeeMasterDAL(request);
            }
            return response;

        }

        public List<ErrorItem> Validate(RequestEmployeeMaster request)
        {
            List<ErrorItem> err = new List<ErrorItem>();

            if (request.requestEmployeeMaster.EmployeeCode == "")
                err.Add(new ErrorItem { DataItem = "Employee Code", ErrorNo = "MRF0009" });
            if (request.requestEmployeeMaster.EmployeeName == "")
                err.Add(new ErrorItem { DataItem = "Employee Name", ErrorNo = "MRF0009" });
            if (request.requestEmployeeMaster.Department == "")
                err.Add(new ErrorItem { DataItem = "Department", ErrorNo = "MRF0009" });
            if (request.requestEmployeeMaster.Designation == "")
                err.Add(new ErrorItem { DataItem = "Designation", ErrorNo = "MRF0009" });
            if (request.requestEmployeeMaster.EmailID == "")
                err.Add(new ErrorItem { DataItem = "Email ID", ErrorNo = "MRF0009" });
            if (request.requestEmployeeMaster.ContactNO == "")
                err.Add(new ErrorItem { DataItem = "Contact NO", ErrorNo = "MRF0009" });
            if (request.requestEmployeeMaster.Status == "")
                err.Add(new ErrorItem { DataItem = "Status", ErrorNo = "MRF0009" });

            return err;
        }
    }
}
