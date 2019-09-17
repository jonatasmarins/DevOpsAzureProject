using AutoMapper;
using AzureDevOpsProject.Domain.Command;
using AzureDevOpsProject.Domain.Command.OutPut;
using AzureDevOpsProject.Domain.Entities;
using AzureDevOpsProject.Domain.ValueObjects;

namespace AzureDevOpsProject.CrossCutting.Config.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Request -> Entity

            CreateMap<CreateProjectCommand, Project>()
                .ForPath(dest => dest.Name, opt => opt.MapFrom(src => new Name(src.Name)))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => new Url(src.Url)));

            #endregion

            #region Entity -> Request

            CreateMap<Project, ProjectCommandResult>()
                .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Name.NameProject))
                .ForPath(dest => dest.Url, opt => opt.MapFrom(src => src.Url.UrlProject));

            #endregion
        }
    }
}
