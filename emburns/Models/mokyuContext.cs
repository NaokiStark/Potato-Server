﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace emburns.Models
{
    public partial class mokyuContext : DbContext
    {
        public mokyuContext()
        {
        }

        public mokyuContext(DbContextOptions<mokyuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Blocks { get; set; } = null!;
        public virtual DbSet<CiSession> CiSessions { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Community> Communities { get; set; } = null!;
        public virtual DbSet<Feed> Feeds { get; set; } = null!;
        public virtual DbSet<Follow> Follows { get; set; } = null!;
        public virtual DbSet<KyunScore> KyunScores { get; set; } = null!;
        public virtual DbSet<Lofe> Loves { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Point> Points { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostCategory> PostCategories { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<Rank> Ranks { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<ReactionsList> ReactionsLists { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersRequest> UsersRequests { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("blocks");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Blocked)
                    .HasColumnType("int(11)")
                    .HasColumnName("blocked");

                entity.Property(e => e.Blocker)
                    .HasColumnType("int(11)")
                    .HasColumnName("blocker");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("date")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<CiSession>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ci_sessions");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Timestamp, "ci_sessions_timestamp");

                entity.Property(e => e.Data)
                    .HasColumnType("blob")
                    .HasColumnName("data");

                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .HasColumnName("id");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(45)
                    .HasColumnName("ip_address");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(255)")
                    .HasColumnName("postid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .HasColumnName("text");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(255)")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Community>(entity =>
            {
                entity.ToTable("communities");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(1000)
                    .HasColumnName("avatar");

                entity.Property(e => e.Background)
                    .HasMaxLength(256)
                    .HasColumnName("background");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("category_id");

                entity.Property(e => e.Cover)
                    .HasMaxLength(256)
                    .HasColumnName("cover");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Creator)
                    .HasColumnType("int(11)")
                    .HasColumnName("creator");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Members)
                    .HasColumnType("text")
                    .HasColumnName("members");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Feed>(entity =>
            {
                entity.ToTable("feed");

                entity.HasIndex(e => e.Userid, "FEED_USER_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Attachment)
                    .HasMaxLength(1000)
                    .HasColumnName("attachment")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.AttachmentType)
                    .HasColumnType("int(10)")
                    .HasColumnName("attachment_type");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Loves)
                    .HasColumnType("int(11)")
                    .HasColumnName("loves");

                entity.Property(e => e.Nsfw).HasColumnName("nsfw");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("parent_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Sticky).HasColumnName("sticky");

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .HasColumnName("text")
                    .UseCollation("utf8mb4_bin");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");

                entity.Property(e => e.ViaId)
                    .HasColumnType("int(255)")
                    .HasColumnName("via_id");

                entity.Property(e => e.Wall)
                    .HasColumnType("int(255)")
                    .HasColumnName("wall");

                /*
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feeds)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FEED_USER_FK");*/
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("follows");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Follower)
                    .HasColumnType("int(255)")
                    .HasColumnName("follower");

                entity.Property(e => e.Following)
                    .HasColumnType("int(255)")
                    .HasColumnName("following");
            });

            modelBuilder.Entity<KyunScore>(entity =>
            {
                entity.ToTable("kyun_scores");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Beatmapartist)
                    .HasColumnType("text")
                    .HasColumnName("beatmapartist");

                entity.Property(e => e.Beatmapid)
                    .HasColumnType("int(11)")
                    .HasColumnName("beatmapid");

                entity.Property(e => e.Beatmaptitle)
                    .HasColumnType("text")
                    .HasColumnName("beatmaptitle");

                entity.Property(e => e.Beatmapversion)
                    .HasColumnType("text")
                    .HasColumnName("beatmapversion");

                entity.Property(e => e.Totalcombo)
                    .HasColumnType("int(11)")
                    .HasColumnName("totalcombo");

                entity.Property(e => e.Totalscore)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("totalscore");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Lofe>(entity =>
            {
                entity.ToTable("loves");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Post)
                    .HasColumnType("int(11)")
                    .HasColumnName("post");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("log");

                entity.Property(e => e.ActionType)
                    .HasColumnType("int(11)")
                    .HasColumnName("action_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasColumnName("notes");

                entity.Property(e => e.ObjectId)
                    .HasColumnType("int(11)")
                    .HasColumnName("object_id");

                entity.Property(e => e.ObjectType)
                    .HasColumnType("text")
                    .HasColumnName("object_type");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Addressee)
                    .HasColumnType("int(11)")
                    .HasColumnName("addressee")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Message1)
                    .HasMaxLength(1000)
                    .HasColumnName("message");

                entity.Property(e => e.Sender)
                    .HasColumnType("int(11)")
                    .HasColumnName("sender")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notifications");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Interventor)
                    .HasColumnType("int(11)")
                    .HasColumnName("interventor");

                entity.Property(e => e.Link)
                    .HasColumnType("text")
                    .HasColumnName("link")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Listable)
                    .IsRequired()
                    .HasColumnName("listable")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Meta)
                    .HasColumnType("int(11)")
                    .HasColumnName("meta");

                entity.Property(e => e.Readed).HasColumnName("readed");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");

                entity.Property(e => e.Wsqueue).HasColumnName("wsqueue");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permissions");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Permissions)
                    .HasMaxLength(1000)
                    .HasColumnName("permissions");

                entity.Property(e => e.RankId)
                    .HasColumnType("int(11)")
                    .HasColumnName("rank_id");
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.ToTable("points");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("int(11)")
                    .HasColumnName("amount");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(11)")
                    .HasColumnName("postid");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(11)")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Body)
                    .HasMaxLength(10000)
                    .HasColumnName("body");

                entity.Property(e => e.Caption)
                    .HasMaxLength(256)
                    .HasColumnName("caption");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("category_id");

                entity.Property(e => e.CommunityId)
                    .HasColumnType("int(11)")
                    .HasColumnName("community_id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Creator)
                    .HasColumnType("int(11)")
                    .HasColumnName("creator");

                entity.Property(e => e.Points)
                    .HasColumnType("int(11)")
                    .HasColumnName("points");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasColumnName("status");

                entity.Property(e => e.Sticky).HasColumnName("sticky");

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.ToTable("post_category");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CatImage)
                    .HasMaxLength(1000)
                    .HasColumnName("cat_image");

                entity.Property(e => e.Icon)
                    .HasMaxLength(1000)
                    .HasColumnName("icon");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Shortname)
                    .HasMaxLength(100)
                    .HasColumnName("shortname");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.ToTable("post_comments");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("created")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Postid)
                    .HasColumnType("int(255)")
                    .HasColumnName("postid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .HasColumnName("text");

                entity.Property(e => e.Userid)
                    .HasColumnType("int(255)")
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("ranks");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(20)
                    .HasColumnName("fullname");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.RequiredPoints)
                    .HasColumnType("int(11)")
                    .HasColumnName("required_points");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("reactions");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ElementId)
                    .HasColumnType("int(11)")
                    .HasColumnName("element_id");

                entity.Property(e => e.ElementType)
                    .HasColumnType("int(11)")
                    .HasColumnName("element_type");

                entity.Property(e => e.Reactions)
                    .HasColumnType("text")
                    .HasColumnName("reactions");
            });

            modelBuilder.Entity<ReactionsList>(entity =>
            {
                entity.ToTable("reactions_list");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ReactionEmoji)
                    .HasMaxLength(10)
                    .HasColumnName("reaction_emoji");

                entity.Property(e => e.ReactionText)
                    .HasMaxLength(500)
                    .HasColumnName("reaction_text");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.AnonExpires)
                    .HasColumnType("datetime")
                    .HasColumnName("anon_expires");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(250)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("'default.png'")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Background)
                    .HasColumnType("text")
                    .HasColumnName("background")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Country)
                    .HasMaxLength(250)
                    .HasColumnName("country")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Cover)
                    .HasMaxLength(250)
                    .HasColumnName("cover")
                    .HasDefaultValueSql("'default.png'")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp")
                    .HasColumnName("created")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Donation).HasColumnName("donation");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(32)
                    .HasColumnName("lastname")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Lastpost)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("lastpost")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Moderator).HasColumnName("moderator");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.PointDate)
                    .HasColumnType("int(20)")
                    .HasColumnName("point_date");

                entity.Property(e => e.Quote)
                    .HasColumnType("text")
                    .HasColumnName("quote")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Rank)
                    .HasPrecision(10, 2)
                    .HasColumnName("rank")
                    .HasDefaultValueSql("'2.00'");

                entity.Property(e => e.RemainPoints)
                    .HasColumnType("int(11)")
                    .HasColumnName("remain_points")
                    .HasDefaultValueSql("'10'");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.User1)
                    .HasMaxLength(256)
                    .HasColumnName("user")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Wshash)
                    .HasColumnType("text")
                    .HasColumnName("wshash")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            modelBuilder.Entity<UsersRequest>(entity =>
            {
                entity.ToTable("users_request");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("'https://onics.me/style/default.png'")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Background)
                    .HasColumnType("text")
                    .HasColumnName("background")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Country)
                    .HasMaxLength(250)
                    .HasColumnName("country")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Cover)
                    .HasMaxLength(128)
                    .HasColumnName("cover")
                    .HasDefaultValueSql("'default.png'")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(32)
                    .HasColumnName("lastname")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Lastpost)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("lastpost")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Moderator).HasColumnName("moderator");

                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Quote)
                    .HasColumnType("text")
                    .HasColumnName("quote")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.RequestHash)
                    .HasColumnType("text")
                    .HasColumnName("requestHash")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.User)
                    .HasMaxLength(256)
                    .HasColumnName("user")
                    .UseCollation("utf8_unicode_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Wshash)
                    .HasColumnType("text")
                    .HasColumnName("wshash")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
