﻿using DataAccessLayer;
using MVCLabb.Data.Repositories;
using MVCLabb.Data.Repositories.Interfaces;
using MVCLabb.Models;
using MVCLabb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Controllers
{
    [AllowAnonymous] /// Låter anonyma användare se sidan utan att vara authentiserade.
    public class HomeController : Controller
    {
        private IPictureRepository repo { get; set; }
        public HomeController(IPictureRepository repo)
        {
            this.repo = repo;
        }
        // GET: Home
        public ActionResult Index()
        {
            
            var newestPictures = new List<PictureViewModel>();
            var picturesFromDB = repo.All().OrderByDescending(x => x.DatePosted).Take(5).ToList();
                foreach (var pic in picturesFromDB)
                {
                    newestPictures.Add(EntityModelMapper.EntityToModel(pic));
                }
            

            return View(newestPictures);
        }
    }
}