
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Interfaces.B2SCommonInterfaceRepositories;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.Interfaces.B2SSecurityInterfaceRepositories;
using Repository.Interfaces.B2STxnInterfaceRepositories;
using Repository.PublicClasses;
using Repository.PublicInterfaces;
using Repository.Repositories.B2SCommonRepositories;
using Repository.Repositories.B2SConfigRepositories;
using Repository.Repositories.B2SMainRepositories;
using Repository.Repositories.B2SSecurityRepositories;
using Repository.Repositories.B2STxnRepositories;
using Services.Implementations;

using Services.Interfaces;

using System.Linq;
using System.Text;
using Utilities;

namespace Framework
{
    public static class ServiceExtentions
    {
        /// <summary>
        /// Add Locally All Dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services, IConfiguration config)
        {
            #region DBContext
            services.AddSingleton<IB2SConfigDBContext>(s => new DBContext(config.GetConnectionString("B2SConfig")));
            services.AddSingleton<IB2STxnDBContext>(new DBContext(config.GetConnectionString("B2STxn")));
            services.AddSingleton<IB2SMainDBContext>(new DBContext(config.GetConnectionString("B2SMain")));
            services.AddSingleton<IB2SSecurityDBContext>(new DBContext(config.GetConnectionString("B2SSecurity")));
            services.AddSingleton<IB2SCommonDBContext>(new DBContext(config.GetConnectionString("B2SCommon")));

            #endregion


            #region B2SConfig 

            services.AddScoped(typeof(IB2SConfigRepository<>), typeof(B2SConfigRepository<>));

            services.AddScoped<INodeStateRepository, NodeStateRepository>();
            services.AddScoped<INodeRepository, NodeRepository>();
            services.AddScoped<IChannelInfoRepository, ChannelInfoRepository>();
            services.AddScoped<IChannelKeysRepository, ChannelKeysRepository>();
            services.AddScoped<IConnectorBrokerRepository, ConnectorBrokerRepository>();
            services.AddScoped<IMessageMapRepository, MessageMapRepository>();
            services.AddScoped<IModuleFormatterRepository, ModuleFormatterRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleServiceBrokerRepository, ModuleServiceBrokerRepository>();
            services.AddScoped<IModuleTPRepository, ModuleTPRepository>();
            services.AddScoped<IModuleValidatorRepository, ModuleValidatorRepository>();
            services.AddScoped<INodeBrokerRepository, NodeBrokerRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<ISwitchRepository, SwitchRepository>();
            services.AddScoped<IChannelBatchRepository, ChannelBatchRepository>();
            services.AddScoped<IEntityIDRepository, EntityIDRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskScheduleRepository, TaskScheduelRepository>();
            services.AddScoped<IFieldSelectRepository, FieldSelectRepository>();
            services.AddScoped<IRouteCardGroupRepository, RouteCardGroupRepository>();
            services.AddScoped<IRouteTerminalGroupRepository, RouteTerminalGroupRepository>();
            services.AddScoped<IFITRepository, FITRepository>();
            services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();

            #endregion

            #region B2STxn 

            services.AddScoped(typeof(IB2STxnRepository<>), typeof(B2STxnRepository<>));

            services.AddSingleton<ISingletoneSwitchRepositroy, SwitchRepository>();
            services.AddSingleton<ITxnRespHistoryRepository, TxnRespHistoryRepository>();
            services.AddSingleton<IVwTxnRevHistorySourceRepository, VwTxnRevHistorySourceRepository>();
            services.AddSingleton<ITxnByPosConditionRepository, TxnByPosConditionRepository>();


            #endregion


            #region B2SMain 

            services.AddScoped(typeof(IB2SMainRepository<>), typeof(B2SMainRepository<>));

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPrivilegeRepository, PrivilegeRepository>();
            services.AddScoped<IAcqCurrencyExchangeRepository, AcqCurrencyExchangeRepository>();
            services.AddSingleton<IBasicValueRepository, BasicValueRepository>();
            #endregion

            #region B2SSecurity 
            
            services.AddScoped(typeof(IB2SSecurityRepository<>), typeof(B2SSecurityRepository<>));

            services.AddScoped<IKeyStoreRepository, KeyStoreRepository>();
            #endregion

            #region B2SCommon
            services.AddScoped(typeof(IB2SCommonRepository<>),typeof(B2SCommonRepository<>));
            services.AddScoped<IISoCurrencyRepository, IsoCurrencyRepository>();
            #endregion

        }


        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                byte[] secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
                byte[] encryptKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptionKey);

                //Gets or sets if HTTPS is required for the metadata address or authority. The default is true. This should be disabled only in development environments.
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //There is a token validation parameter called ClockSkew, it gets or sets the clock skew to apply when validating a time.
                    //The default value of ClockSkew is 5 minutes. That means if you haven't set it, your token will be still valid for up to 5 minutes.
                    //If you want to expire your token on the exact time; you'd need to set ClockSkew to zero
                    ClockSkew = TimeSpan.FromMinutes(jwtSettings.ClockSkew),
                    RequireSignedTokens = true,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                    TokenDecryptionKey = new SymmetricSecurityKey(encryptKey)
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                            throw new Exception("Authentication Failed!");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                            throw new Exception("Authentication Failed!");

                        throw new Exception("You are unauthorized to access this resource.");
                    }
                };
            });


        }



    }
}