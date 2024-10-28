using AutoMapper;
using XYZUniversity.API.Models.Domain; // Update this namespace based on your project structure
using XYZUniversity.API.Models.DTO; // Update this namespace based on your project structure

namespace XYZUniversity.API.Mappings // Update this namespace based on your project structure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping configurations for Student
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<AddStudentRequestDto, Student>().ReverseMap();
            CreateMap<UpdateStudentRequestDto, Student>().ReverseMap();
            CreateMap<AddPaymentRequestDto, Payment>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<UpdatePaymentRequestDto, Payment>().ReverseMap();

            // Mapping configurations for Payment
            /*           CreateMap<Payment, PaymentDto>().ReverseMap();
                       CreateMap<AddPaymentRequestDto, Payment>().ReverseMap();
                       CreateMap<UpdatePaymentRequestDto, Payment>().ReverseMap();*/
        }
    }
}
