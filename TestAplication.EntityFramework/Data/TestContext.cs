using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestAplication.EntityFramework.Data
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }
        public virtual DbSet<ReporteMovimiento> ReporteMovimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Database=Test; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.ClienteId).HasColumnName("clienteId");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Genero)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuentaId)
                    .HasName("PK__Cuenta__1808B1226DF622DC");

                entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuenta__C6B74B88E4C1E445")
                    .IsUnique();

                entity.Property(e => e.CuentaId).HasColumnName("cuentaId");

                entity.Property(e => e.ClienteId).HasColumnName("clienteId");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.NumeroCuenta)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("numero_cuenta");

                entity.Property(e => e.SaldoInicial)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo_inicial");

                entity.Property(e => e.TipoCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_cuenta");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__Cuenta__clienteI__2A4B4B5E");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.Property(e => e.MovimientoId).HasColumnName("movimientoId");

                entity.Property(e => e.CuentaId).HasColumnName("cuentaId");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Saldo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("saldo");

                entity.Property(e => e.TipoMovimiento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_movimiento");

                entity.Property(e => e.Valor)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("valor");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.CuentaId)
                    .HasConstraintName("FK__Movimient__cuent__29572725");
            });

            modelBuilder.Entity<ReporteMovimiento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReporteMovimientos");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Movimiento).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NumeroCuenta)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SaldoDisponible).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
