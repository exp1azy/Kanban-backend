using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface IColumnRepository
    {
        public Task AddColumnAsync(ColumnCreateClientModel columnModel, CancellationToken cancellationToken = default);

        public Task<Column> UpdateColumnAsync(ColumnUpdateClientModel columnModel, CancellationToken cancellationToken = default);

        public Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
    }
}
