using AutoMapper;
using DigitalLeader.Entities;
using DigitalLeader.Services.Interfaces;
using DigitalLeader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLeader.Web.Areas.Admin.Controllers
{
    public class VacancyController : Controller
    {
        private ITechnologyService _technologyService;
        private IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService,
            ITechnologyService technologyService)
        {
            _technologyService = technologyService;
            _vacancyService = vacancyService;
        }


        // GET: Admin/Vacancy
        public ActionResult Index()
        {
            var vacancies = _vacancyService.GetAll();
           var viewModel = Mapper.Map<List<Vacancy>, List<VacancyViewModel>>(vacancies);

            return View(viewModel);
        }

        // GET: Admin/Vacancy/Create
        public ActionResult Create()
        {
            var viewModel = new VacancyViewModel
            {
                CreatedDate = DateTime.Now,
            };
            viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());

            return View(viewModel);
        }

        // POST: Admin/Vacancy/Create
        [HttpPost]
        public ActionResult Create(VacancyViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vacancy = Mapper.Map<VacancyViewModel, Vacancy>(viewModel);

                    vacancy.Technologies = viewModel.TechnologiesIds != null ?
                        _technologyService.GetByIds(viewModel.TechnologiesIds) : new List<Technology>();

                    _vacancyService.Insert(vacancy);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
            }


            return View(viewModel);
        }

        // GET: Admin/Vacancy/Edit
        public ActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(id));
            
           

            viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
            viewModel.TechnologiesSelectList.ForEach(item =>
            {
                item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
            });

            return View(viewModel);
        }

        // POST: Admin/Vacancy/Edit
        [HttpPost]
        public ActionResult Edit(VacancyViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vacancy = Mapper.Map<VacancyViewModel, Vacancy>(viewModel);

                    vacancy.Technologies = viewModel.TechnologiesIds != null ?
                        _technologyService.GetByIds(viewModel.TechnologiesIds) :
                        new List<Technology>();

                    _vacancyService.Update(vacancy);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(viewModel.ID));
               
                viewModel.TechnologiesSelectList = Mapper.Map<List<Technology>, List<SelectListItem>>(_technologyService.GetAll());
                viewModel.TechnologiesSelectList.ForEach(item =>
                {
                    item.Selected = viewModel.TechnologiesIds.Contains(int.Parse(item.Value));
                });
            }

            return View(viewModel);
        }

        // GET: Admin/Vacancy/Details
        public ActionResult Details(int id)
        {
            var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(id));

            return View(viewModel);
        }
        // GET: Admin/Vacancy/Delete
        public ActionResult Delete(int id)
        {
            var viewModel = Mapper.Map<Vacancy, VacancyViewModel>(_vacancyService.GetById(id));

            return View(viewModel);
        }

        // POST: Admin/Vacancy/Delete
        [HttpPost]
        public ActionResult Delete(VacancyViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<VacancyViewModel, Vacancy>(viewModel);

                    _vacancyService.Delete(entity);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(viewModel);
        }
    }
}