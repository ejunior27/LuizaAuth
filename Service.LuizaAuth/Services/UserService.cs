using AutoMapper;
using Domain.LuizaAuth.DTOs;
using Domain.LuizaAuth.Entities;
using Domain.LuizaAuth.Exceptions;
using Domain.LuizaAuth.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Service.LuizaAuth.Services
{
    public class UserService : IUserService
    {
        private const string MSG_USUARIO_NAO_ENCONTRADO = "Usuário não encontrado";
        private const string TEMP_PASSWORD = "Senha@123";
        private const string RECOVERY_PASSWORD_BODY = "Utilize a senha {0} para acessar o sistema. Altere-a o mais breve possível.";
        private const string RECOVERY_PASSWORD_SUBJECT = "Recuperação de senha";
        private const string CREATE_USER_BODY = "Seu usuário foi criado com sucesso!";
        private const string CREATE_USER_SUBJECT = "Bem vindo(a), {0}";

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        public async Task<List<UserDto>> GetAsync()
        {
            var _users = await userRepository.GetAll();

            var _userDTOs = mapper.Map<List<UserDto>>(_users);

            return _userDTOs;
        }        

        public async Task<bool> PostAsync(UserDto userViewModel)
        {
            Validator.ValidateObject(userViewModel, new ValidationContext(userViewModel), true);

            var user = mapper.Map<User>(userViewModel);
            user.Password = EncryptPassword(user.Password);

            await userRepository.CreateAsync(user);

            var bodyEmail = CREATE_USER_BODY;
            var subjectEmail = string.Format(CREATE_USER_SUBJECT, user.Name.Split(" ")[0]);

            await emailService.SendEmail(mapper.Map<User, UserDto>(user), subjectEmail, bodyEmail);

            return true;
        }              
                
        public async Task<UserAuthenticateResponseDto> AuthenticateAsync(UserAuthenticateRequestDto user)
        {
            user.Password = EncryptPassword(user.Password);

            var _user = await userRepository.FindAsync(x => x.Email.ToLower() == user.Email.ToLower()
                                                    && x.Password.ToLower() == user.Password.ToLower());
            if (_user == null)
                throw new BusinessException(MSG_USUARIO_NAO_ENCONTRADO);

            return new UserAuthenticateResponseDto { User = mapper.Map<UserDto>(_user) };
        }

        private string EncryptPassword(string senha)
        {
            var sha = new SHA1CryptoServiceProvider();

            var encryptedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));

            var stringBuilder = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        public async Task RecoveryPassword(RecoveryPasswordDto recoveryPasswordViewModel)
        {
            try
            {
                var user = await userRepository.FindAsync(x => x.Email.ToLower() == recoveryPasswordViewModel.Email.ToLower());

                if (user == null)
                    throw new BusinessException(MSG_USUARIO_NAO_ENCONTRADO);

                user.Password = EncryptPassword(TEMP_PASSWORD);

                await userRepository.UpdateAsync(user);

                var bodyEmail = string.Format(RECOVERY_PASSWORD_BODY, TEMP_PASSWORD);
                var subjectEmail = RECOVERY_PASSWORD_SUBJECT;

                await emailService.SendEmail(mapper.Map<User, UserDto>(user), subjectEmail, bodyEmail);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
