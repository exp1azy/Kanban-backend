﻿using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanban.Server.Data
{
    [Table("board")]
    public class Board
    {
        [Key][Column("id")] public int Id { get; set; }

        [Column("user_id")] public int UserId { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("created")] public DateTime Created { get; set; }

        public ICollection<Column> Columns { get; set; }
    }
}