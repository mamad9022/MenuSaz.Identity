using AutoMapper;
using MediatR;
using MenuSaz.Identity.Application.Exception;
using MenuSaz.Identity.Application.Extensions;
using MenuSaz.Identity.Application.Feature.User.Login.Dtos;
using MenuSaz.Identity.Application.Repositories;
using MenuSaz.Identity.Application.Services;

namespace MenuSaz.Identity.Application.Feature.User.Login.Commad;
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
{
    public readonly IUserRepository _userRepository;
    public readonly IMapper _mapper;
    public readonly IJwtService _jwtService;
    public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }
    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync(x => x.Username == request.Username);

        if (user is null)
            throw new MenuSazException(Domain.Common.ErrorCode.BadRequest, ResponseExtension.GetMessage(x => x.UsernameNotValid));

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new MenuSazException(Domain.Common.ErrorCode.BadRequest, ResponseExtension.GetMessage(x => x.PasswordWrong));

        return new LoginDto(await _jwtService.GenerateJwtToken(user));

    }
}
