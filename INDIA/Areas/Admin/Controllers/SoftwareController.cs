
using BusinessRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.VMModel;

namespace INDIA.Areas.Admin.Controllers
{
    public class SoftwareController : Controller
    {
        // GET: Admin/Software
        IRepository repository;
        public ActionResult Index()
        {
            return View();
        }

        public SoftwareController()
        {
            repository = new Repository();
        }
        [HttpPost]
        public  JsonResult AddSoftware(VMSoftware vmsoftware)
        {
            return Json(repository.AddSoftware(vmsoftware));
        }

        [HttpGet]
        public JsonResult getSoftwareList()
        {
            return Json(repository.getSoftwareList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getsoftwareid(int softwareid)
        {
            return Json(repository.getsoftwareid(softwareid), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult deleteSoftware(int deleteid)
        {
            return Json(repository.deleteSoftware(deleteid), JsonRequestBehavior.AllowGet);
        }
    }
}