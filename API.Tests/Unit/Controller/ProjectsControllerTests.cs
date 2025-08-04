using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests.Unit.Controller;

[TestClass]
public class ProjectsControllerTests : UnitTestClassBase
{
    [TestMethod]
    public async Task GetAllProjects_ReturnsAllProjects()
    {
        // Arrange
        var projectDtos = new List<ProjectDto>
        {
            new ProjectDto { Id = 1, Name = "Unit Test Project 1", Status = "ToDo" },
            new ProjectDto { Id = 2, Name = "Unit Test Project 2", Status = "InProgress" }
        };

        SetupGetAllAsync(projectDtos);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);

        var returnedProjects = okResult.Value as IEnumerable<ProjectDto>;
        Assert.IsNotNull(returnedProjects);
        Assert.AreEqual(2, returnedProjects.Count());
    }
}