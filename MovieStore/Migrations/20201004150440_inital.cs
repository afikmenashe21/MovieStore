using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.CreateTable(
        //        name: "Actor",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Name = table.Column<string>(nullable: true),
        //            MovieId = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Actor", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Genre",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Type = table.Column<string>(nullable: false),
        //            MovieId = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Genre", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Movie",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Name = table.Column<string>(nullable: false),
        //            ReleaseDate = table.Column<DateTime>(nullable: false),
        //            Duration = table.Column<int>(nullable: false),
        //            Director = table.Column<string>(nullable: true),
        //            Poster = table.Column<string>(nullable: true),
        //            Trailer = table.Column<string>(nullable: true),
        //            Storyline = table.Column<string>(nullable: true),
        //            AverageRating = table.Column<double>(nullable: false),
        //            imdbID = table.Column<string>(nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Movie", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "User",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            UserName = table.Column<string>(nullable: false),
        //            Type = table.Column<int>(nullable: false),
        //            Password = table.Column<string>(nullable: false),
        //            Email = table.Column<string>(nullable: false),
        //            FirstName = table.Column<string>(nullable: false),
        //            LastName = table.Column<string>(nullable: false),
        //            Address = table.Column<string>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_User", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "MovieActor",
        //        columns: table => new
        //        {
        //            MovieId = table.Column<int>(nullable: false),
        //            ActorId = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_MovieActor", x => new { x.MovieId, x.ActorId });
        //            table.ForeignKey(
        //                name: "FK_MovieActor_Actor_ActorId",
        //                column: x => x.ActorId,
        //                principalTable: "Actor",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_MovieActor_Movie_MovieId",
        //                column: x => x.MovieId,
        //                principalTable: "Movie",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "MovieGenre",
        //        columns: table => new
        //        {
        //            MovieId = table.Column<int>(nullable: false),
        //            GenreId = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_MovieGenre", x => new { x.MovieId, x.GenreId });
        //            table.ForeignKey(
        //                name: "FK_MovieGenre_Genre_GenreId",
        //                column: x => x.GenreId,
        //                principalTable: "Genre",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_MovieGenre_Movie_MovieId",
        //                column: x => x.MovieId,
        //                principalTable: "Movie",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Review",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Headline = table.Column<string>(nullable: false),
        //            Content = table.Column<string>(nullable: false),
        //            Rating = table.Column<double>(nullable: false),
        //            Published = table.Column<DateTime>(nullable: false),
        //            AuthorId = table.Column<int>(nullable: true),
        //            MovieId = table.Column<int>(nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Review", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Review_User_AuthorId",
        //                column: x => x.AuthorId,
        //                principalTable: "User",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "FK_Review_Movie_MovieId",
        //                column: x => x.MovieId,
        //                principalTable: "Movie",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_MovieActor_ActorId",
        //        table: "MovieActor",
        //        column: "ActorId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_MovieGenre_GenreId",
        //        table: "MovieGenre",
        //        column: "GenreId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Review_AuthorId",
        //        table: "Review",
        //        column: "AuthorId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Review_MovieId",
        //        table: "Review",
        //        column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "MovieActor");

            //migrationBuilder.DropTable(
            //    name: "MovieGenre");

            //migrationBuilder.DropTable(
            //    name: "Review");

            //migrationBuilder.DropTable(
            //    name: "Actor");

            //migrationBuilder.DropTable(
            //    name: "Genre");

            //migrationBuilder.DropTable(
            //    name: "User");

            //migrationBuilder.DropTable(
            //    name: "Movie");
        }
    }
}
