using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public class ServiceManager(IMapper mapper, IUnitOfWork unitOfWork, SignInManager<User> signInManager, UserManager<User> userManager,
        IOptions<JwtOptions> options)
        : IServiceManager
        {
        public IAuthService AuthService => new AuthService(signInManager, userManager, options);

        public ICustomerService CustomerService => new CustomerService(unitOfWork, userManager);

        public IOrderService OrderService => new OrderService(unitOfWork);
        public IProductService ProductService => new ProductService(unitOfWork, mapper);

        public IInvoiceService InvoiceService => new InvoiceService(unitOfWork, mapper);
        }
    }
