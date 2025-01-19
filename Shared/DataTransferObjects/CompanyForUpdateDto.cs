namespace Shared.DataTransferObjects{
        public record CompanyForUpdateDto : CompanyManipulationDto
        {
            public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
            
        }

}