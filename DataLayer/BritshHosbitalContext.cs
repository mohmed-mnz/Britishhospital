using Microsoft.EntityFrameworkCore;

namespace Models.Models;

public partial class BritshHosbitalContext : DbContext
{
    public BritshHosbitalContext(DbContextOptions<BritshHosbitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advertisment> Advertisment { get; set; }

    public virtual DbSet<BookingSettingOrg> BookingSettingOrg { get; set; }

    public virtual DbSet<Citizen> Citizen { get; set; }


    public virtual DbSet<CounterServices> CounterServices { get; set; }

    public virtual DbSet<Counters> Counters { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<Display> Display { get; set; }

    public virtual DbSet<DisplayAdverts> DisplayAdverts { get; set; }

    public virtual DbSet<DisplayCounters> DisplayCounters { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<Group> Group { get; set; }

    public virtual DbSet<GroupUser> GroupUser { get; set; }

    public virtual DbSet<Organization> Organization { get; set; }

    public virtual DbSet<Reservations> Reservations { get; set; }

    public virtual DbSet<Service> Service { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        modelBuilder.Entity<Advertisment>(entity =>
        {
            entity.Property(e => e.AdvertName).HasMaxLength(50);
            entity.Property(e => e.Mediatype).HasColumnName("mediatype");
        });

        modelBuilder.Entity<BookingSettingOrg>(entity =>
        {
            entity.Property(e => e.EndWorkingHour).HasPrecision(0);
            entity.Property(e => e.KioskClosingTime).HasPrecision(0);
            entity.Property(e => e.StartWorkingHour).HasPrecision(0);

            entity.HasOne(d => d.Org).WithMany(p => p.BookingSettingOrg)
                .HasForeignKey(d => d.OrgId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BookingSettingOrg_Organization");
        });

        modelBuilder.Entity<Citizen>(entity =>
        {
            entity.Property(e => e.Mobile).HasMaxLength(11);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Nid).HasMaxLength(14);
            entity.Property(e => e.Sex).HasColumnName("sex");
        });



        modelBuilder.Entity<CounterServices>(entity =>
        {
            entity.HasOne(d => d.Counter).WithMany(p => p.CounterServices)
                .HasForeignKey(d => d.CounterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CounterServices_Counters");

            entity.HasOne(d => d.Service).WithMany(p => p.CounterServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CounterServices_Service");
        });

        modelBuilder.Entity<Counters>(entity =>
        {
            entity.HasKey(e => e.CounterId);

            entity.Property(e => e.CounterName).HasMaxLength(50);

            entity.HasOne(d => d.Emp).WithMany(p => p.Counter)
            .HasForeignKey(d => d.empid)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Counter_employee");


            entity.HasOne(d => d.Org).WithMany(p => p.Counters)
                .HasForeignKey(d => d.Orgid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Counters_Organization");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Citizenid).HasColumnName("citizenid");
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Citizen).WithMany(p => p.Customer)
                .HasForeignKey(d => d.Citizenid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Customer_Citizen");
        });

        modelBuilder.Entity<Display>(entity =>
        {
            entity.Property(e => e.DisplayName).HasMaxLength(50);

            entity.HasOne(d => d.Org).WithMany(p => p.Display)
                .HasForeignKey(d => d.Orgid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Display_Organization");
        });

        modelBuilder.Entity<DisplayAdverts>(entity =>
        {
            entity.Property(e => e.DisplayId).HasColumnName("DisplayID");

            entity.HasOne(d => d.Advert).WithMany(p => p.DisplayAdverts)
                .HasForeignKey(d => d.AdvertId)
                .HasConstraintName("FK_DisplayAdverts_Advertisment");

            entity.HasOne(d => d.Display).WithMany(p => p.DisplayAdverts)
                .HasForeignKey(d => d.DisplayId)
                .HasConstraintName("FK_DisplayAdverts_Display");
        });

        modelBuilder.Entity<DisplayCounters>(entity =>
        {
            entity.HasOne(d => d.Counter).WithMany(p => p.DisplayCounters)
                .HasForeignKey(d => d.CounterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DisplayCounters_Counters");

            entity.HasOne(d => d.Display).WithMany(p => p.DisplayCounters)
                .HasForeignKey(d => d.DisplayId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DisplayCounters_Display");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("employee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Citizenid).HasColumnName("citizenid");
            entity.Property(e => e.Orgid).HasColumnName("orgid");
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Citizen).WithMany(p => p.Employee)
                .HasForeignKey(d => d.Citizenid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_employee_Citizen");

            entity.HasOne(d => d.Org).WithMany(p => p.Employee)
                .HasForeignKey(d => d.Orgid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_employee_Organization");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.GroupName).HasMaxLength(100);
        });

        modelBuilder.Entity<GroupUser>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");

            entity.HasOne(d => d.Emp).WithMany(p => p.GroupUser)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK_GroupUser_employee");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupUser)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_GroupUser_Group");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Orgid);

            entity.Property(e => e.OrgName).HasMaxLength(100);
        });

        modelBuilder.Entity<Reservations>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RegistarionData");

            entity.Property(e => e.CallAt).HasColumnType("datetime");
            entity.Property(e => e.CounterId).HasColumnName("counterId");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EndServing).HasColumnType("datetime");
            entity.Property(e => e.IsCancelled).HasColumnName("isCancelled");
            entity.Property(e => e.IsTransferd)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isTransferd");
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Nid)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("NId");
            entity.Property(e => e.Orgid).HasColumnName("orgid");
            entity.Property(e => e.ReservationDate).HasColumnType("datetime");
            entity.Property(e => e.ReservationType).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TicketNumber).HasMaxLength(100);
            entity.Property(e => e.TransferedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Citizen).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CitizenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Citizen");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Reservations_Customer");

            entity.HasOne(d => d.Org).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Orgid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reservations_Organization");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Prefix).HasMaxLength(50);
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}