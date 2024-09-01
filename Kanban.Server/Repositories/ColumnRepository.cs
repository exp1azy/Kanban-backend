using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class ColumnRepository(DataContext dataContext) : IColumnRepository
    { 
        private readonly DataContext _dataContext = dataContext;

        public async Task AddColumnAsync(ColumnClientCreateModel column, CancellationToken cancellationToken = default)
        {
            await _dataContext.Columns.AddAsync(new Column
            {
                BoardId = column.BoardId,
                Name = column.Name,
                Position = column.Position
            }, cancellationToken);

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Column?> UpdateColumnNameAsync(ColumnClientUpdateNameModel column, CancellationToken cancellationToken = default)
        {
            var columnToUpdate = await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == column.Id, cancellationToken);
            columnToUpdate.Name = column.Name;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return columnToUpdate;
        }

        public async Task<Column?> UpdateColumnPositionAsync(ColumnClientUpdatePositionModel column, CancellationToken cancellationToken = default)
        {
            var columnToUpdate = await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == column.Id, cancellationToken);
            columnToUpdate.Position = column.Position;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return columnToUpdate;
        }

        public async Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            var columnToDelete = await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            _dataContext.Columns.Remove(columnToDelete);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
