using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Resources;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class ColumnRepository(DataContext dataContext) : IColumnRepository
    { 
        private readonly DataContext _dataContext = dataContext;

        public async Task AddColumnAsync(ColumnCreateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            var columnToCreate = new Column
            {
                BoardId = columnModel.BoardId,
                Name = columnModel.Name,
                Position = columnModel.Position
            };

            await _dataContext.Columns.AddAsync(columnToCreate, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            var columnToDelete = await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                ?? throw new ApplicationException(Error.ColumnNotFound);

            _dataContext.Columns.Remove(columnToDelete);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Column> UpdateColumnAsync(ColumnUpdateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            var columnToUpdate = await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == columnModel.Id, cancellationToken)
                ?? throw new ApplicationException(Error.ColumnNotFound);

            columnToUpdate.Name = columnModel.Name;
            columnToUpdate.Position = columnModel.Position;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return columnToUpdate;
        }
    }
}
