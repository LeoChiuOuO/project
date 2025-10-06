using AutoMapper;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Models.Profiles;
public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report, ReportDTO>()
            .ForMember(dest => dest.TestItemName, opt => opt.MapFrom(src => src.TestItem.name));
    }
}
