
using System.Net;
using System.Net.Http;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        // Known issue: this test can fail when running with all tests but passes on its own
        public void GetByEmployeeId_ThreeLevels_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            int expectedNumberOfReports = 5;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            var employee = reportingStructure.employee;
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.numberOfReports);
        }

        [TestMethod]
        // Known issue: this test can fail when running with all tests but passes on its own
        public void GetByEmployeeId_TwoLevels_Returns_Ok()
        {
            // Arrange
            var employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f";
            int expectedNumberOfReports = 3;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            var employee = reportingStructure.employee;
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.numberOfReports);
        }

        [TestMethod]
        // Known issue: this test can fail when running with all tests but passes on its own
        public void GetByEmployeeId_OneLevel_Returns_Ok()
        {
            // Arrange
            var employeeId = "c0c2293d-16bd-4603-8e08-638a9d18b22c";
            int expectedNumberOfReports = 1;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            var employee = reportingStructure.employee;
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.numberOfReports);
        }

        [TestMethod]
        // Known issue: this test can fail when running with all tests but passes on its own
        public void GetByEmployeeId_NoLevels_Returns_Ok()
        {
            // Arrange
            var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";
            int expectedNumberOfReports = 0;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            var employee = reportingStructure.employee;
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.numberOfReports);
        }
    }
}
