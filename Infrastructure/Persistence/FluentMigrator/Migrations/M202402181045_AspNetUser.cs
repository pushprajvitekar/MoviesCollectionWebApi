using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(202402181045)]
    public class M202402181045_AspNetUser : ForwardOnlyMigration
    {
        public override void Up()
        {

            Create.Table("AspNetRoles")
              .WithColumn("Id").AsString().PrimaryKey("PK_AspNetRoles").NotNullable()
              .WithColumn("ConcurrencyStamp").AsString().Nullable()
              .WithColumn("Name").AsString(256).NotNullable()
              .WithColumn("NormalizedName").AsString(256).Nullable()
            .Indexed("RoleNameIndex");


        

            Create.Table("AspNetUsers")
               .WithColumn("Id").AsString().NotNullable().PrimaryKey("PK_AspNetUsers")
               .WithColumn("UserName").AsString(256).Nullable()
               .WithColumn("NormalizedUserName").AsString(256).Nullable()
               .WithColumn("Email").AsString(256).Nullable()
               .WithColumn("NormalizedEmail").AsString(256).Nullable()
               .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
               .WithColumn("PasswordHash").AsString().Nullable()
               .WithColumn("SecurityStamp").AsString().Nullable()
               .WithColumn("ConcurrencyStamp").AsString().Nullable()
               .WithColumn("PhoneNumber").AsString().Nullable()
               .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
               .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
               .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
               .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
               .WithColumn("AccessFailedCount").AsInt32().NotNullable()
               ;

            Create.Table("AspNetUserClaims")
              .WithColumn("Id").AsInt32().PrimaryKey("PK_AspNetUserClaims").Identity()
              .WithColumn("ClaimType").AsString().Nullable()
              .WithColumn("ClaimValue").AsString().Nullable()
              .WithColumn("UserId").AsString().NotNullable().Indexed("IX_AspNetUserClaims_UserId")
                                   .ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUsers", "Id")
                                   .OnDelete(System.Data.Rule.Cascade);

            Create.Table("AspNetUserLogins")
              .WithColumn("LoginProvider").AsString().NotNullable().PrimaryKey("PK_AspNetUserLogins")
              .WithColumn("ProviderKey").AsString().NotNullable().PrimaryKey("PK_AspNetUserLogins")
              .WithColumn("ProviderDisplayName").AsString().Nullable()
              .WithColumn("UserId").AsString()
                                   .NotNullable()
                                   .Indexed("IX_AspNetUserLogins_UserId")
                                   .ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUsers", "Id")
                                   .OnDelete(System.Data.Rule.Cascade);



            //migrationBuilder.CreateTable(
            //   name: "AspNetUserTokens",
            //   columns: table => new
            //   {
            //       UserId = table.Column<string>(type: "text", nullable: false),
            //       LoginProvider = table.Column<string>(type: "text", nullable: false),
            //       Name = table.Column<string>(type: "text", nullable: false),
            //       Value = table.Column<string>(type: "text", nullable: true)
            //   },
            //   constraints: table =>
            //   {
            //       table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //       table.ForeignKey(
            //           name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //           column: x => x.UserId,
            //           principalTable: "AspNetUsers",
            //           principalColumn: "Id",
            //           onDelete: ReferentialAction.Cascade);
            //   });

            Create.Table("AspNetUserTokens")
                .WithColumn("UserId").AsString().NotNullable()
                .WithColumn("LoginProvider").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Value").AsString().Nullable()
          ;
            Create.PrimaryKey("PK_AspNetUserTokens").OnTable("AspNetUserTokens").Columns(["UserId", "LoginProvider", "Name"]);
            Create.ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId").FromTable("AspNetUserTokens").ForeignColumn("UserId").ToTable("AspNetUsers").PrimaryColumn("Id").OnDelete(System.Data.Rule.Cascade);


            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");
            //Create.Index("IX_AspNetUserClaims_UserId").OnTable("AspNetUserClaims").OnColumn("UserId");
            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");
            //Create.Index("IX_AspNetUserLogins_UserId").OnTable("AspNetUserLogins").OnColumn("UserId");
            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");
            Create.Index("EmailIndex").OnTable("AspNetUsers").OnColumn("NormalizedEmail");
            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true);
            Create.Index("UserNameIndex").OnTable("AspNetUsers").OnColumn("NormalizedUserName").Unique();

            Create.Table("AspNetUserRoles")
              .WithColumn("UserId").AsString()
                                   .PrimaryKey("PK_AspNetUserRoles")
                                   .Indexed("IX_AspNetUserRoles_UserId")
                                   .ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUsers", "Id")

              .WithColumn("RoleId").AsString()
                                   .PrimaryKey("PK_AspNetUserRoles")
                                   .Indexed("IX_AspNetUserRoles_RoleId")
                                   .ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetRoles", "Id")
                                   .OnDelete(System.Data.Rule.Cascade);

        }
    }
}
