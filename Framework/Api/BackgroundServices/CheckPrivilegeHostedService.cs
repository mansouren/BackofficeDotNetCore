using Framework.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Services.BackgroundServices
{
    public class CheckPrivilegeHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public CheckPrivilegeHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var privilegeRepository = scope.ServiceProvider.GetRequiredService<IPrivilegeRepository>();
            var assembly = Assembly.GetEntryAssembly(); //Get Assembly
            
            List<Type> ControllerTypes = assembly.GetTypes()
                                        .Where(type => type.IsSubclassOf(typeof(ControllerBase)))
                                        .ToList();

            foreach (Type t in ControllerTypes)
            {

                Attribute[] attrs = Attribute.GetCustomAttributes(t);

                foreach (Attribute attr in attrs)
                {
                    if (attr is PrivilegeCheckerAttribute)
                    {
                        PrivilegeCheckerAttribute permissionAttr = (PrivilegeCheckerAttribute)attr;
                        var title = permissionAttr.Title;
                        var gid = permissionAttr.Gid;
                        await privilegeRepository.AddPrivilege(gid, title, null, cancellationToken);
                        var methodsWithCustomattr = assembly.GetTypes()
                        .SelectMany(t => t.GetMethods())
                        .Where(m => m.GetCustomAttributes(typeof(PrivilegeCheckerAttribute), false).Length > 0)
                        .ToArray();

                        foreach (var method in methodsWithCustomattr)
                        {
                            PrivilegeCheckerAttribute attribute = (PrivilegeCheckerAttribute)method.GetCustomAttribute(typeof(PrivilegeCheckerAttribute));
                            await privilegeRepository.AddPrivilege(attribute.Gid, attribute.Title, gid, cancellationToken);
                        }
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
