using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BC;
using Entity;
using Newtonsoft.Json;
using Template_new.Common;

namespace Template_new.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        #region
        public ActionResult GRN()
        {
            return View();
        }

        public ActionResult GRNEntryPageLoad()
        {
            listBomCreationItemdet = new List<GRNEntryDetailEntity>();
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.ReqGRNEntry = new GRNEntryEntity();
            GRNEntryBC bc = new GRNEntryBC();
            response = bc.GRNEntryPageloadBC(request);

            string resulttable = Utility.DataTableToJSONWithJavaScriptSerializer(response.JS_Supplierdetails);

            var data = resulttable;
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public class GRNEntryDetails
        {
            public string MaterialCode { get; set; }
            public string MaterialName { get; set; }
            public string Quantity { get; set; }
            public string Remove { get; set; }

        }
        static List<GRNEntryDetailEntity> listBomCreationItemdet = new List<GRNEntryDetailEntity>();
        public ActionResult AddBOMCreationItemDetails()
        {
            List<GRNEntryDetails> listBomItemInsertdet = new List<GRNEntryDetails>();


            if (Request.Form["action"].ToString() == "Update")
            {
                for (int i = listBomCreationItemdet.Count - 1; i >= 0; i--)
                {
                    if (listBomCreationItemdet[i].ITEMCODE == Request.Form["MATERIALCODEdts"])
                    {
                        listBomCreationItemdet.RemoveAt(i);
                    }
                }

            }
            int duplicatecount = listBomCreationItemdet.Where(inventorylist => inventorylist.ITEMCODE == Request.Form["MATERIALCODEdts"].ToString()).Count();

            string data = "";
            if (duplicatecount == 0)
            {
                GRNEntryDetailEntity Itemdts = new GRNEntryDetailEntity();
                Itemdts.ITEMCODE = Request.Form["MATERIALCODEdts"];
                Itemdts.ITEMNAME = Request.Form["MATERIALNAMEdts"];
                Itemdts.QUANTITY = Request.Form["QTY"];


                listBomCreationItemdet.Add(Itemdts);


                listBomItemInsertdet = (from inv in listBomCreationItemdet.AsEnumerable()
                                        select new GRNEntryDetails
                                        {
                                            MaterialCode = inv.ITEMCODE,
                                            MaterialName = inv.ITEMNAME,
                                            Quantity = inv.QUANTITY,

                                        }).ToList();
                data = "true" + "|" + JsonConvert.SerializeObject(listBomItemInsertdet);

            }
            else
            {
                data = "false" + "|" + JsonConvert.SerializeObject("Material Code " + Request.Form["MATERIALCODEdts"].ToString() + " Already Exist.");
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BomCreationDeleteRow(string Code)
        {

            listBomCreationItemdet.Remove(listBomCreationItemdet.Single(r => r.ITEMCODE == Code));

            List<GRNEntryDetails> listBomCreationItemInsertdet = new List<GRNEntryDetails>();

            listBomCreationItemInsertdet = (from inv in listBomCreationItemdet.AsEnumerable()
                                            select new GRNEntryDetails
                                            {
                                                MaterialCode = inv.ITEMCODE,
                                                MaterialName = inv.ITEMNAME,
                                                Quantity = inv.QUANTITY,
                                            }).ToList();


            return Json(JsonConvert.SerializeObject(listBomCreationItemInsertdet), JsonRequestBehavior.AllowGet);

        }
        public ActionResult InsertBomDetails(string actiontype, string AUTOID)
        {
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.ReqGRNEntry = new GRNEntryEntity();
            request.requestGRNEntrydetails = listBomCreationItemdet;
            request.ReqGRNEntry.GRNNO = Request.Form["GRNNO"];
            request.ReqGRNEntry.DATE = Request.Form["DATE"];
            request.ReqGRNEntry.USERCODE = Session["LoginEmployeeCode"].ToString();
            GRNEntryBC bc = new GRNEntryBC();
            string value = "Update";
            if (value != actiontype)
            {
                response = bc.GRNEntryInsertBC(request);
            }
            else
            {
                request.ReqGRNEntry.AUTOID = AUTOID;
                response = bc.GRNEntryUpdateBC(request);
            }


            ManageError Err = new ManageError();

            string resultjson = Err.GenerateErrorMessages(response.ErrorContainer);
            resultjson = response.result + "|" + resultjson;

            JsonResult json = Json(resultjson, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }

        public ActionResult BomMasterEdit(string AUTOID)
        {
            RequestGRNEntry request = new RequestGRNEntry();
            ResponseGRNEntry response = new ResponseGRNEntry();
            request.ReqGRNEntry = new GRNEntryEntity();
            request.ReqGRNEntry.AUTOID = AUTOID;
            GRNEntryBC bc = new GRNEntryBC();
            response = bc.GRNEntryEditDtsBC(request);

            listBomCreationItemdet = new List<GRNEntryDetailEntity>();

            for (int i = 0; i < response.BomDetails.Rows.Count; i++)
            {
                GRNEntryDetailEntity BOMNOEdit = new GRNEntryDetailEntity();

                BOMNOEdit.ITEMCODE = response.BomDetails.Rows[i]["MaterialCode"].ToString();
                BOMNOEdit.ITEMNAME = response.BomDetails.Rows[i]["MaterialName"].ToString();
                BOMNOEdit.QUANTITY = response.BomDetails.Rows[i]["Quantity"].ToString();
                BOMNOEdit.Remove = response.BomDetails.Rows[i]["Remove"].ToString();

                listBomCreationItemdet.Add(BOMNOEdit);
            }

            var data = Utility.DataTableToJSONWithJavaScriptSerializer(response.BomHeader) + "|" +
                       Utility.DataTableToJSONWithJavaScriptSerializer(response.BomDetails);

            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }

        #endregion
    }
}