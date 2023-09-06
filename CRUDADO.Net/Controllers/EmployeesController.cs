using CRUDADOdotNet.Data;
using CRUDADOdotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDADOdotNet.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDAL _dal;
        public EmployeesController(EmployeeDAL dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            List<Employee>employees=new List<Employee>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"]= ex.Message;
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if(!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Model data is not invalid";
            }
            bool result = _dal.Insert(emp);
            if(!result)
            {
                TempData["errorMessage"] = "Unable to save the data";
                return View();
            }
            TempData["successMessage"] = "Employee data saved successfull!";

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            try
            {
                Employee emp = _dal.GetById(id);
                if(emp.Id==0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
                bool result = _dal.Update(emp);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to update the data";
                    return View();
                }
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            TempData["successMessage"] = "Employee data update successfull!";

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            try
            {
                Employee emp = _dal.GetById(id);
                if (emp.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Employee emp)
        {
            try
            {
                bool result = _dal.Delete(emp.Id);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data";
                    return View();
                }
                else
                {
                    TempData["successMessage"] = "Employee data delete successfull!";
                    return RedirectToAction("Index");
                }
               
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
           

        }
    }
}
