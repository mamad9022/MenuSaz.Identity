using AutoMapper;
using MediatR;
using MenuSaz.Identity.Application.Exception;
using MenuSaz.Identity.Application.Extensions;
using MenuSaz.Identity.Application.Feature.User.Dtos;
using MenuSaz.Identity.Application.UnitOfWork;

namespace MenuSaz.Identity.Application.Feature.User.Command
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.User.AnyAsync(x => x.Username == request.Username))
                throw new MenuSazException(Domain.Common.ErrorCode.Duplicated, ResponseExtension.GetMessage(x => x.Duplicated));

            var user = new Domain.Models.User(
                request.Username,
                request.Firstname,
                request.Lastname,
                BCrypt.Net.BCrypt.HashPassword(request.Password),
                request.Phonenumber,
                false);

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<UserDto>(user);
        }
    }
}
