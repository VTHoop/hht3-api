using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class HhtDbContext : DbContext
    {
        public HhtDbContext(DbContextOptions<HhtDbContext> options) : base(options) { }

        public DbSet<AlertDto> Alerts { get; set; }
        public DbSet<GameDto> Games { get; set; }
        public DbSet<GameFoodDto> GameFoods { get; set; }
        public DbSet<GamePreviewDto> GamePreviews { get; set; }
        public DbSet<HaveTicketDto> HaveTickets { get; set; }
        public DbSet<LocationDto> Locations { get; set; }
        public DbSet<TeamDto> Teams { get; set; }
        public DbSet<TeamYearDto> TeamYear { get; set; }
        public DbSet<UserDto> User { get; set; }
        public DbSet<UserGameDto> UserGame { get; set; }
        public DbSet<WantTicketDto> WantTickets { get; set; }
        public DbSet<YearDto> Years { get; set; }

    }
}
