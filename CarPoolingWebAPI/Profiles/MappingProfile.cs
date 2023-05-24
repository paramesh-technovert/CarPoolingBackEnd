using AutoMapper;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CarPoolingWebAPI.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<UserDetailsRequestDT,UserDetail>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
            CreateMap<UserDetail, UserDetailsRequestDT>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>GetImage( src.ImageUrl)));
            CreateMap<LoginCredentialsDT, LoginDetail>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailId.ToLower()))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<LoginDetail, LoginCredentialsResponseDT>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
            CreateMap<OfferedRideDT, OfferRide>()
                .ForMember(dest => dest.RideProviderId, opt => opt.MapFrom(src => src.RideOwnerId))
                .ForMember(dest => dest.Fair, opt => opt.MapFrom(src => src.Fair))
                .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.TotalSeats))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Stops, opt => opt.Ignore());
            CreateMap<BookRideRequestDT, BookRideResponseDT>()
                .ForMember(dest => dest.RideId, opt => opt.MapFrom(src => src.RideId))
                .ForMember(dest => dest.SeatsBooked, opt => opt.MapFrom(src => src.BookedSeats))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateTime));
            CreateMap<OfferRide, BookRideResponseDT>()
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.RideProviderId))
                .ForMember(dest => dest.Fair, opt => opt.MapFrom(src => src.Fair));
            CreateMap<BookRideRequestDT, BookRideRequestDBO>().ReverseMap();
            CreateMap<BookRideRequestDTO, BookRideRequestDBO>().ReverseMap();
            CreateMap<MatchedRidesRequestDT, MatchedRidesRequestDBO>().ReverseMap();
            CreateMap<BookedRidesDBO, BookedRidesDT>();
            CreateMap<OfferedRideDBO, OfferedRideDT>();
            CreateMap<OfferedRideDTO, OfferedRideDT>();
            CreateMap<OfferedRidesDT, OfferedRideDTO>();
            CreateMap<OfferedRidesDBO, OfferedRidesDT>();
            CreateMap<OfferedRidesDT, OfferedRidesDTO>();
            CreateMap<StopsDTO,StopsDT>().ReverseMap();
            CreateMap<UserDetailsRequestDTO, UserDetailsRequestDT>().ReverseMap();
            CreateMap<BookedRidesDBO, BookedRidesDTO>();
            CreateMap<MatchedRidesResponseDBO, MatchedRidesResponseDT>();
            CreateMap<LoginCredentialsDTO, LoginCredentialsDT>();
            CreateMap<BookRideRequestDTO, BookRideRequestDT>().ReverseMap();
            CreateMap<BookRideResponseDT, BookRideResponseDTO>();
            CreateMap<LoginCredentialsResponseDT, LoginCredentialsResponseDTO>();
            CreateMap<MatchedRidesRequestDTO, MatchedRidesRequestDT>();
            CreateMap<MatchedRidesResponseDT, MatchedRidesResponseDTO>();
        }
        public string GetImage(string path)
        {
            byte[] imagePath = System.IO.File.ReadAllBytes(path);
            string base64String = "data:image/png;base64," + Convert.ToBase64String(imagePath);
            return base64String;
        }
    }
}
