using IDP_Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace IDP_Back_End.Repository
{
  public class DBContext : DbContext
  {
    public DBContext(DbContextOptions<DBContext> opt)
        : base(opt) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryID);

        modelBuilder.Entity<Task>()
            .HasMany(t => t.CheckListItems)
            .WithOne(cli => cli.Task)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(cli => cli.TaskID); 
            
        modelBuilder.Entity<Task>()
            .HasMany(t => t.Comments)
            .WithOne(c => c.Task)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(c => c.TaskID);
            
        modelBuilder.Entity<User>()
            .HasMany(u => u.TasksCreated)
            .WithOne(tc => tc.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(tc => tc.CreatedByID);
            
        modelBuilder.Entity<User>()
            .HasMany(u => u.TasksOfUser)
            .WithOne(tc => tc.TaskOf)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(tc => tc.TaskOfID);
            
        modelBuilder.Entity<User>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(c => c.UserID);
            
        modelBuilder.Entity<User>()
            .HasMany(u => u.ChatMessages)
            .WithOne(cm => cm.User)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(cm => cm.UserID);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<TaskCategory> Categories { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<CheckListItem> CheckListItems { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

  }
}
