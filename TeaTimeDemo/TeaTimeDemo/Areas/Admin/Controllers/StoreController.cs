﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using TeaTimeDemo.DataAccess.Data;
using TeaTimeDemo.DataAccess.Repository.IRepository;
using TeaTimeDemo.Models;
using TeaTimeDemo.Models.ViewModels;
using TeaTimeDemo.Utility;


namespace TeaTimeDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Store> objCategoryList = _unitOfWork.Store.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Store());
            }
            else
            {
                Store storeObj = _unitOfWork.Store.Get(u => u.Id == id);
                return View();
            }

        }
        [HttpPost]
        public IActionResult Upsert(Store storeObj)
        {
            if (ModelState.IsValid)
            {
                if (storeObj.Id == 0)
                {
                    _unitOfWork.Store.Add(storeObj);
                }
                else
                {
                    _unitOfWork.Store.Update(storeObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "店鋪新增成功!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(storeObj);
            }   
        }
            #region API CALLS
            [HttpGet]
        public IActionResult GetAll()
        {
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();
            return Json(new { data = objStoreList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var storeToBeDelete = _unitOfWork.Store.Get(u =>u.Id == id);
            if (storeToBeDelete == null)
            {
                return Json(new {success = false, message = "刪除失敗"});
            }
            _unitOfWork.Store.Remove(storeToBeDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }
        #endregion
    }
}
