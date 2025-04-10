using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using DAC;
using System.Text;
using System.Threading.Tasks;

namespace BC
{
    public class LoginBC
    {
        public ResponseLoginDetails LoginsBC(RequestLoginDetails request)
        {

            ResponseLoginDetails response = new ResponseLoginDetails();
            WMSDAL DAL = new WMSDAL();
            response.ErrorContainer = Validate(request);
            if (response.ErrorContainer.Count == 0)
            {
                response = DAL.LoginDAL(request);
            }
            return response;
        }

        public ResponseLoginDetails LogoutUpdateBC(RequestLoginDetails request)
        {

            ResponseLoginDetails response = new ResponseLoginDetails();
            WMSDAL DAL = new WMSDAL();
            response.ErrorContainer = Validate(request);           
            response = DAL.LogoutUpdateDAL(request);
            return response;
        }


        public List<ErrorItem> Validate(RequestLoginDetails request)
        {
            List<ErrorItem> err = new List<ErrorItem>();
            if (request.requestLoginDetails.UserName == "")
                err.Add(new ErrorItem { DataItem = "User Name", ErrorNo = "SSB0009" });
            if (request.requestLoginDetails.Password == "")
                err.Add(new ErrorItem { DataItem = "Password", ErrorNo = "SSB0009" });


            return err;
        }
    }
}
