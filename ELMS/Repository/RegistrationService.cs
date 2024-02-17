using ELearningWeb.Controllers;
using ELearningWeb.DbContexts;
using ELearningWeb.DTO;
using ELearningWeb.Helper;
using ELearningWeb.IRepository;
using ELearningWeb.Models.Write;

namespace ELearningWeb.Repository
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ReadDbContext _contextR;
        private readonly WriteDbContext _contextW;
        private readonly IEmailService _emailService;
        public RegistrationService(WriteDbContext contextW,ReadDbContext contextR, IEmailService emailService)
        {

            _contextW = contextW;
            _contextR = contextR;
            _emailService = emailService;

        }

        public async Task<MessageHelper> Registration(SignUpDTO obj)
        {
            try
            {
                if(obj.Email == null)
                {
                    throw new Exception("Email should not be null");
                }
                if(obj.Password == null)
                {
                    throw new Exception("Password must be given");
                }
                if (obj.Password.Length < 7)
                {
                    throw new Exception("Password length at least greater than six or equal ");
                }

                var user = new User
                {
                    UserName = obj.UserName,
                    Email = obj.Email,
                    Password = obj.Password,
                    ConfirmPassword = obj.ConfirmPassword,
                    Address = obj.Address,
                    Age = obj.Age,
                    Phone = obj.Phone,
                    UserRole = "Students",
                    CreateDate = DateTime.Now
                };

                await _contextW.Users.AddAsync(user);
                await _contextW.SaveChangesAsync();
                Random random = new Random();
                int randomNumber = random.Next(1000, 9999);
                string otpCode = randomNumber.ToString();


                var otpData = new UserOtp
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Otp = otpCode,
                    IsActive = true, 
                    IsUsed = false,
                    CreateDate = DateTime.Now

                };


                await _contextW.UserOtps.AddAsync(otpData);
                await _contextW.SaveChangesAsync();

                MailRequest info = new MailRequest();
                info.ToEmail = obj.Email;
                info.Subject = "Otp verfication code";
                info.Body = $"This is your otp verification code : {otpCode}. Please do not share with anyone";
                await _emailService.SendEmailAsync(info);

 
                return new MessageHelper
                {
                    Message = "Otp send successfully",
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MessageHelper> OtpVerify(OtpVerifyDTO obj)
        {
            try
            {
                if(obj == null)
                {
                    throw new Exception("Data not found");
                }

                var checkOtp = await Task.FromResult(_contextW.UserOtps.Where(y => y.Email == obj.Email && y.Otp == obj.OtpCode && y.IsUsed == false).FirstOrDefault());

                

                if (checkOtp == null)
                {
                    throw new Exception("Otp is not correct");
                }

                var otpData = await Task.FromResult(_contextW.UserOtps.Where(y => y.Email == obj.Email && y.Otp == obj.OtpCode).FirstOrDefault());

                otpData.IsUsed = true;
                _contextW.UserOtps.Update(otpData);
                await _contextW.SaveChangesAsync();

                return new MessageHelper
                {
                    Message = "Otp verfication is successfull",
                    StatusCode = 200
                };


            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
