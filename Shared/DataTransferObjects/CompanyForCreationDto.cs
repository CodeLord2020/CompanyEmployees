

namespace Shared.DataTransferObjects {
    public record CompanyForCreationDto : CompanyManipulationDto
    {
        public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
    }

}