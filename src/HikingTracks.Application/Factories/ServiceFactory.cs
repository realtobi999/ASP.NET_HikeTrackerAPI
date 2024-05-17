using HikingTracks.Application.Interfaces;
using HikingTracks.Application.Service.Accounts;
using HikingTracks.Application.Service.Hikes;
using HikingTracks.Application.Service.Photos;
using HikingTracks.Application.Service.Segments;
using HikingTracks.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HikingTracks.Application.Factories;

public class ServiceFactory : IServiceFactory
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IConfiguration _configuration;

    public ServiceFactory(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _configuration = configuration;
    }  
    
    public IAccountService CreateAccountService()
    {
        return new AccountService(_repositoryManager, _loggerManager);
    }

    public IHikeService CreateHikeService()
    {
        return new HikeService(_repositoryManager, _loggerManager);
    }

    public IPhotoService CreatePhotoService()
    {
        return new PhotoService(_repositoryManager, _loggerManager);
    }

    public IFormFileService CreateFormFileService()
    {
        return new FormFileService();
    }

    public ISegmentService CreateSegmentService()
    {
        return new SegmentService(_repositoryManager, _loggerManager);
    }

    public ITokenService CreateTokenService()
    {
        var jwtIssuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = _configuration.GetSection("Jwt:Key").Get<string>();

        if (jwtIssuer is null)
        {
            throw new ArgumentNullException(nameof(jwtIssuer), "JWT Issuer configuration is missing");
        }
        else if (jwtKey is null)
        {
            throw new ArgumentNullException(nameof(jwtKey), "JWT Key configuration is missing");
        }

        return new TokenService(jwtIssuer, jwtKey);
    }

    public ISegmentHikeService CreateSegmentHikeService()
    {
        return new SegmentHikeService(_repositoryManager, _loggerManager);
    }
}
