using AutoMapper;
using Diary.Domain.Aggregates.User;
using Diary.Domain.Contracts;
using Diary.Infrastructure.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Application.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, TokenViewModel>
    {
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private readonly IRepository<User> userRepository;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly  AuthSettings authSettings;
        private readonly IMapper mapper;

        public AuthCommandHandler(IUnitOfWork uow,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IRepository<User> userRepository,
            IOptions<AuthSettings> authSettings,
            IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.authSettings = authSettings.Value;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<TokenViewModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (user == null)
            {
                user = new User
                {
                    PhoneNumber = request.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    UserName = request.PhoneNumber
                }; 
                var result = await this.userManager.CreateAsync(user, request.Password);
            }

            var principal = await this.signInManager.CreateUserPrincipalAsync(user);

            var claimsToken = principal.Claims.ToList();

            var newToken = new JwtSecurityToken(
                this.authSettings.Issuer,
                this.authSettings.Audience,
                claimsToken,
                this.authSettings.NotBefore,
                this.authSettings.Expiration,
                this.authSettings.SigningCredentials);
            var userModel = this.mapper.Map<TokenUserViewModel>(user);

            return new TokenViewModel {
                Token = this.tokenHandler.WriteToken(newToken),
                Message = "Ok",
                User = userModel
            };
        }
    }

    public class TokenUserViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserSecondName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
