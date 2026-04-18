using System.Collections.Generic;
using cs330_proj1;

namespace CourseProject.Tests;

public class CourseServicesTests
{
    // ==========================================
    // USER STORY 2: Get All Available Courses
    // ==========================================
    
    [Fact]
    public void GetCourses_ReturnsAllCourses()
    {
        // Arrange
        CourseServices service = new CourseServices();

        // Act
        List<Course> courses = service.GetCourses();

        // Assert
        Assert.NotNull(courses);
        Assert.Equal(4, courses.Count); // Repository has 4 courses
    }

    [Fact]
    public void GetCourses_ContainsExpectedCourse()
    {
        // Arrange
        CourseServices service = new CourseServices();

        // Act
        List<Course> courses = service.GetCourses();

        // Assert
        Assert.Contains(courses, c => c.Name == "ARTD 201");
        Assert.Contains(courses, c => c.Name == "ARTS 101");
        Assert.Contains(courses, c => c.Name == "STAT 201");
        Assert.Contains(courses, c => c.Name == "ENGL 302");
    }

    [Fact]
    public void GetCourses_VerifyCourseDetails()
    {
        // Arrange
        CourseServices service = new CourseServices();

        // Act
        List<Course> courses = service.GetCourses();
        Course statCourse = courses.Find(c => c.Name == "STAT 201");

        // Assert
        Assert.NotNull(statCourse);
        Assert.Equal("stats", statCourse.Title);
        Assert.Equal(4.0, statCourse.Credits);
    }

    // ==========================================
    // USER STORY 3: Get Course Offerings by Semester
    // ==========================================

    [Fact]
    public void GetCourseOfferingsBySemester_Spring2021_ReturnsTwoOfferings()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2021";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemester(semester);

        // Assert
        Assert.NotNull(offerings);
        Assert.Equal(2, offerings.Count); // ARTD 201 and STAT 201 are offered Spring 2021
    }

    [Fact]
    public void GetCourseOfferingsBySemester_Spring2022_ReturnsOneOffering()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2022";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemester(semester);

        // Assert
        Assert.NotNull(offerings);
        Assert.Single(offerings); // Only ARTS 101 is offered Spring 2022
        Assert.Equal("ARTS 101", offerings[0].TheCourse.Name);
    }

    [Fact]
    public void GetCourseOfferingsBySemester_NonexistentSemester_ReturnsEmptyList()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Fall 2025";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemester(semester);

        // Assert
        Assert.NotNull(offerings);
        Assert.Empty(offerings);
    }

    [Fact]
    public void GetCourseOfferingsBySemester_VerifyCorrectCourses()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2021";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemester(semester);

        // Assert
        Assert.Contains(offerings, o => o.TheCourse.Name == "ARTD 201");
        Assert.Contains(offerings, o => o.TheCourse.Name == "STAT 201");
        Assert.DoesNotContain(offerings, o => o.TheCourse.Name == "ARTS 101"); // Offered in Spring 2022, not 2021
    }

    // ==========================================
    // USER STORY 4: Get Course Offerings by Semester and Department
    // ==========================================

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_Spring2021ART_ReturnsARTD201()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2021";
        string dept = "ART";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        // Assert
        Assert.NotNull(offerings);
        Assert.Single(offerings);
        Assert.Equal("ARTD 201", offerings[0].TheCourse.Name);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_Spring2021STAT_ReturnsSTAT201()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2021";
        string dept = "STAT";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        // Assert
        Assert.NotNull(offerings);
        Assert.Single(offerings);
        Assert.Equal("STAT 201", offerings[0].TheCourse.Name);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_Spring2022ART_ReturnsARTS101()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2022";
        string dept = "ART";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        // Assert
        Assert.NotNull(offerings);
        Assert.Single(offerings);
        Assert.Equal("ARTS 101", offerings[0].TheCourse.Name);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_NonexistentDepartment_ReturnsEmptyList()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2021";
        string dept = "MATH";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        // Assert
        Assert.NotNull(offerings);
        Assert.Empty(offerings);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_WrongSemester_ReturnsEmptyList()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Fall 2021";
        string dept = "ART";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        // Assert
        Assert.NotNull(offerings);
        Assert.Empty(offerings);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_VerifySectionInfo()
    {
        // Arrange
        CourseServices service = new CourseServices();
        string semester = "Spring 2021";
        string dept = "ARTD";

        // Act
        List<CourseOffering> offerings = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        // Assert
        Assert.NotNull(offerings);
        Assert.Single(offerings);
        Assert.Equal("D1", offerings[0].Section);
        Assert.Equal("Spring 2021", offerings[0].Semester);
    }
}