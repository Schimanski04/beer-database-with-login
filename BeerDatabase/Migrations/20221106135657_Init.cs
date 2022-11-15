using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerDatabase.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breweries",
                columns: table => new
                {
                    BreweryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.BreweryId);
                });

            migrationBuilder.CreateTable(
                name: "Kinds",
                columns: table => new
                {
                    KindId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.KindId);
                });

            migrationBuilder.CreateTable(
                name: "Pubs",
                columns: table => new
                {
                    PubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pubs", x => x.PubId);
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    BeerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlcoholContent = table.Column<double>(type: "float", nullable: true),
                    PricePerHalfLitre = table.Column<double>(type: "float", nullable: true),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreweryId = table.Column<int>(type: "int", nullable: false),
                    KindId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.BeerId);
                    table.ForeignKey(
                        name: "FK_Beers_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Breweries",
                        principalColumn: "BreweryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beers_Kinds_KindId",
                        column: x => x.KindId,
                        principalTable: "Kinds",
                        principalColumn: "KindId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeerPubs",
                columns: table => new
                {
                    BeerId = table.Column<int>(type: "int", nullable: false),
                    PubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerPubs", x => new { x.BeerId, x.PubId });
                    table.ForeignKey(
                        name: "FK_BeerPubs_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerPubs_Pubs_PubId",
                        column: x => x.PubId,
                        principalTable: "Pubs",
                        principalColumn: "PubId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "BreweryId", "Email", "ImgSrc", "ImgTitle", "Location", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "info@prazdroj.cz", "https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/Logo-Assets-1.png", "Pilsner Urquell logo", "U Prazdroje 64/7, 301 00 Plzeň", "Pilsner Urquell", "+420 377 062 111" },
                    { 2, "info@prazdroj.cz", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_3.png", "Gambrinus logo", "U Prazdroje 64/7, 301 00 Plzeň", "Gambrinus", "+420 377 062 111" },
                    { 3, "info@prazdroj.cz", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_4.png", "Velkopopovický Kozel logo", "Ringhofferova 1, 251 69 Velké Popovice", "Velkopopovický Kozel", "+420 377 066 088" },
                    { 4, "info@prazdroj.cz", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_5.png", "Radegast logo", "Nošovice 238, 739 51 Nošovice", "Radegast", "+420 558 602 566" },
                    { 5, "info@prazdroj.cz", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_6.png", "Birell logo", "Nošovice 238, 739 51 Nošovice", "Birell", "+420 558 602 566" },
                    { 6, "info@prazdroj.cz", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_10.png", "Excelent logo", "U Prazdroje 64/7, 301 00 Plzeň", "Excelent", "+420 377 062 111" }
                });

            migrationBuilder.InsertData(
                table: "Kinds",
                columns: new[] { "KindId", "Type" },
                values: new object[,]
                {
                    { 1, "světlé" },
                    { 2, "polotmavé" },
                    { 3, "tmavé" },
                    { 4, "řezané" },
                    { 5, "jiný" }
                });

            migrationBuilder.InsertData(
                table: "Pubs",
                columns: new[] { "PubId", "Location", "Name", "PhoneNumber", "Rating" },
                values: new object[,]
                {
                    { 1, "U Prazdroje 64/7, 301 00 Plzeň", "Na Spilce", "+420 724 617 355", 3.6000000000000001 },
                    { 2, "Moskevská 13, 460 01 Liberec", "Restaurace Plzeňka - Duli", "+420 485 100 738", 3.5 },
                    { 3, "Vinohradská 35/25, 120 00 Praha", "Restaurant Hajnovka", "+420 224 218 386", 4.2000000000000002 },
                    { 4, "Československé armády 3654/7, 466 05 Jablonec nad Nisou", "Motorest H & H Hušek", "+420 483 305 408", 4.7999999999999998 },
                    { 5, "Liberec XIX-Horní Hanychov 153, 46008 Liberec XIX-Horní Hanychov", "Hotel Ještěd", "+420 485 104 291", 4.7999999999999998 },
                    { 6, "Janov nad Nisou 524, 46811 Janov nad Nisou", "Restaurace Prezidentská chata", "+420 604 551 469", 4.2999999999999998 },
                    { 7, "Nádražní 60/114, 15000 Praha 5 - Smíchov", "Plzeňský restaurant Anděl", "+420 608 181 852", 3.8999999999999999 },
                    { 8, "náměstí Republiky 97/9, 30100 Plzeň 3 - Vnitřní Město", "Šenk a restaurace Lékárna", "+420 735 123 648", 4.5999999999999996 }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "BeerId", "AlcoholContent", "BreweryId", "Description", "ImgSrc", "ImgTitle", "KindId", "Name", "PricePerHalfLitre" },
                values: new object[,]
                {
                    { 1, 4.4000000000000004, 1, "Tajemství chuti legendárního spodně kvašeného ležáku s obsahem alkoholu 4,4 % leží především ve vysoce kvalitních surovinách a zachováním původního výrobního postupu. Charakteristickou vůni sladových zrn a vyváženou karamelovou chuť získává pivo trojitým rmutováním, měkká plzeňská voda mu propůjčuje unikátní jemnou chuť, žatecký chmel sametovou hořkost. Vlastní slad pak dodává pivu zlatavou barvu.", "https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/Asset__2-1080x1080.png", "Pilsner Urquell", 1, "Pilsner Urquell", 28.899999999999999 },
                    { 2, 4.0999999999999996, 2, "I mimo hospodu naše desítka spolehlivě osvěží. V každé várce vyvažujeme ten správný poměr vlastního českého sladu a chmelové odrůdy Sládek, aby chutnala stejně od prvního do posledního doušku a vybízela k dalšímu napití.", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__8-1080x1080.png", "Originál 10°", 1, "Originál 10°", 17.899999999999999 },
                    { 3, 4.7999999999999998, 3, "Kozel Mistrův ležák je světlý ležák s plnějším tělem a vyšším podílem karamelových sladů. Obsahuje 4,8% obj.", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__18-1080x1080.png", "Mistrův ležák", 1, "Mistrův ležák", 18.899999999999999 },
                    { 4, 5.0999999999999996, 4, "Radegast Ryze Hořká 12 je třikrát chmelený prémiový světlý ležák, s 5,1% obsahem alkoholu, který vyniká plnou hořkou chutí (36 IBU) a výrazným chmelovým aroma. Vaří se ze tří vybraných druhů moravského chmele. Kromě odrůd Žatecký poloraný červeňák a Sládek obsahuje i českou odrůdu Žatecký pozdní, která je díky své vysoké kvalitě považována za budoucnost českého pivovarnictví.", "https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/ryze-horka-12-1080x1080.png", "Ryze hořká 12", 1, "Ryze hořká 12", 20.899999999999999 },
                    { 5, 0.5, 5, "Světlý Birell okouzluje plností chuti i příjemnou hořkostí dosahující podmanivé chuti alkoholických piv. Od svého zrodu posbíral Birell celou řadu pivních cen, mezi nimi i ocenění Nejlepší nealkoholické pivo roku na World Beer Awards 2012. V Česku byl v roce 2015 oceněn 1. místem v soutěži Zlatý pohár PIVEX 2015 v kategorii nealkoholické pivo. Neobsahuje konzervanty, umělá barviva ani umělá sladidla.", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__30-1080x1080.png", "Světlý", 1, "Světlý", 15.9 },
                    { 6, 4.7000000000000002, 6, "Excelentní jedenáctistupňový ležák byl poprvé uvařen v Plzni v roce 2008. Obsahuje 4,7 % alkoholu a díky unikátnímu trojitému chmelení, při němž plzeňští sládci používají odrůdu toho nejkvalitnějšího žateckého chmele – Žatecký poloraný červeňák, vyniká plnou chmelovou chutí. Každý doušek tak ve vás oživí chuť se napít znovu.", "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__66-1080x1080.png", "Excelent", 1, "Excelent", 16.899999999999999 }
                });

            migrationBuilder.InsertData(
                table: "BeerPubs",
                columns: new[] { "BeerId", "PubId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 2 },
                    { 5, 3 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 6, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerPubs_PubId",
                table: "BeerPubs",
                column: "PubId");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BreweryId",
                table: "Beers",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_KindId",
                table: "Beers",
                column: "KindId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerPubs");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "Pubs");

            migrationBuilder.DropTable(
                name: "Breweries");

            migrationBuilder.DropTable(
                name: "Kinds");
        }
    }
}
