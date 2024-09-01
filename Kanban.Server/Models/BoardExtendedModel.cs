﻿using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class BoardExtendedModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public List<ColumnFullModel> Columns { get; set; }

        public static BoardExtendedModel? Map(Board board) => board == null ? null : new BoardExtendedModel
        {
            Id = board.Id,
            UserId = board.UserId,
            Name = board.Name,
            Created = board.Created,
            Columns = new List<ColumnFullModel>().AddRange(board.Columns.Select(ColumnFullModel.Map))
        };
    }
}