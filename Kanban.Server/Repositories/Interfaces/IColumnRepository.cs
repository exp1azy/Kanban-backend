using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface IColumnRepository
    {
        public Task AddColumnAsync(ColumnClientCreateModel column, CancellationToken cancellationToken = default);

        public Task<Column?> UpdateColumnNameAsync(ColumnClientUpdateNameModel column, CancellationToken cancellationToken = default);

        public Task<Column?> UpdateColumnPositionAsync(ColumnClientUpdatePositionModel column, CancellationToken cancellationToken = default);

        public Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
    }
}
