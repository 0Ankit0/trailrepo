using Microsoft.EntityFrameworkCore;

namespace RepositoryPattern.DAL
{
	public partial class StudentContext : DbContext 
	{
		
		public StudentContext(DbContextOptions<StudentContext> options)
			: base(options)
		{
		}

		public virtual DbSet<Student> Students { get; set; } 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			 modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentID);

                entity.Property(e => e.StudentID)
                      .ValueGeneratedOnAdd();  
            });

			base.OnModelCreating(modelBuilder); 
		}
	}
}
