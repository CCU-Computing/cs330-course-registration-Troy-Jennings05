using System;
using System.Collections.Generic;

namespace cs330_proj1
{
   public class CourseServices
   {
      private CourseRepository repo = new CourseRepository();

      // USER STORY 1
      public List<CourseOffering> getOfferingsByGoalIdAndSemester(string theGoalId, string semester)
      {
         List<CoreGoal> theGoals = repo.Goals;
         List<CourseOffering> theOfferings = repo.Offerings;

         CoreGoal theGoal = null;

         foreach (CoreGoal cg in theGoals)
         {
            if (cg.Id.Equals(theGoalId))
            {
               theGoal = cg;
               break;
            }
         }

         if (theGoal == null) throw new Exception("Didn't find the goal");

         List<CourseOffering> results = new List<CourseOffering>();

         foreach (CourseOffering c in theOfferings)
         {
            if (c.Semester.Equals(semester) &&
                theGoal.Courses.Contains(c.TheCourse))
            {
               results.Add(c);
            }
         }

         return results;
      }

      // USER STORY 2
      public List<Course> GetCourses()
      {
         return repo.Courses;
      }

      // USER STORY 3
      public List<CourseOffering> getCourseOfferingsBySemester(string semester)
      {
         List<CourseOffering> results = new List<CourseOffering>();

         foreach (CourseOffering offering in repo.Offerings)
         {
            if (offering.Semester.Equals(semester))
            {
               results.Add(offering);
            }
         }

         return results;
      }

      
      public List<CourseOffering> getCourseOfferingsBySemesterAndDept(string semester, string dept)
      {
         List<CourseOffering> results = new List<CourseOffering>();

         foreach (CourseOffering offering in repo.Offerings)
         {
            if (offering.Semester.Equals(semester) &&
                offering.TheCourse.Name.StartsWith(dept))
            {
               results.Add(offering);
            }
         }

         return results;
      }

      
      public List<Course> getCoursesByGoalId(string goalId)
      {
         foreach (CoreGoal goal in repo.Goals)
         {
            if (goal.Id.Equals(goalId))
            {
               return goal.Courses;
            }
         }

         return new List<Course>();
      }

      
      public List<Course> getCoursesByGoalIds(string goalId1, string goalId2)
      {
         CoreGoal g1 = null;
         CoreGoal g2 = null;

         foreach (CoreGoal g in repo.Goals)
         {
            if (g.Id.Equals(goalId1)) g1 = g;
            if (g.Id.Equals(goalId2)) g2 = g;
         }

         List<Course> results = new List<Course>();

         if (g1 != null && g2 != null)
         {
            foreach (Course c in g1.Courses)
            {
               if (g2.Courses.Contains(c))
               {
                  results.Add(c);
               }
            }
         }

         return results;
      }

      
      public List<CoreGoal> getCoreGoalsThatAreNotCoveredBySemester(string semester)
      {
         List<CoreGoal> results = new List<CoreGoal>();

         foreach (CoreGoal goal in repo.Goals)
         {
            bool covered = false;

            foreach (CourseOffering offering in repo.Offerings)
            {
               if (offering.Semester.Equals(semester) &&
                   goal.Courses.Contains(offering.TheCourse))
               {
                  covered = true;
                  break;
               }
            }

            if (!covered)
            {
               results.Add(goal);
            }
         }

         return results;
      }
   }
}


        
        //Add more service functions here, as needed, for the project

      /* As a student, I want to see all available courses so that I know what my options are */

      /* As a student, I want to see all course offerings by semester, so that I can choose from what's
         available to register for next semester */

      /* As a student I want to see all course offerings by semester and department so that I can 
      choose major courses to register for */

      /* As a student I want to see all courses that meet a core goal, so that I can plan out
         my courses over the next few semesters and choose core courses that make sense for me */

      /* As a student I want to find a course that meets two different core goals, so that I can
      "feed two birds with one seed" (save time by taking one class that will fulfill two 
        requirements */

      /* As a freshman adviser, I want to see all the core goals which do not have any course offerings 
         for a given semester, so that I can work with departments to get some courses offered
         that students can take to meet those goals */


   

