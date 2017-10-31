﻿using BusinessLogicRepository;
using HkEbPortal.Filters;
using HkEbPortal.Models.EB_PORTAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HkEbPortal.App_Start;

namespace HkEbPortal.Controllers
{
    [Authorization]
    public class ReimbursementController : BaseController
    {
        // GET: Reimbursement
        public ActionResult Index()
        {
            if (!new Common().IsOpenEnrollment(UserInfo.USUS_KY))
            {
                return Redirect("../eflexi/Home/Index");
            }
            string cliv_ky = Request["clivKy"];

            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB() { pEHUSER = UserInfo.USUS_ID };
            var list = CommonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB, SPEH_CLIV_CLAIM_INVOICE_INFO_LIST_WEB_RESULT>(entity);
            return View(list);
        }

        public ActionResult Add()
        {
            var fmfmentity = new SPEH_MEME_MEMBER_INFO_LIST_WEB() { pEHUSER = UserInfo.USUS_ID }; // 家庭成员
            var fmfmlist = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(fmfmentity);
            var entity = new SPEH_EBEB_VALUE_LIST() { pMEME_KY = UserInfo.USUS_KY };
            var list = CommonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(entity);
            var ivtype = new SPEH_SYSV_VALUE_LIST() { pSYSV_TYPE = "SYSV_CLIV_TYPE" };
            var ivlist = CommonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(ivtype);
            fmfmlist.ForEach(x => { x.MEME_NAME = x.SYSV_MEME_REL_CD_DESC + "-" + x.MEME_NAME; });
            var selectFMlist = new SelectList(fmfmlist, "MEME_KY", "MEME_NAME");
            var selectEBlist = new SelectList(list, "EBEB_KY", "EBEB_DESC");
            var selectIVlist = new SelectList(ivlist.Where(x => x.value == "I"), "value", "text", "I");
            ViewData["FMFM_DropDownList"] = selectFMlist;
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;
            return View();
        }

