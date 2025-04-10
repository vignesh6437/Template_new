using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAC;
using Entity;

namespace BC
{
    public class UpdateProfileBC
    {
        public ResponseUpdateProfile UpdateProfilePageLoadBC(RequestUpdateProfile request)
        {
            ResponseUpdateProfile response = new ResponseUpdateProfile();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateProfilePageLoadDAL(request);

            return response;
        }
        public ResponseUpdateProfile UpdateProfileInsertBC(RequestUpdateProfile request)
        {
            ResponseUpdateProfile response = new ResponseUpdateProfile();
            WMSDAL DAL = new WMSDAL();
            response = DAL.UpdateProfileInsertDAL(request);

            return response;
        }
    }
}
