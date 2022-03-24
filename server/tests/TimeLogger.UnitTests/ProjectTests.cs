using System;
using System.Globalization;
using Timelogger.Domain;
using Xunit;

namespace TimeLogger.UnitTests
{
    public class ProjectsTests
    {
        [Fact]
        public void Add_TimeLog_to_Project()
        {
            // Arrange
            var name = "E-commerce Project";
            var deadline = DateTime.ParseExact("20220531", "yyyyMMdd", CultureInfo.InvariantCulture);
            var project = new Project(name, deadline);
            var timeLogDescription = "Initial Interviews";
            var timeLogDuration = 50;

            // Act
            var sut = project.AddTimeLog(timeLogDescription, timeLogDuration);

            // Assert
            Assert.Equal(timeLogDescription, sut.Description);
        }
    }
}
