﻿using BoxOffice.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using BoxOffice.Models;
using System.Web.Mvc;
using System.Linq;

namespace Unit_Tests
{
    
    
    /// <summary>
    ///This is a test class for MoviesControllerTest and is intended
    ///to contain all MoviesControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MoviesControllerTest
    {
        

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        public static void MyClassCleanup()
        {
            BoxOfficeContext db = new BoxOfficeContext();
            db.Movies.Remove(db.Movies.Find(db.Movies.Where(s => s.Name == "Kick-Ass")));
            
        }
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddMovie
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        [UrlToTest("http://localhost:2583")]
        public void AddMovieTest()
        {
            MoviesController target = new MoviesController(); // TODO: Initialize to an appropriate value
            AddMovieModel add = new AddMovieModel
            {
                Name = "Kick-Ass",
                DVDs = 5,
                Price = 5.0M
            }; // TODO: Initialize to an appropriate value
            Movie expected = new Movie
                {
                    Name = "Kick-Ass"
                }; // TODO: Initialize to an appropriate value
            Movie actual;
            actual = target.AddMovieTest(add);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}