using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.Identity.Properties;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public IdentityDbContext() : this("DefaultConnection")
        {
        }

        public IdentityDbContext(DbCompiledModel model) : base(model)
        {
        }

        public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) 
            : base(existingConnection, model, contextOwnsConnection)
        {
        }
    }

    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> 
        where TUser : IdentityUser
    {
        public IdentityDbContext() : this("DefaultConnection")
        {
        }

        public IdentityDbContext(DbCompiledModel model) : base(model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) 
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, bool throwIfV1Schema = true) 
            : base(nameOrConnectionString)
        {
            if (throwIfV1Schema && IsIdentityV1Schema(this))
            {
                throw new InvalidOperationException(Resources.IdentityV1SchemaError);
            }
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) 
            : base(nameOrConnectionString, model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) 
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        internal static bool IsIdentityV1Schema(DbContext db)
        {
            var connection = db.Database.Connection as SqlConnection;
            if ((connection == null) || !db.Database.Exists()) return false;
            using (var connection2 = new SqlConnection(connection.ConnectionString))
            {
                connection2.Open();
                return (((VerifyColumns(connection2, "AspNetUsers", "Id", "UserName", "PasswordHash", "SecurityStamp", "Discriminator") 
                          && VerifyColumns(connection2, "AspNetRoles", "Id", "Name")) 
                         && (VerifyColumns(connection2, "AspNetUserRoles", "UserId", "RoleId") 
                             && VerifyColumns(connection2, "AspNetUserClaims", "Id", "ClaimType", "ClaimValue", "User_Id"))) 
                        && VerifyColumns(connection2, "AspNetUserLogins", "UserId", "ProviderKey", "LoginProvider"));
            }
        }

        internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
        {
            var list = new List<string>();
            using (var command = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn))
            {
                command.Parameters.Add(new SqlParameter("Table", table));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
            }
            return columns.All(list.Contains);
        }
    }

    public class IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : DbContext 
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim> 
        where TRole : IdentityRole<TKey, TUserRole> 
        where TUserLogin : IdentityUserLogin<TKey> 
        where TUserRole : IdentityUserRole<TKey> 
        where TUserClaim : IdentityUserClaim<TKey>
    {
        public IdentityDbContext() : this("DefaultConnection")
        {
        }

        public IdentityDbContext(DbCompiledModel model) : base(model)
        {
        }

        public IdentityDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) 
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            var configuration = modelBuilder.Entity<TUser>().ToTable("AspNetUsers");
            configuration.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            configuration.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            configuration.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            var indexAttribute = new IndexAttribute("UserNameIndex")
            {
                IsUnique = true
            };
            configuration.Property(u => u.UserName).IsRequired().HasMaxLength(0x100)
                .HasColumnAnnotation("Index", new IndexAnnotation(indexAttribute));
            configuration.Property(u => u.Email).HasMaxLength(0x100);
            modelBuilder.Entity<TUserRole>().HasKey(r => new {r.UserId, r.RoleId })
                .ToTable("AspNetUserRoles");
            modelBuilder.Entity<TUserLogin>().HasKey(l => new {l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");
            modelBuilder.Entity<TUserClaim>().ToTable("AspNetUserClaims");
            var configuration2 = modelBuilder.Entity<TRole>().ToTable("AspNetRoles");
            var attribute2 = new IndexAttribute("RoleNameIndex")
            {
                IsUnique = true
            };
            configuration2.Property(r => r.Name).IsRequired().HasMaxLength(0x100).HasColumnAnnotation("Index", 
                new IndexAnnotation(attribute2));
            configuration2.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if ((entityEntry == null) || (entityEntry.State != EntityState.Added))
                return base.ValidateEntity(entityEntry, items);
            var source = new List<DbValidationError>();
            var user = entityEntry.Entity as TUser;
            if (user != null)
            {
                if (Users.Any(u => string.Equals(u.UserName, user.UserName)))
                {
                    source.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, Resources.DuplicateUserName, user.UserName)));
                }
                if (RequireUniqueEmail && Users.Any(u => string.Equals(u.Email, user.Email)))
                {
                    source.Add(new DbValidationError("User", string.Format(CultureInfo.CurrentCulture, Resources.DuplicateEmail, user.Email)));
                }
            }
            else
            {
                var role = entityEntry.Entity as TRole;
                if ((role != null) && Roles.Any(r => string.Equals(r.Name, role.Name)))
                {
                    source.Add(new DbValidationError("Role", string.Format(CultureInfo.CurrentCulture, Resources.RoleAlreadyExists, role.Name)));
                }
            }
            return source.Any() ? new DbEntityValidationResult(entityEntry, source) : base.ValidateEntity(entityEntry, items);
        }

        public bool RequireUniqueEmail { get; set; }

        public virtual IDbSet<TRole> Roles { get; set; }

        public virtual IDbSet<TUser> Users { get; set; }
    }
}
