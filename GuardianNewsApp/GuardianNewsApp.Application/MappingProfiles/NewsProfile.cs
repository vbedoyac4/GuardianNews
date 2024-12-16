using AutoMapper;
using GuardianNewsApp.Domain.Entities;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<NewsArticle, News>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.WebPublicationDate))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.SectionName))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.WebTitle))
            .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.WebUrl));
    }
}
