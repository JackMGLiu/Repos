using System.Data.Common;
using System.Data.SqlClient;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreRepository;
using NetCoreRepository.Impl;
using NetCoreRepository.Interface;
using NetCoreService;
using NetCoreService.DTO;
using NetCoreService.Impl;
using NetCoreService.Interface;
using NLog.Extensions.Logging;
using NLog.Web;
using UEditorNetCore;
using WebApplication1.Codes;
using WebApplication1.Model;

namespace WebApplication1
{
    public class Startup
    {
        //AutoMapper
        private MapperConfiguration _mapperConfiguration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
        }

        public IConfigurationRoot Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //配置文件
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //连接字符串
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            //AutoMapper
            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());
            //services.AddAutoMapper();

            //services.AddSingleton(typeof(ISqlHelper<>), typeof(SqlHelper<>));
            //services.AddSingleton<IDbConnection>(CreateDbConnection());

            //services.AddSingleton<IConfiguration>(Configuration);

            //注释业务处理模块，for sql server
            services.AddSingleton<ITestRepository>(new TestRepository(CreateDbConnection()));
            services.AddSingleton<IUserRepository>(new UserRepository(CreateDbConnection()));
            services.AddSingleton<ISysUserRepository>(new SysUserRepository(CreateDbConnection()));
            services.AddSingleton<ISysMenuRepository>(new SysMenuRepository(CreateDbConnection()));
            services.AddSingleton<IDictTypeRepository>(new DictTypeRepository(CreateDbConnection()));
            services.AddSingleton<IDictDetailRepository>(new DictDetailRepository(CreateDbConnection()));
            services.AddSingleton<ILogsRepository>(new LogsRepository(CreateDbConnection()));

            services.AddSingleton<ITestService, TestService>();
            services.AddSingleton<IUserService,UserService>();
            services.AddSingleton<ISysUserService, SysUserService>();
            services.AddSingleton<ISysMenuService, SysMenuService>();
            services.AddSingleton<IDictTypeService, DictTypeService>();
            services.AddSingleton<IDictDetailService, DictDetailService>();
            services.AddSingleton<ILogsService, LogsService>();

            //第一个参数为配置文件路径，默认为项目目录下config.json
            //第二个参数为是否缓存配置文件，默认false
            services.AddUEditorService();

            // Add framework services.
            //services.AddMvc();
            //处理json序列化后字母大小写问题
            services.AddMvc().AddJsonOptions(op =>
            {
                op.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
                op.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                //忽略循环引用
                //op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //Session服务
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // 添加NLog到.net core框架中
            loggerFactory.AddNLog();
            //添加NLog的中间件
            app.AddNLogWeb();
            // 指定NLog的配置文件
            env.ConfigureNLog("NLog.config");

            //app.UseRequestIP();//使用中间件
            app.LoginValidate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //Session
            app.UseSession();

            //cookie中间件需放置到UseMvc前 Microsoft.AspNetCore.Authentication.Cookies
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "member",  //认证方案：这是一个已知中间件的值，当有多个实例的中间件如果你想限制授权到一个实例时这个选项将会起作用。
                LoginPath = "/Account/Login",  //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径。
                //AccessDeniedPath = "", //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。
                AutomaticAuthenticate = true, //自动认证：这个标志表明中间件应该会在每个请求上进行验证和重建他创建的序列化主体。
                AutomaticChallenge = true //自动挑战：这个标志标明当中间件认证失败时应该重定向浏览器到登录路径或者禁止访问路径。
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }

        /// <summary>
        /// 创建SqlHelper
        /// </summary>
        /// <param name="settings">配置文件</param>
        /// <param name="connectionStrings">数据库连接字符串</param>
        /// <returns></returns>
        DbConnection CreateDbConnection()
        {
            //ISqlHelper sqlHelper = null;
            DbConnection connection = null;
            var dataBase = Configuration.GetSection("AppSettings")["DataBase"];
            //var defaultConnection = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            var defaultConnection = Configuration["ConnectionStrings:DefaultConnection"];


            //var dataBase = "sqlserver";
            //var defaultConnection = "server=.\\MSSQL2012;database=ssowebtest;uid=sa;pwd=sa2012LJ;";
            switch (dataBase)
            {
                case "msssql":
                    connection = new SqlConnection(defaultConnection);
                    break;
                case "sqlite":
                    connection = new SqliteConnection(defaultConnection);
                    break;
            }
            return connection;
        }
    }
}
