using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Turnstyle.Models
{
    public class triumphContext : DbContext
    {
        public triumphContext()
        {
        }

        public triumphContext(DbContextOptions<triumphContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Rolepermissions> Rolepermissions { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Teammember> Teammember { get; set; }
        public virtual DbSet<Thread> Thread { get; set; }
        public virtual DbSet<Threadtags> Threadtags { get; set; }
        public virtual DbSet<Userrole> Userrole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(256);

                entity.Property(e => e.Image)
                    .HasColumnName("image");

                entity.Property(e => e.Lastlogin)
                    .HasColumnName("lastlogin")
                    .HasColumnType("date");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Parentcomment)
                    .HasColumnName("parentcomment")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Parentthread)
                    .HasColumnName("parentthread")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_author_fkey");

                entity.HasOne(d => d.ImageNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Image)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_image_fkey");

                entity.HasOne(d => d.ParentcommentNavigation)
                    .WithMany(p => p.InverseParentcommentNavigation)
                    .HasForeignKey(d => d.Parentcomment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_parentcomment_fkey");

                entity.HasOne(d => d.ParentthreadNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Parentthread)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_parentthread_fkey");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Eventdate)
                    .HasColumnName("eventdate")
                    .HasColumnType("date");

                entity.Property(e => e.Isvisible).HasColumnName("isvisible");

                entity.Property(e => e.Opponent)
                    .HasColumnName("opponent")
                    .HasMaxLength(64);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(256);

                entity.HasOne(d => d.CreatedbyNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.Createdby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("event_createdby_fkey");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasColumnName("source")
                    .HasMaxLength(512);

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.ImageNavigation)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("image_owner_fkey");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Rolepermissions>(entity =>
            {
                entity.ToTable("rolepermissions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Permissionid)
                    .HasColumnName("permissionid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Roleid)
                    .HasColumnName("roleid")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Rolepermissions)
                    .HasForeignKey(d => d.Permissionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolepermissions_permissionid_fkey");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Rolepermissions)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolepermissions_roleid_fkey");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasMaxLength(16);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasColumnName("logo");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Teammember>(entity =>
            {
                entity.ToTable("teammember");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Changedby)
                    .HasColumnName("changedby")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Lastchanged)
                    .HasColumnName("lastchanged")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64);

                entity.Property(e => e.Teamid)
                    .HasColumnName("teamid")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.ChangedbyNavigation)
                    .WithMany(p => p.Teammember)
                    .HasForeignKey(d => d.Changedby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teammember_changedby_fkey");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Teammember)
                    .HasForeignKey(d => d.Teamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("teammember_teamid_fkey");
            });

            modelBuilder.Entity<Thread>(entity =>
            {
                entity.ToTable("thread");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content");

                entity.Property(e => e.Dateposted)
                    .HasColumnName("dateposted")
                    .HasColumnType("date");

                entity.Property(e => e.Headerimage)
                    .HasColumnName("headerimage")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(256);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Thread)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("thread_author_fkey");

                entity.HasOne(d => d.HeaderimageNavigation)
                    .WithMany(p => p.Thread)
                    .HasForeignKey(d => d.Headerimage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("thread_headerimage_fkey");
            });

            modelBuilder.Entity<Threadtags>(entity =>
            {
                entity.ToTable("threadtags");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Thread)
                    .HasColumnName("thread")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.TagNavigation)
                    .WithMany(p => p.Threadtags)
                    .HasForeignKey(d => d.Tag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("threadtags_tag_fkey");

                entity.HasOne(d => d.ThreadNavigation)
                    .WithMany(p => p.Threadtags)
                    .HasForeignKey(d => d.Thread)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("threadtags_thread_fkey");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.ToTable("userrole");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Grantedby)
                    .HasColumnName("grantedby")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Roleid)
                    .HasColumnName("roleid")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.GrantedbyNavigation)
                    .WithMany(p => p.UserroleGrantedbyNavigation)
                    .HasForeignKey(d => d.Grantedby)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userrole_grantedby_fkey");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userrole)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userrole_roleid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserroleUser)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userrole_userid_fkey");
            });
        }
    }
}
