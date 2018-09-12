using JobPlacementDashboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.ViewModels
{

    public class SnapshotViewModel
    {
        [DisplayName("New Students")]
        public List<JPStudent> NewStudents { get; set; }
        [DisplayName("Weekly Applications")]
        public List<JPApplication> WeeklyApplications { get; set; }
        [DisplayName("Weekly Hires")]
        public List<JPHire> WeeklyHires { get; set; }
        [DisplayName("Total Weekly Applications")]
        public int TotalWeeklyApplications { get; set; }
        [DisplayName("Total Weekly Hires")]
        public int TotalWeeklyHires { get; set; }
        [DisplayName("Total Students")]
        public int TotalStudents { get; set; }
        [DisplayName("Unhired Graduates")]
        public int UnhiredGraduates { get; set; }
        [DisplayName("Average Days in Job Placement per Student")]              /* PB */
        public int AvgDaysInJP { get; set; }
        [DisplayName("Total Days in Job Placement for All Students")]
        public int TotalDaysInJP { get; set; }


        public SnapshotViewModel(List<JPStudent> newStudents, List<JPHire> weeklyHires, int totalWeeklyApps, int totalWeeklyHires, int totalStudents, int unhiredGraduates, int avgDaysInJP, int totalDaysInJP)
        {
            NewStudents = newStudents;
            WeeklyHires = weeklyHires;
            TotalWeeklyApplications = totalWeeklyApps;
            TotalWeeklyHires = totalWeeklyHires;
            TotalStudents = totalStudents;
            UnhiredGraduates = unhiredGraduates;
            AvgDaysInJP = avgDaysInJP;
            TotalDaysInJP = totalDaysInJP;
        }
    }
}