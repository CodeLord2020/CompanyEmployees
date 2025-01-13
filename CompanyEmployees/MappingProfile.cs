
using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Company, CompanyDTO>().
            // ForCtorParam("FullAddress",
            CreateMap<Company, CompanyDTO>() .ForMember(c => c.FullAddress,
             opt => opt.MapFrom(
                x => string.Join(" ", x.Address, x.Country)
            ));

            CreateMap<Employee,  EmployeeDto>();
        }
        
    }
}