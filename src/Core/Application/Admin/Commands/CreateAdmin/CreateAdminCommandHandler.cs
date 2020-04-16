﻿namespace Application.Admin.Commands.CreateAdmin
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Common.Interfaces;
    using MediatR;

    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand>
    {
        private readonly IUserManager userManager;

        public CreateAdminCommandHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            //TODO: Add User as default role in db
            if (!request.Role.Equals(AppConstants.AdministratorRole))
            {
                throw new BadRequestException("Invalid role");
            }

            var result = await this.userManager.AddToRoleAsync(request.Email, request.Role);
            if (!result)
            {
                throw new BadRequestException($"Something went wrong while adding user to {request.Role} role");
            }

            return Unit.Value;
        }
    }
}