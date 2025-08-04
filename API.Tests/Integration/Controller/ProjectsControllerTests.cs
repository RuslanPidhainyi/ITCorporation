using API.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace API.Tests.Integration.Controller;

[TestClass]
public class ProjectsControllerTests : IntegrationTestClassBase
{
    [TestMethod]
    public async Task CreateProject()
    {
        // Arrange
        var projectDto = new CreateUpdateProjectDto
        {
            Name = "Integration Test Project",
            Status = "ToDo"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/projects", projectDto);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ProjectDto>();
        Assert.IsNotNull(result);

        var dbProject = await _context.Projects.FindAsync(result!.Id);
        Assert.IsNotNull(dbProject);
        Assert.AreEqual(projectDto.Name, dbProject.Name);
        Assert.AreEqual(projectDto.Status, dbProject.Status);
    }

    [TestMethod]
    public async Task CreateProject_ShouldFail_WhenAssertionIsWrong()
    {
        // Arrange
        var projectDto = new CreateUpdateProjectDto
        {
            Name = "Integration Test Project",
            Status = "ToDo"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/projects", projectDto);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ProjectDto>();
        Assert.IsNotNull(result);

        var dbProject = await _context.Projects.FindAsync(result!.Id);
        Assert.IsNotNull(dbProject);

        Assert.AreEqual("WRONG NAME", dbProject.Name);
        Assert.AreEqual("WRONG STATUS", dbProject.Status);
    }
}