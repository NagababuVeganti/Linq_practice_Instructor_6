﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Linq_Practice_6.DataAccess;
using Linq_Practice_6.Models;
using System.Collections.Generic;

namespace Linq_Practice_6
{
    public class GroupTotalsByCollege
    {
        public string College { get; set; }
        public int scores { get; set; }
    }

    public class TotalByStudentAndCollege
    {
        public string Name { get; set; }
        public string College { get; set; }

        public int total_score { get; set; }
    }

    public class EnrollmentByStudentAndCollege
    {
        public string Name { get; set; }
        public string College { get; set; }

        public List<Enrollment> scores { get; set; }

    }

    public class EnrollmentsByCollege
    {
        public string College { get; set; }
        public List<Enrollment> scores { get; set; }
    }

    public class HomeController : Controller
    {
        SchoolDbContext dbContext;
        public HomeController(SchoolDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {

            populateData();

            return View();
        }

        public async Task<IActionResult> Queries()
        {

            /*
             
            //Query 1 Basic Select

            //Get all the records from Enrollments table.

            List<Enrollment> allrecords = dbContext.Enrollments.ToList();
            Console.WriteLine("Feteched sll Records");
            ViewData["title"] = "All Records";
            ViewData["data"] = allrecords;
            return View("allrecords");

            */


            /*
             
            //Query 2: Select with filter(where).

            //Get all the records from Enrollments table with A Grade.

            List<Enrollment> allrecords = dbContext.Enrollments.Where(c=>c.Grade.Equals("A")).ToList();
            Console.WriteLine("Feteched qll Records with A Grade");
            ViewData["title"] = "All Records with A Grade";
            ViewData["data"] = allrecords;
            return View("allrecords");
             
            */



            /* 

            // Query 3: Select Query to get all the records from Enrollments table.
            

             List<Enrollment> allrecords= dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s=>s.Course.College).ToList();
             Console.WriteLine("Feteched Records");
             ViewData["title"] = "All Records";
             ViewData["data"] = allrecords;
             return View("data");

            */


            /*
           

            // Query 4:Select All records in the Enrollments table with Student and course information for the Course number ISM 6225.


            List<Enrollment> ism_records = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).Where(s => s.Course.Number.Contains("ISM 6225")).ToList();
            Console.WriteLine("Feteched Records for ISM 6225");
            ViewData["title"] = "Records of Ross";
            ViewData["data"] = ism_records;
            return View("data");

           */




            /*

             // Quer 5: Select All records in the Enrollments table with a givencollege Abbreviation (Example Abbreviation:”CoE”)

             List<Enrollment> CoE_records = dbContext.Enrollments.Include(s => s.Student).Include(s => s.Course).Include(s => s.Course.College).Where(s => s.Course.College.Abbreviation.Contains("CoE")).ToList();
             Console.WriteLine("Feteched Records for MCOB College");
             ViewData["title"] = "Records of MCOB College:";
             ViewData["data"] = MCOB_records;
             return View("data");

             */




            /*

                 // Query 6.1: Group by Query on Enrollments table to get sum of total scores of all students by College Name or College Abbreviation.

                 List<EnrollmentsByCollege> EnrollmentsGroup1 =  dbContext.Enrollments
                                                                            .Include(s => s.Student)
                                                                            .Include(s => s.Course)
                                                                            .Include(s => s.Course.College)
                                                                            .GroupBy(s => s.Course.College.Name)
                                                                            .Select(s => new EnrollmentsByCollege
                                                                                        {
                                                                                            College= s.Key,
                                                                                            scores=s.ToList()
                                                                                        })
                                                                            .ToList();


                  ViewData["title"] = "Sum of all students Scores Grouped By College";

                  ViewData["data"] = EnrollmentsGroup1;

                  return View("collgeGroupby");

        */



            /*

            // Query 6.2: Group by Query on Enrollments table to get sum of total scores of all students by College Name or College Abbreviation.

            List<GroupTotalsByCollege> TotalScoresByCollege =  dbContext.Enrollments
                                                                        .Include(s => s.Student)
                                                                        .Include(s => s.Course.College)
                                                                        .GroupBy(s =>  s.Course.College.Name)
                                                                        .Select(s => new GroupTotalsByCollege
                                                                                       {
                                                                                           College= s.Key,
                                                                                           scores=s.Sum(s => s.score)
                                                                                       })
                                                                        .ToList();

           ViewData["title"] = "Sum of all students Scores Grouped By College";

           ViewData["data"] = TotalScoresByCollege;

           return View("collgeGroupby");

          */


            // Query 7.1: Group By Query  multiple Columns

            //Get the total scores for a student across all enrollments by college 

            List<EnrollmentByStudentAndCollege> EnrollmentsByStudentAndCollege =
                                                dbContext.Enrollments
                                                            .Include(s => s.Student)
                                                            .Include(s => s.Course)
                                                            .Include(s => s.Course.College)
                                                            .GroupBy(s => new
                                                            {
                                                                s.Student.Name,
                                                                s.Course.College.Abbreviation
                                                            })
                                                            .Select(s => new EnrollmentByStudentAndCollege
                                                            {
                                                                Name = s.Key.Name,
                                                                College = s.Key.Abbreviation,
                                                                scores = s.ToList()
                                                            })
                                                            .ToList();

            ViewData["title"] = "Sum of Scores Grouped By College and Student Name";

            ViewData["data"] = EnrollmentsByStudentAndCollege;

            return View("group");






            // Query 7.2: Group By Query  multiple Columns

            //Get the total scores for a student across all enrollments by college 

            List<TotalByStudentAndCollege> TotalsByStudentAndCollege = dbContext.Enrollments
                                                .Include(s => s.Student)
                                                .Include(s => s.Course)
                                                .Include(s => s.Course.College)
                                                .GroupBy(s => new
                                                {
                                                    s.Student.Name,
                                                    s.Course.College.Abbreviation
                                                })
                                                .Select(s => new TotalByStudentAndCollege
                                                {
                                                    Name = s.Key.Name,
                                                    College = s.Key.Abbreviation,
                                                    total_score = s.Sum(s => s.score)

                                                })
                                                .ToList();

            ViewData["title"] = "Sum of Scores Grouped By College and Student Name";

            ViewData["data"] = TotalsByStudentAndCollege;

            return View("group");






        }


        void populateData()
        {
            Random rnd = new Random();

            string[] Colleges = {
                                 "Muma College of Business, MCOB",
                                 "College of Engineering, CoE",
                                 "College of Arts and Sciences, CAS",
                                 "College of Nursing, CON",
                                 "Morsani College of Medicine,MCOM",
                                 "College of Public Health,COPH"
            };
            string[] Courses = {
                "ISM 6225, Distributed Information Systems",
                "ISM 6218, Advanced Database Management Systems",
                "ISM 6137, Statistical Data Mining",
                "ISM 6419, Data Visualization",
                "ISM 6930, Blockchain Fundamentals",
                "ISM 6562, Big Data for Business",
                "ISM 6328, Information Security and IT Risk Management",
                "QMB 6304, Analytical Methods For Business",
                "ISM 6136, Data Mining",
                "NUR 3125, Pathophysiology for Nursing Practice",
                "NUR 3145, Pharmacology in Nursing Practice",
                "NUR 4165, Nursing Inquiry",
                "NUR 3078, Information Technology Skills for Nurses",
                "NUR 4169, Evidence-Based Practice for Baccalaureate Prepared Nurse",
                "NUR 4634, Population Health",
                "NSP 3147, Web-Based Education for Staff Development"

            };

            string[] Students = {
                "Monica", "Sara","Adam","Jude","Callie","Ross","Stark",
                "Chandler","Phoebe","Carrie","Tristan","sally","Robert",
                "Sid","Warner","Joey","Andy","Conner","Ruby","Kate"
            };
            string[] Grades = { "A", "A-", "B+", "B", "B-" };
            int[] scores = { 95, 91, 87, 82, 75, 66, 55, 80, 63, 90, 45, 60 };

            College[] colleges = new College[Colleges.Length];
            Course[] courses = new Course[Courses.Length];
            Student[] students = new Student[Students.Length];

            for (int i = 0; i < Colleges.Length; i++)
            {
                College college = new College
                {
                    Name = Colleges[i].Split(",")[0],
                    Abbreviation = Colleges[i].Split(",")[1]
                };

                dbContext.Colleges.Add(college);
                colleges[i] = college;
            }

            for (int i = 0; i < Courses.Length; i++)
            {
                Course course = new Course
                {

                    Number = Courses[i].Split(",")[0],
                    Name = Courses[i].Split(",")[1],
                    College = colleges[rnd.Next(colleges.Length)]
                };

                dbContext.Courses.Add(course);
                courses[i] = course;
            }

            for (int i = 0; i < Students.Length; i++)
            {
                Student student = new Student
                {
                    Name = Students[i]
                };

                dbContext.Students.Add(student);
                students[i] = student;
            }

            foreach (Student student in students)
            {
                foreach (Course course in courses)
                {
                    Enrollment enrollment = new Enrollment
                    {
                        Course = course,
                        Student = student,
                        Grade = Grades[rnd.Next(Grades.Length)],
                        score = scores[rnd.Next(scores.Length)]
                    };

                    dbContext.Enrollments.Add(enrollment);
                }
            }

            dbContext.SaveChanges();
        }
    }
}
