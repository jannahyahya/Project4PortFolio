using AutoRSN.Models;
using AutoRSN.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AutoRSN.Views.StorageType
{
    public class StorageTypeController : Controller
    {
        // GET: StorageType
        public ActionResult Index()
        {
            StorageRepository rp = new StorageRepository();
            List<AutoRSN.Models.StorageCode> storageType = rp.getAllStorageType();
            return View(storageType);
        }
        public ActionResult Create()
        {
            AutoRSN.Models.StorageCode type = new Models.StorageCode();
        
            return View(type);
        }

        public ActionResult Edit(string strageType)
        {
            StorageRepository repo = new StorageRepository();
             
            return View(repo.getStorageType(strageType));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StorageCode storageCode, FormCollection collection)
        {
            StorageRepository repo = new StorageRepository();
            if(repo.updateStorageType(storageCode))
            {
                TempData["Message"] = "Storage Type Update Successfully.. ";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Failed to update storage type";
                return View(storageCode);
            }

          //  return View(repo.getStorageType(strageType));
        }




        public ActionResult DeleteConfirmation(string storagetype)
        {
            TempData["Message"] = "Are You Sure You Want To Delete this storage type?  = " + storagetype;
            StorageCode storeCode = new StorageCode();
            storeCode.storageType = storagetype;
            List<StorageCode> list = new List<StorageCode>();
            list.Add(storeCode);
            return View(list);

        }

        public ActionResult Delete(string storeType) //used in customer controller
        {
            StorageRepository rp = new StorageRepository();
            if (rp.deleteStorageType(storeType))
            {
                TempData["Message"] = "storage type  = " + storeType + " Deleted";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["Message"] = "Error Deleting storeType.";

            }
            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StorageCode storageCode, FormCollection collection)
        {
            System.Diagnostics.Debug.WriteLine(storageCode.storageType);

            StorageRepository rp = new StorageRepository();
            if (!rp.checkStorageType(storageCode))
            {
                rp.insertStorageType(storageCode);
                TempData["Message"] = "New Storage Type Insert Successfully.. ";
                return RedirectToAction("Index");

            }

            else
            {
                TempData["Message"] = "Storage Type ALready Exists..! ";
                return View(storageCode);
            }
            //return View(storageType);
        }
    }
}