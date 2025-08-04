using API.Controllers;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Moq;

namespace API.Tests.Unit;

public abstract class UnitTestClassBase
{
    //INFO: Mock repositories and Mapper for unit tests, also controller has mock dependencies in the constructor
    protected Mock<IProjectRepository> projectRepoMock = null!;
    protected Mock<IEmployeeRepository> employeeRepoMock = null!;
    protected Mock<IMapper> mapperMock = null!;
    protected ProjectsController controller = null!;

    [TestInitialize]
    public void SetUp()
    {
        projectRepoMock = new Mock<IProjectRepository>();
        employeeRepoMock = new Mock<IEmployeeRepository>();
        mapperMock = new Mock<IMapper>();

        controller = new ProjectsController(
            projectRepoMock.Object,
            employeeRepoMock.Object,
            mapperMock.Object
        );
    }

    //INFO: Configuring mock behavior for GetAllAsync — returning a prepared list of DTOs
    protected void SetupGetAllAsync(IEnumerable<ProjectDto> result)
    {
        projectRepoMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(result);

        mapperMock
            .Setup(m => m.Map<IEnumerable<ProjectDto>>(It.IsAny<object>()))
            .Returns(result);
    }
}