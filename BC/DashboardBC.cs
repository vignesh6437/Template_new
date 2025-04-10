using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAC;
using LoginEntity;

namespace BC
{
    public class DashboardBC
    {
        public ResponseDashboard DashboardPageLoadBC(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            WMSDAL DAL = new WMSDAL();
            response = DAL.DashboardPageLoadDAL(request);

            return response;
        }
        public ResponseDashboard SendMessagesBC(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            WMSDAL DAL = new WMSDAL();
            response = DAL.SendMessageDAL(request);

            return response;
        }

        public ResponseDashboard GetMessagesBC(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            WMSDAL DAL = new WMSDAL();
            response = DAL.GetMessagesDAL(request);

            return response;
        }
        public ResponseDashboard MessageNotificationBC(RequestDashboard request)
        {
            ResponseDashboard response = new ResponseDashboard();
            WMSDAL DAL = new WMSDAL();
            response = DAL.MessageNotificationDAL(request);

            return response;
        }
    }
}
