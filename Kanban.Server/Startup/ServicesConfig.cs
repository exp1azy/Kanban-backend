using Kanban.Server.Repositories;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Services;
using Kanban.Server.Services.Interfaces;

namespace Kanban.Server.Startup
{
    public static class ServicesConfig
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IBoardRepository, BoardRepository>();
            builder.Services.AddTransient<IColumnRepository, ColumnRepository>();
            builder.Services.AddTransient<ICardRepository, CardRepository>();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IKanbanService, KanbanService>();
        }
    }
}
