using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface IColumnRepository
    {
        public Task<IEnumerable<Column>> GetAllColumnsAsync(int boardId, CancellationToken cancellationToken = default);

        public Task AddColumnAsync(ColumnClientCreateModel column, CancellationToken cancellationToken = default);

        public Task<Column?> UpdateColumnAsync(ColumnClientUpdateModel column, CancellationToken cancellationToken = default);

        public Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default);
    }
}
