using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using System;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HkEbPortal.Controllers
{
    [Authorization]
    [UserInfoIsConfirm]
    [IsOpenEnrollment(false)]
    public class ReimbursementController : BaseController
    {
        // GET: Reimbursement
        public ActionResult Index()
        {

            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB { pEHUSER = UserInfo.USUS_ID };
            var list = CommonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB, SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB_RESULT>(entity);
            var lang = Request.Cookies["defaultLang"]?.Value == "en";
            if (lang) list.ForEach(x =>
                        {
                            x.SYSV_CLIV_STS_DESC = x.SYSV_CLIV_STS_DESC_ENG;
                        });

            dynamic model = new ExpandoObject();
            model.Initial = UserInfo.INTIAL_AMT;
            model.Remaining = UserInfo.FMFM_CUR_AMT;
            model.List = list;
            return View(model);
        }

        public ActionResult Add()
        {
            // 家庭成员
            var fmlist = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(new SPEH_MEME_MEMBER_INFO_LIST_WEB { pEHUSER = UserInfo.USUS_ID });

            var eblist = CommonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(new SPEH_EBEB_VALUE_LIST { pMEME_KY = UserInfo.USUS_KY });

            var ivlist = CommonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(new SPEH_SYSV_VALUE_LIST { pSYSV_TYPE = "SYSV_CLIV_TYPE" });

            fmlist.ForEach(x => { x.MEME_NAME = x.SYSV_MEME_REL_CD_DESC + "-" + x.MEME_NAME; });
            
            ViewData["FMFM_DropDownList"] = new SelectList(fmlist, "MEME_KY", "MEME_NAME");
            ViewData["EBEB_DropDownList"] = new SelectList(eblist, "EBEB_KY", "EBEB_DESC");
            ViewData["CLIV_DropDownList"] = new SelectList(ivlist.Where(x => x.value == "I"), "value", "text", "I"); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Add(FormCollection form)
        {
            string memeKy = form["FMFM_DropDownList"];
            string ebebKy = form["EBEB_DropDownList"];
            string clivType = form["CLIV_DropDownList"];
            //string clivID = form["CLIV_ID"];
            string clivDate = form["CLIV_Date"];
            string applyDate = DateTime.Now.ToString("yyyy-MM-dd");
            //string apply_amt = form["APPLY_AMT"];
            string clivChg = form["CLIV_CHG"];
            string comment = form["COMMENT"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT
            {
                pEHUSER = UserInfo.USUS_ID,
                pMEME_KY = memeKy,
                pFMFM_KY = UserInfo.USUS_KY,
                pGPGP_KY = UserInfo.GPGP_KY,
                pEBEB_KY = ebebKy,
                pSYSV_CLIV_TYPE = clivType,
                //pCLIV_ID = clivID,
                pCLIV_DT = DateTime.ParseExact(clivDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")).ToString("yyyy-MM-dd"),
                pCLIV_APP_DT = applyDate,
                //pCLIV_APPLY_AMT = apply_amt,
                pCLIV_CHG = clivChg,
                pCLIV_COMMENT = comment
            };
            CommonBl.Execute(entity);

            return Json(new { Code = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.DenyGet);
        }


        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return View();

            // 家庭成员
            var fmfmlist = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(new SPEH_MEME_MEMBER_INFO_LIST_WEB { pEHUSER = UserInfo.USUS_ID });

            var ebList = CommonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(new SPEH_EBEB_VALUE_LIST { pMEME_KY = UserInfo.USUS_KY });

            var ivlist = CommonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(new SPEH_SYSV_VALUE_LIST { pSYSV_TYPE = "SYSV_CLIV_TYPE" });

            var result = CommonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT, SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT>(new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT { pCLIV_KY = id })?.FirstOrDefault();
            fmfmlist.ForEach(x => { x.MEME_NAME = x.SYSV_MEME_REL_CD_DESC + "-" + x.MEME_NAME; });

            var selectFMlist = new SelectList(fmfmlist, "MEME_KY", "MEME_NAME", result?.MEME_KY);
            var selectEBlist = new SelectList(ebList, "EBEB_KY", "EBEB_DESC", result?.EBEB_KY);
            var selectIVlist = new SelectList(ivlist.Where(x => x.value == "I"), "value", "text", result?.SYSV_CLIV_TYPE ?? "I");
            ViewData["FMFM_DropDownList"] = selectFMlist;
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;

            ViewBag.Id = id;

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(FormCollection form)
        {
            if (string.IsNullOrEmpty(form["CLIV_KY"])) return Json(new { Code = 2, Msg = "Fail" }, JsonRequestBehavior.AllowGet);
            string clivKy = form["CLIV_KY"];
            string meme = form["FMFMDropDownList"];
            string ebeb = form["EBEBDropDownList"];
            string ivtype = form["CLIVDropDownList"];
            //string ivid = form["CLIV_ID"];
            string ivdate = form["CLIV_Date"];
            //string appAMT = form["ApplyAMT"];
            string ivchg = form["CLIV_CHG"];
            //string appdate = form["Apply_Date"];
            string comment = form["COMMENT"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE()
            {
                pFMFM_KY = UserInfo.USUS_KY,
                pCLIV_KY = clivKy,
                pGPGP_KY = UserInfo.GPGP_KY,
                pMEME_KY = meme,
                pEBEB_KY = ebeb,
                pSYSV_CLIV_TYPE = ivtype,
                //pCLIV_ID = ivid,
                pCLIV_CHG = ivchg,
                //pCLIV_APPLY_AMT = appAMT,
                pCLIV_DT = DateTime.ParseExact(ivdate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")).ToString("yyyy-MM-dd"),
                //pCLIV_APP_DT = appdate,
                pCLIV_COMMENT = comment
            };
            CommonBl.Execute(entity);

            return Json(new { Code = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UploadImg(string id)
        {
            string filePath = "";
            //前台页面通过 < file name = "img" > 标签数组上传图片，后台根据Request.Files["img"]来接收前台上传的图片。
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (files.Count == 0)
                return Json("{\"result\":-1,\"message\":\"submit failed\",\"filename\":\"12424.jpg\",\"fileext\":\"...\"}", JsonRequestBehavior.AllowGet);

            for (int i = 0; i < files.AllKeys.Count(); i++)
            {
                if (files.AllKeys[i] != "img")
                {
                    if (files[i].FileName.Length > 0)
                    {
                        System.Web.HttpPostedFile postedfile = files[i];
                        filePath = "";
                        var ext = Path.GetExtension(postedfile.FileName);
                        var fileName = DateTime.Now.Ticks.ToString() + ext;
                        // 组合文件存储的相对路径
                        filePath = "/Upload/images/" + fileName;
                        if (!Directory.Exists(HttpRuntime.AppDomainAppPath + "/Upload/images/"))
                        {
                            Directory.CreateDirectory(HttpRuntime.AppDomainAppPath + "/Upload/images/");
                        }
                        // 将相对路径转换成物理路径
                        var path = Server.MapPath(filePath);
                        postedfile.SaveAs(path);
                        string fex = Path.GetExtension(postedfile.FileName);

                        var entiy = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE() { pCLIV_IMG_PATH = filePath, pCLIV_KY = id, pEHUSER = UserInfo.USUS_ID };
                        CommonBl.Execute(entiy);

                    }
                }
                else
                {
                    return Json("{\"result\":-1,\"message\":\"撒的撒\",\"filename\":\"" + files[i].FileName + "\",\"fileext\":\"" + Path.GetExtension(files[i].FileName) + "\"}", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = 0, message = "upload success", filename = "../" + filePath, fileext = "...." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(string id)
        {
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_DELETE { pCLIV_KY = id, pEHUSER = UserInfo.USUS_ID };
            CommonBl.Execute(entity);
            return Json(entity.pRTN_MSG, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Upload()
        //{
        //    string clivKy = Request["clivKy"];
        //    var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT() { pCLIV_KY = clivKy };
        //    var result = CommonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT, SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT>(entity)?.FirstOrDefault();
        //    ViewBag.LiuNo = clivKy;

        //    //Image pic = Image.FromFile("");
        //    //MemoryStream ms = new MemoryStream();
        //    //pic.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    //ms.Close();
        //    //ms = null;
        //    //pic.Dispose();
        //    //pic = null;

        //    //string imgPath = @"\Upload\images\"+ pic;

        //    ViewBag.ImagePath = result?.CLIV_IMG_PATH ?? "";
        //    return View(result);
        //}

        //public ActionResult GetImg()
        //{
        //    var imgpath = Request["ImgPath"];

        //    Bitmap bmp = new Bitmap(100, 35);
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.Clear(Color.White);
        //    g.FillRectangle(Brushes.Red, 2, 2, 65, 31);
        //    g.DrawString("学习MVC", new Font("黑体", 15f), Brushes.Yellow, new PointF(5f, 5f));
        //    MemoryStream ms = new MemoryStream();
        //    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    g.Dispose();
        //    bmp.Dispose();
        //    return File(ms.ToArray(), "image/jpeg");
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditFsaClaimStatus(string id)
        {
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE { pSYSV_CLIV_STS = "02", pCLIV_KY = id, pEHUSER = UserInfo.USUS_ID };
            CommonBl.Execute(entity);
            return Json(new { Code = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.DenyGet);
        }
    }
}