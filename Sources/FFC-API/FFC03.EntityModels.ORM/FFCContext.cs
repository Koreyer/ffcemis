using FFC01.Foundation.Data;
using FFC01.Foundation.Tools;
using FFC02.EntityModels.E01.AppIdentity;
using FFC02.EntityModels.E02.AddressBusiness;
using FFC02.EntityModels.E03.CommodityBusiness;
using FFC02.EntityModels.E04.OrderBusiness;
using FFC02.EntityModels.E05.ShoppingCarBusiness;
using FFC02.EntityModels.E06.MessageManagementBusiness;
using FFC02.EntityModels.E07.SystemBusiness;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FFC03.EntityModels.ORM
{
    public class FFCContext: IdentityDbContext<AppUser,AppRole, Guid>
    {
        public FFCContext(DbContextOptions<FFCContext> options) : base(options) { }
        public FFCContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var commodityType = modelBuilder.Entity<CommodityType>();
            //commodityType.HasOne(x => x.Parent).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Order>().HasOne(x => x.AppUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<AppUser>().HasOne(x => x.ShoppingCar).WithMany().OnDelete(DeleteBehavior.Cascade);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                if (typeof(Data<Guid>).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
            base.OnModelCreating(modelBuilder);




        }

        //public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCar> ShoppingCars { get; set; }
        public DbSet<ShoppingCarWithItem> ShoppingWithItems { get; set; }
        public DbSet<HomePageCarousel> HomePageCarousels { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<MessageItem> MessageItems { get; set; }
        public DbSet<CommodityEvaluation> CommodityEvaluations { get; set; }

    }
}
