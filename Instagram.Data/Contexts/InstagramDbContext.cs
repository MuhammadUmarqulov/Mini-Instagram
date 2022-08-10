using Instagram.Domain.Constants;
using Instagram.Domain.Entities.Posts;
using Instagram.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Data.Contexts
{
    public class InstagramDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSettings.CONNETION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and Post relationship ( n : n ) 

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                    .WithOne(f => f.FollowingTo);

            modelBuilder.Entity<UserFollowing>()
                .HasOne(f => f.FollowingTo)
                    .WithMany(u => u.Followers);
            ;

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followings)
                    .WithOne(f => f.FollowingFrom);


            modelBuilder.Entity<UserFollowing>()
                .HasOne(f => f.FollowingFrom)
                    .WithMany(u => u.Followings);

            // post and user relationship (n : 1) if user is deleted, all his posts are deleted

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                    .WithOne(post => post.User)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                    .WithOne(comment => comment.Post)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasMany(user => user.SavedPosts)
                    .WithOne(post => post.User)
                        .OnDelete(DeleteBehavior.ClientCascade);

            // Post and Comment relationship (1 : n) 

            modelBuilder.Entity<Post>()
                .HasMany(post => post.Comments)
                    .WithOne(comment => comment.Post)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.Likes)
                    .WithOne(like => like.Post)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.SavedPosts)
                    .WithOne(savedPost => savedPost.Post)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Post>()
                .HasMany(post => post.Contents)
                    .WithOne(content => content.Post)
                        .OnDelete(DeleteBehavior.ClientCascade);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<SavedPost> SavedPosts { get; set; }
        public DbSet<UserFollowing> Followings { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
