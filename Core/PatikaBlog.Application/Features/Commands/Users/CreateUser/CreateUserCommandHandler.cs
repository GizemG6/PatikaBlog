using MediatR;
using Microsoft.AspNetCore.Identity;
using PatikaBlog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatikaBlog.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            var response = new CreateUserCommandResponse();

            if (result.Succeeded)
            {
                var roleExist = await _roleManager.RoleExistsAsync(request.RoleName);
                if (roleExist)
                {
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, request.RoleName);
                    if (roleResult.Succeeded)
                    {
                        response.Successed = true;
                        response.Message = "The user has been successfully created and assigned to the role.";
                    }
                    else
                    {
                        response.Successed = false;
                        response.Message = "The user was created but not assigned to the role.";
                        foreach (var error in roleResult.Errors)
                        {
                            response.Message += $"{error.Code} - {error.Description}<br>";
                        }
                    }
                }
                else
                {
                    response.Successed = false;
                    response.Message = $"Role '{request.RoleName}' does not exist.";
                }
            }
            else
            {
                response.Successed = false;
                response.Message = "Failed to create user. Errors:";
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}<br>";
                }
            }

            return response;
        }
    }
}
