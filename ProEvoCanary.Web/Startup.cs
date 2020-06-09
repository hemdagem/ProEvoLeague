﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProEvoCanary.DataAccess;
using ProEvoCanary.DataAccess.Helpers;
using ProEvoCanary.DataAccess.Repositories;
using ProEvoCanary.DataAccess.Repositories.Interfaces;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Models;

namespace ProEvoCanary.Web
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Default}/{action=Index}/{id?}");
			});
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();

			services.AddTransient<ICacheManager, CacheManager>();
			services.AddTransient<IDBConfiguration, DbConfiguration>();
			services.AddTransient<IDbHelper, DbHelper>();

			services.AddTransient<IRssLoader, RssLoader>();
			services.AddTransient<IXmlGenerator, XmlGenerator>();

			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<IResultRepository, ResultsRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IEventWriteRepository, EventWriteRepository>();
			services.AddTransient<IEventReadRepository, EventReadRepository>();

			//Auto mapper
			var mapperConfiguration = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<PlayerModel, Models.PlayerModel>();
				cfg.CreateMap<ResultsModel, Models.ResultsModel>();
				cfg.CreateMap<TournamentType, Models.TournamentType>();
				cfg.CreateMap<Standings, Models.Standings>();
			});

			services.AddTransient<IMapper>(x => mapperConfiguration.CreateMapper());


		}
		
	}
}