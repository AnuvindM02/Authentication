using Authentication.Application.Features.CreateProfile;
using Authentication.Application.Features.GetAllUsers;
using Authentication.Application.Features.GetProfile;
using Authentication.Application.Features.UpdateProfile;
using Authentication.Application.RabbitMq.Models;
using Authentication.Domain.Entities;
using AutoMapper;
namespace Authentication.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProfileRequest, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Middlename, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, CreateProfileResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Middlename, opt => opt.MapFrom(src => src.Middlename))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, GetProfileResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Middlename, opt => opt.MapFrom(src => src.Middlename))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()));

            CreateMap<UpdateProfileRequest, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Middlename, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, UpdateProfileResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Middlename, opt => opt.MapFrom(src => src.Middlename))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, GetAllUsersDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Middlename))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, NewUserMessage>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Middlename))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Lastname));
        }
    }
}
