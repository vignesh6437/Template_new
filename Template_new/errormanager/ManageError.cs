using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Web;
using Entity;
using DAC;

namespace Template_new
{
    public class ManageError
    {

        public String GenerateErrorMessages(List<ErrorItem> e)
        {
            try
            {
                ResourceManager rm = Template_new.ErrorManager.ErrorMessage.ResourceManager;
                StringBuilder sb = new StringBuilder();
                if (e != null)
                {
                    foreach (ErrorItem Err in e)
                    {
                        sb.Append(" -" + HttpUtility.HtmlEncode(rm.GetString(Err.ErrorNo)) + "<Br />");
                        sb.Replace("{A}", Err.DataItem);
                    }
                }
                if (sb.Length > 0)
                {
                    return sb.ToString().Substring(0, (sb.Length - 6)).Replace("'", "&#39");
                }
                else
                {
                    return sb.ToString();
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
    }
}