        [HttpPost]
        public JsonResult Add(FormCollection form)
        {
            string meme_ky = form["FMFM_DropDownList"];
            string ebeb_ky = form["EBEB_DropDownList"];
            string clivType = form["CLIV_DropDownList"];
            //string clivID = form["CLIV_ID"];
            string clivDate = form["CLIV_Date"];
            string applyDate = DateTime.Now.ToString("yyyy-MM-dd");
            //string apply_amt = form["APPLY_AMT"];
            string cliv_chg = form["CLIV_CHG"];
            string comment = form["COMMENT"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_INSERT
            {
                pMEME_KY = meme_ky,
                pFMFM_KY = UserInfo.USUS_KY,
                pGPGP_KY = UserInfo.GPGP_KY,
                pEBEB_KY = ebeb_ky,
                pSYSV_CLIV_TYPE = clivType,
                //pCLIV_ID = clivID,
                pCLIV_DT = DateTime.ParseExact(clivDate, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")).ToString("yyyy-MM-dd"),
                pCLIV_APP_DT = applyDate,
                //pCLIV_APPLY_AMT = apply_amt,
                pCLIV_CHG = cliv_chg,
                pCLIV_COMMENT = comment
            };
            CommonBl.Execute(entity);

            return Json(new { Code = entity.pRTN_CD, Msg = entity.pRTN_MSG }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Edit(string clivKy, string plpl_ky)
        {
            if (string.IsNullOrEmpty(clivKy)) return View();
            var fmfmentity = new SPEH_MEME_MEMBER_INFO_LIST_WEB() { pEHUSER = UserInfo.USUS_ID }; // 家庭成员
            var fmfmlist = CommonBl.QuerySingle<SPEH_MEME_MEMBER_INFO_LIST_WEB, SPEH_MEME_MEMBER_INFO_LIST_WEB_RESULT>(fmfmentity);
            var entity = new SPEH_EBEB_VALUE_LIST() { pMEME_KY = UserInfo.USUS_KY };
            var ebList = CommonBl.QuerySingle<SPEH_EBEB_VALUE_LIST, SPEH_EBEB_VALUE_LIST_RESULT>(entity);
            var ivtype = new SPEH_SYSV_VALUE_LIST() { pSYSV_TYPE = "SYSV_CLIV_TYPE" };
            var ivlist = CommonBl.QuerySingle<SPEH_SYSV_VALUE_LIST, SPEH_SYSV_VALUE_LIST_RESULT>(ivtype);
            var editentity = new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT() { pCLIV_KY = clivKy };
            var result = CommonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT, SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT>(editentity)?.FirstOrDefault();
            fmfmlist.ForEach(x => { x.MEME_NAME = x.SYSV_MEME_REL_CD_DESC + "-" + x.MEME_NAME; });
            var selectFMlist = new SelectList(fmfmlist, "MEME_KY", "MEME_NAME", result?.MEME_KY);
            var selectEBlist = new SelectList(ebList, "EBEB_KY", "EBEB_DESC", result?.EBEB_KY);
            var selectIVlist = new SelectList(ivlist.Where(x => x.value == "I"), "value", "text", result?.SYSV_CLIV_TYPE ?? "I");
            ViewData["FMFM_DropDownList"] = selectFMlist;
            ViewData["EBEB_DropDownList"] = selectEBlist;
            ViewData["CLIV_DropDownList"] = selectIVlist;

            return View(result);
        }

        [HttpPost]
        public JsonResult EditUpdate(FormCollection form)
        {
            if (string.IsNullOrEmpty(form["CLIV_KY"])) return Json(new { Code = 2, Msg = "Fail" }, JsonRequestBehavior.AllowGet);
            string cliv_ky = form["CLIV_KY"];
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
                pCLIV_KY = cliv_ky,
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
        public JsonResult Delete(string CLIV_KY)
        {
            var del = new SPEH_CLIV_CLAIM_INVOICE_INFO_DELETE() { pCLIV_KY = CLIV_KY };
            CommonBl.Execute(del);

            return Json(del.pRTN_MSG, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Upload()
        {
            string CLIV_KY = Request["clivKy"];
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT() { pCLIV_KY = CLIV_KY };
            var list = CommonBl.QuerySingle<SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT, SPEH_CLIV_CLAIM_INVOICE_INFO_SELECT_RESULT>(entity);
            ViewBag.LiuNo = CLIV_KY;

            //Image pic = Image.FromFile("");
            //MemoryStream ms = new MemoryStream();
            //pic.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //ms.Close();
            //ms = null;
            //pic.Dispose();
            //pic = null;

            //string imgPath = @"\Upload\images\"+ pic;

            ViewBag.ImagePath = list.Count > 0 ? list?.First()?.CLIV_IMG_PATH : "";
            return View();
        }

        public ActionResult GetImg()
        {
            var imgpath = Request["ImgPath"];

            Bitmap bmp = new Bitmap(100, 35);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            g.FillRectangle(Brushes.Red, 2, 2, 65, 31);
            g.DrawString("学习MVC", new Font("黑体", 15f), Brushes.Yellow, new PointF(5f, 5f));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            bmp.Dispose();
            return File(ms.ToArray(), "image/jpeg");
        }

        [HttpPost]
        public JsonResult UploadImg(string CLIV_KY)
        {
            string Str = "{\"result\":0,\"message\":\"submit success\",\"filename\":\"12424.jpg\",\"fileext\":\"...\"}";

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
                        string filePath = "";
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

                        var entiy = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE() { pCLIV_IMG_PATH = filePath, pCLIV_KY = CLIV_KY };
                        CommonBl.Execute(entiy);
                    }
                }
                else
                {
                    return Json("{\"result\":-1,\"message\":\"撒的撒\",\"filename\":\"" + files[i].FileName + "\",\"fileext\":\"" + Path.GetExtension(files[i].FileName) + "\"}", JsonRequestBehavior.AllowGet);
                }
            }

            return Json(Str, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCLIVSTS(string CLIV_KY)
        {
            var entity = new SPEH_CLIV_CLAIM_INVOICE_INFO_UPDATE() { pSYSV_CLIV_STS = "02", pCLIV_KY = CLIV_KY };
            CommonBl.Execute(entity);
            return Json(entity.pRTN_MSG, JsonRequestBehavior.AllowGet);
        }
    }
}