using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ComputersUnivLab1
{
    public partial class ComputersContext : DbContext
    {
        public ComputersContext()
        {
        }

        public ComputersContext(DbContextOptions<ComputersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<ComputersToDrive> ComputersToDrives { get; set; }
        public virtual DbSet<Cooler> Coolers { get; set; }
        public virtual DbSet<Cpu> Cpus { get; set; }
        public virtual DbSet<Drive> Drives { get; set; }
        public virtual DbSet<Gpu> Gpus { get; set; }
        public virtual DbSet<Gpuinterface> Gpuinterfaces { get; set; }
        public virtual DbSet<Motherboard> Motherboards { get; set; }
        public virtual DbSet<PowerSupply> PowerSupplies { get; set; }
        public virtual DbSet<Ram> Rams { get; set; }
        public virtual DbSet<RamsToComputer> RamsToComputers { get; set; }
        public virtual DbSet<Ramtype> Ramtypes { get; set; }
        public virtual DbSet<Socket> Sockets { get; set; }
        public virtual DbSet<SocketsToCooler> SocketsToCoolers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-T2J2V70\\MSSQLSERVER01; Database=Computers; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>(entity =>
            {
                entity.Property(e => e.Cpuid).HasColumnName("CPUId");

                entity.Property(e => e.Gpuid).HasColumnName("GPUId");

                entity.HasOne(d => d.Cooler)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.CoolerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Computers_Coolers");

                entity.HasOne(d => d.Cpu)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.Cpuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Computers_CPUs");

                entity.HasOne(d => d.Gpu)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.Gpuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Computers_GPUs");

                entity.HasOne(d => d.Motherboard)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.MotherboardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Computers_Motherboards");

                entity.HasOne(d => d.PowerSupply)
                    .WithMany(p => p.Computers)
                    .HasForeignKey(d => d.PowerSupplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Computers_PowerSupplies");
            });

            modelBuilder.Entity<ComputersToDrive>(entity =>
            {
                entity.HasOne(d => d.Computer)
                    .WithMany(p => p.ComputersToDrives)
                    .HasForeignKey(d => d.ComputerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComputersToDrives_Computers");

                entity.HasOne(d => d.Drive)
                    .WithMany(p => p.ComputersToDrives)
                    .HasForeignKey(d => d.DriveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComputersToDrives_Drives");
            });

            modelBuilder.Entity<Cooler>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Tdp).HasColumnName("TDP");
            });

            modelBuilder.Entity<Cpu>(entity =>
            {
                entity.ToTable("CPUs");

                entity.Property(e => e.Manufacturer).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Socket)
                    .WithMany(p => p.Cpus)
                    .HasForeignKey(d => d.SocketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CPUs_Sockets");
            });

            modelBuilder.Entity<Drive>(entity =>
            {
                entity.Property(e => e.Interface).IsRequired();

                entity.Property(e => e.Manufacturer).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Gpu>(entity =>
            {
                entity.ToTable("GPUs");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Interface)
                    .WithMany(p => p.Gpus)
                    .HasForeignKey(d => d.InterfaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GPUs_GPUInterfaces");
            });

            modelBuilder.Entity<Gpuinterface>(entity =>
            {
                entity.ToTable("GPUInterfaces");
            });

            modelBuilder.Entity<Motherboard>(entity =>
            {
                entity.Property(e => e.Formfactor).IsRequired();

                entity.Property(e => e.GpuinterfaceId).HasColumnName("GPUInterfaceId");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Ramcount).HasColumnName("RAMCount");

                entity.Property(e => e.RamtypeId).HasColumnName("RAMTypeId");

                entity.Property(e => e.Usbcount).HasColumnName("USBCount");

                entity.HasOne(d => d.Gpuinterface)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.GpuinterfaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motherboards_GPUInterfaces");

                entity.HasOne(d => d.Ramtype)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.RamtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motherboards_RAMTypes");

                entity.HasOne(d => d.Socket)
                    .WithMany(p => p.Motherboards)
                    .HasForeignKey(d => d.SocketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motherboards_Sockets");
            });

            modelBuilder.Entity<PowerSupply>(entity =>
            {
                entity.Property(e => e.Manufacturer).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Ram>(entity =>
            {
                entity.ToTable("RAMs");

                entity.Property(e => e.Manufacturer).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Rams)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RAMs_RAMTypes");
            });

            modelBuilder.Entity<RamsToComputer>(entity =>
            {
                entity.ToTable("RAMsToComputers");

                entity.Property(e => e.Ramid).HasColumnName("RAMId");

                entity.HasOne(d => d.Computer)
                    .WithMany(p => p.RamsToComputers)
                    .HasForeignKey(d => d.ComputerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RAMsToComputers_Computers");

                entity.HasOne(d => d.Ram)
                    .WithMany(p => p.RamsToComputers)
                    .HasForeignKey(d => d.Ramid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RAMsToComputers_RAMs");
            });

            modelBuilder.Entity<Ramtype>(entity =>
            {
                entity.ToTable("RAMTypes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Socket>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SocketsToCooler>(entity =>
            {
                entity.HasOne(d => d.Cooler)
                    .WithMany(p => p.SocketsToCoolers)
                    .HasForeignKey(d => d.CoolerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SocketsToCoolers_Coolers");

                entity.HasOne(d => d.Socket)
                    .WithMany(p => p.SocketsToCoolers)
                    .HasForeignKey(d => d.SocketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SocketsToCoolers_Sockets");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
