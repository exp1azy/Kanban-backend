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
            var isThereColumnWithSamePosition = await _dataContext.Columns.AnyAsync(c =>
                c.BoardId == columnModel.BoardId 
                && c.Position == columnModel.Position, cancellationToken);

            if (isThereColumnWithSamePosition)
                throw new ApplicationException(Error.ColumnWithSpecifiedPositionAlreadyExist);

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
            using var trans = await _dataContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var columnToDelete = await _dataContext.Columns
                    .Include(c => c.Cards)
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                    ?? throw new ApplicationException(Error.ColumnNotFound);

                if (columnToDelete.Cards.Count != 0)
                    _dataContext.Cards.RemoveRange(columnToDelete.Cards);

                _dataContext.Columns.Remove(columnToDelete);
                await _dataContext.SaveChangesAsync(cancellationToken);

                await trans.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await trans.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Column?> GetColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Column> UpdateColumnAsync(ColumnUpdateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            var columnToUpdate = await _dataContext.Columns.FirstOrDefaultAsync(c => c.Id == columnModel.Id, cancellationToken)
                ?? throw new ApplicationException(Error.ColumnNotFound);

            var isThereColumnWithSamePosition = await _dataContext.Columns.AnyAsync(c =>
                c.BoardId == columnToUpdate.BoardId
                && c.Position == columnModel.Position
                && c.Id != columnModel.Id, cancellationToken);

            if (isThereColumnWithSamePosition)
                throw new ApplicationException(Error.ColumnWithSpecifiedPositionAlreadyExist);

            columnToUpdate.Name = columnModel.Name;
            columnToUpdate.Position = columnModel.Position;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return columnToUpdate;
        }
    }
}
