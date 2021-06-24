using Domain.LuizaAuth.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Web.LuizaAuth.Configurations;
using Web.LuizaAuth.Interfaces;

namespace Web.LuizaAuth.Services.Api
{
    public class UserConsumerService : IUserConsumerService
    {
        private const string MSG_ERRO_LOGIN = "Não foi possível realizar o login";
        private const string MSG_ERRO_CADASTRO = "Não foi possível realizar o cadastro";
        private const string MSG_ERRO_RECUPERACAOSENHA = "Não foi possível recuperar a senha";

        private const string ENDPOINT_LOGIN = "/api/user/authenticate";
        private const string ENDPOINT_CADASTRO = "/api/user/create";
        private const string ENDPOINT_RECUPERACAOSENHA = "/api/user/recoverypassword";

        private readonly AppSettings appSettings;

        public UserConsumerService(IOptions<AppSettings> options)
        {
            this.appSettings = options.Value;
        }        

        public async Task<UserAuthenticateResponseDto> LoginAsync(UserAuthenticateRequestDto user)
        {
            try
            {
                var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, appSettings.BaseUrlApi + ENDPOINT_LOGIN)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var userAuth = JsonConvert.DeserializeObject<UserAuthenticateResponseDto>(content);
                    return userAuth;
                }                
                throw new Exception(content);
            }
            catch (Exception ex)
            {
                throw new Exception(MSG_ERRO_LOGIN, ex);
            }
        }

        public async Task<bool> CreateUserAsync(UserDto user)
        {
            try
            {
                var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, appSettings.BaseUrlApi + ENDPOINT_CADASTRO)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<bool>(content);
                }
                throw new Exception(content);
            }
            catch (Exception ex)
            {
                throw new Exception(MSG_ERRO_CADASTRO, ex);
            }
        }

        public async Task RecoveryPasswordAsync(RecoveryPasswordDto recoveryPasswordDto)
        {
            try
            {
                var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, appSettings.BaseUrlApi + ENDPOINT_RECUPERACAOSENHA)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(recoveryPasswordDto), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var response = await httpClient.SendAsync(request);                
            }
            catch (Exception ex)
            {
                throw new Exception(MSG_ERRO_RECUPERACAOSENHA, ex);
            }
        }
    }
}
