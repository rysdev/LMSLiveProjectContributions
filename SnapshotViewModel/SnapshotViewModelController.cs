using JobPlacementDashboard.Models;
using JobPlacementDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPlacementDashboard.Controllers
{
    public class SnapshotViewModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SnapshotViewModel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Snapshot()
        {
            var weeklyAppsList = new List<JPApplication>();
            var weeklyHiresList = new List<JPHire>();
            var newJpStudentsList = new List<JPStudent>();
            var jpStudentCount = 0;
            int unhiredGradCount = 0;
            int totalDaysInJP = 0, avgDaysInJP = 0, totalStudents=0;
            int startDateDay = 0, startDateMonth=0, startDateYear=0, todayDateDay=0, todayDateMonth=0, todayDateYear=0;

            foreach (var student in db.JPStudents)
            {
                int id = student.JPStudentId;

                startDateDay= student.JPStartDate.Day;                   /* ----------------PB--------------- */
                startDateMonth = student.JPStartDate.Month;
                startDateYear = student.JPStartDate.Year;
                todayDateDay = DateTime.Today.Day;
                todayDateMonth = DateTime.Today.Month;
                todayDateYear = DateTime.Today.Year;

                if (student.JPHired == true)
                {
                    if (startDateDay > todayDateDay)                         // checks if the start day was in a previous month
                    {                                                        // if so adjust day count for days in the previous month
                        if (todayDateMonth == 3) todayDateDay += 28;
                        else if (todayDateMonth == 10 || todayDateMonth == 5 || todayDateMonth == 7 || todayDateMonth == 12) todayDateDay += 30;
                        else todayDateDay += 31;
                    }

                    totalDaysInJP += (todayDateDay - startDateDay);
                    totalStudents++;
                } 

                var apps = db.JPApplications.Where(a => a.ApplicationUserId == student.ApplicationUserId).ToList();
                if (student.JPHired == true)
                {
                    var hire = db.JPHires.Where(a => a.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
                    if (hire.IsHiredWithinOneWeekOfCurrentDate == true) weeklyHiresList.Add(hire);
                }

                else if (student.JPHired == false && student.JPGraduated == false)
                {
                    jpStudentCount++;
                    if (student.IsStartDateWithinOneWeekOfCurrentDate == true) newJpStudentsList.Add(student);
                }

                else if (student.JPHired == false && student.JPGraduated == true)
                {
                    unhiredGradCount++;
                }

                foreach (var app in apps) if (app.IsAppliedDateWithinOneWeekOfCurrentDate == true) weeklyAppsList.Add(app);
            }

            avgDaysInJP = (totalDaysInJP / totalStudents);              // PB ---------------------------------------
     
            int totalWeeklyApps = weeklyAppsList.Count();
            int totalWeeklyHires = weeklyHiresList.Count();
            var snapshotStats = new SnapshotViewModel(newJpStudentsList, weeklyHiresList, totalWeeklyApps, totalWeeklyHires, jpStudentCount, unhiredGradCount, avgDaysInJP, totalDaysInJP); 
            //weeklyApps is 3x what it should be (it was 54 today (8/30), but it should be 18) because there are 3 copies of each seed student. Without duplicate seed data this should work fine.)

            return View(snapshotStats);

        }


        // GET: SnapshotViewModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SnapshotViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SnapshotViewModel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SnapshotViewModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SnapshotViewModel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SnapshotViewModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SnapshotViewModel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
