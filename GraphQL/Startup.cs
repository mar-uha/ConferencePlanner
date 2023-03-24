using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferencePlanner.GraphQL.Attendees;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using ConferencePlanner.GraphQL.Sessions;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConferencePlanner.GraphQL
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlite("Data Source=conferences.db"));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<AttendeeQueries>()
                    .AddTypeExtension<SessionQueries>()
                    .AddTypeExtension<SpeakerQueries>()
                    .AddTypeExtension<TrackQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AttendeeMutations>()
                    .AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<SpeakerMutations>()
                    .AddTypeExtension<TrackMutations>()
                .AddSubscriptionType(d => d.Name("Subscription"))
                    .AddTypeExtension<AttendeeSubscriptions>()
                    .AddTypeExtension<SessionSubscriptions>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddDataLoader<SpeakerByIdDataLoader>()
                .AddDataLoader<SessionByIdDataLoader>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}