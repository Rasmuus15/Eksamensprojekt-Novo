using Microsoft.VisualStudio.TestTools.UnitTesting;
using NovoForecastingSystem;
using NovoForecastingSystem.Models;
using NovoForecastingSystem.Models.Enums;
using System;

namespace ProjectTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void EndDateShouldBeCorrectForLowComplexity()
        {
            //Instantier startDato og project, med data for low complexity
            //Arrange 
            DateOnly startDate = new DateOnly(2026, 7, 12);

            Project projectLow = new Project
            {
                StartDate = startDate,
                ComplexityEnum = Complexity.Low
                
            };

            //Hent/kør kode (EndDate) der skal bruges til sammenligning
            //Act
            DateOnly resultLow = projectLow.EndDate;
           

            //Sammenlign/test om EndDate stemmer overens, når complexity er low
            //Assert
            DateOnly expected = startDate.AddDays(81 * 7);
            Assert.AreEqual(expected, resultLow);
        }

        [TestMethod]
        public void EndDateShouldBeCorrectForMediumComplexity()
        {

            //Instantier startDato og project, med data for medium complexity
            //Arrange 
            DateOnly startDate = new DateOnly(2029, 9, 7);

            Project projectMedium = new Project
            {
                StartDate = startDate,
                ComplexityEnum = Complexity.Medium
            };

            //Hent/kør kode (EndDate) der skal bruges til sammenligning
            //Act
            DateOnly resultMedium = projectMedium.EndDate;


            //Sammenlign/test om EndDate stemmer overens, når complexity er medium
            //Assert
            DateOnly expected = startDate.AddDays(108 * 7);
            Assert.AreEqual(expected, resultMedium);
        }

        [TestMethod]
        public void EndDateShouldBeCorrectForHighComplexity()
        {

            //Instantier startDato og project, med data for high complexity
            //Arrange 
            DateOnly startDate = new DateOnly(2027, 3, 11);

            Project projectHigh = new Project
            {
                StartDate = startDate,
                ComplexityEnum = Complexity.High
            };

            //Hent/kør kode (EndDate) der skal bruges til sammenligning
            //Act
            DateOnly resultHigh = projectHigh.EndDate;


            //Sammenlign/test om EndDate stemmer overens, når complexity er high
            //Assert
            DateOnly expected = startDate.AddDays(137 * 7);
            Assert.AreEqual(expected, resultHigh);
        }
    }
}