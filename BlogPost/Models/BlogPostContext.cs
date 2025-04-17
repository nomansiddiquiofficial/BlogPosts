using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Models;

public partial class BlogPostContext : DbContext
{
    public BlogPostContext()
    {
    }

    public BlogPostContext(DbContextOptions<BlogPostContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-SLF552E;database=BlogPost;trusted_connection=true;TrustServerCertificate=True");


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
