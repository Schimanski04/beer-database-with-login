using BeerDatabase.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BeerDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Pub> Pubs { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<BeerPub> BeerPubs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // fluent pro povinnou vazbu
            // 1:N
            builder.Entity<Brewery>()
                .HasMany(br => br.Beers)
                .WithOne(be => be.Brewery)
                .IsRequired();

            builder.Entity<Kind>()
                .HasMany(k => k.Beers)
                .WithOne(b => b.Kind)
                .IsRequired();

            // N:M
            builder.Entity<Beer>()
                .HasMany(left => left.Pubs)
                .WithMany(right => right.Beers)
                .UsingEntity<BeerPub>(
                    right => right.HasOne(e => e.Pub).WithMany(),
                    left => left.HasOne(e => e.Beer).WithMany().HasForeignKey(e => e.BeerId),
                    join => join.ToTable("BeerPubs"));


            // Default data
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "", Name = "", NormalizedName = "", ConcurrencyStamp = "" });

            builder.Entity<Kind>().HasData(new Kind { KindId = 1, Type = "světlé" },
                new Kind { KindId = 2, Type = "polotmavé" },
                new Kind { KindId = 3, Type = "tmavé" },
                new Kind { KindId = 4, Type = "řezané" },
                new Kind { KindId = 5, Type = "jiný" });

            builder.Entity<Brewery>().HasData(new Brewery { BreweryId = 1, Name = "Pilsner Urquell", Location = "U Prazdroje 64/7, 301 00 Plzeň", PhoneNumber = "+420 377 062 111", Email = "info@prazdroj.cz", ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/Logo-Assets-1.png", ImgTitle = "Pilsner Urquell logo" },
                new Brewery { BreweryId = 2, Name = "Gambrinus", Location = "U Prazdroje 64/7, 301 00 Plzeň", PhoneNumber = "+420 377 062 111", Email = "info@prazdroj.cz", ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_3.png", ImgTitle = "Gambrinus logo" },
                new Brewery { BreweryId = 3, Name = "Velkopopovický Kozel", Location = "Ringhofferova 1, 251 69 Velké Popovice", PhoneNumber = "+420 377 066 088", Email = "info@prazdroj.cz", ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_4.png", ImgTitle = "Velkopopovický Kozel logo" },
                new Brewery { BreweryId = 4, Name = "Radegast", Location = "Nošovice 238, 739 51 Nošovice", PhoneNumber = "+420 558 602 566", Email = "info@prazdroj.cz", ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_5.png", ImgTitle = "Radegast logo" },
                new Brewery { BreweryId = 5, Name = "Birell", Location = "Nošovice 238, 739 51 Nošovice", PhoneNumber = "+420 558 602 566", Email = "info@prazdroj.cz", ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_6.png", ImgTitle = "Birell logo" },
                new Brewery { BreweryId = 6, Name = "Excelent", Location = "U Prazdroje 64/7, 301 00 Plzeň", PhoneNumber = "+420 377 062 111", Email = "info@prazdroj.cz", ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Logo-Assets_10.png", ImgTitle = "Excelent logo" });

            builder.Entity<Beer>().HasData(new Beer { BeerId = 1, BreweryId = 1, KindId = 1, Name = "Pilsner Urquell", Description = "Tajemství chuti legendárního spodně kvašeného ležáku s obsahem alkoholu 4,4 % leží především ve vysoce kvalitních surovinách a zachováním původního výrobního postupu. Charakteristickou vůni sladových zrn a vyváženou karamelovou chuť získává pivo trojitým rmutováním, měkká plzeňská voda mu propůjčuje unikátní jemnou chuť, žatecký chmel sametovou hořkost. Vlastní slad pak dodává pivu zlatavou barvu.", AlcoholContent = 4.4, PricePerHalfLitre = 28.90, ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/Asset__2-1080x1080.png", ImgTitle = "Pilsner Urquell" },
                new Beer { BeerId = 2, BreweryId = 2, KindId = 1, Name = "Originál 10°", Description = "I mimo hospodu naše desítka spolehlivě osvěží. V každé várce vyvažujeme ten správný poměr vlastního českého sladu a chmelové odrůdy Sládek, aby chutnala stejně od prvního do posledního doušku a vybízela k dalšímu napití.", AlcoholContent = 4.1, PricePerHalfLitre = 17.90, ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__8-1080x1080.png", ImgTitle = "Originál 10°" },
                new Beer { BeerId = 3, BreweryId = 3, KindId = 1, Name = "Mistrův ležák", Description = "Kozel Mistrův ležák je světlý ležák s plnějším tělem a vyšším podílem karamelových sladů. Obsahuje 4,8% obj.", AlcoholContent = 4.8, PricePerHalfLitre = 18.90, ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__18-1080x1080.png", ImgTitle = "Mistrův ležák" },
                new Beer { BeerId = 4, BreweryId = 4, KindId = 1, Name = "Ryze hořká 12", Description = "Radegast Ryze Hořká 12 je třikrát chmelený prémiový světlý ležák, s 5,1% obsahem alkoholu, který vyniká plnou hořkou chutí (36 IBU) a výrazným chmelovým aroma. Vaří se ze tří vybraných druhů moravského chmele. Kromě odrůd Žatecký poloraný červeňák a Sládek obsahuje i českou odrůdu Žatecký pozdní, která je díky své vysoké kvalitě považována za budoucnost českého pivovarnictví.", AlcoholContent = 5.1, PricePerHalfLitre = 20.90, ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2016/03/ryze-horka-12-1080x1080.png", ImgTitle = "Ryze hořká 12" },
                new Beer { BeerId = 5, BreweryId = 5, KindId = 1, Name = "Světlý", Description = "Světlý Birell okouzluje plností chuti i příjemnou hořkostí dosahující podmanivé chuti alkoholických piv. Od svého zrodu posbíral Birell celou řadu pivních cen, mezi nimi i ocenění Nejlepší nealkoholické pivo roku na World Beer Awards 2012. V Česku byl v roce 2015 oceněn 1. místem v soutěži Zlatý pohár PIVEX 2015 v kategorii nealkoholické pivo. Neobsahuje konzervanty, umělá barviva ani umělá sladidla.", AlcoholContent = 0.5, PricePerHalfLitre = 15.90, ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__30-1080x1080.png", ImgTitle = "Světlý" },
                new Beer { BeerId = 6, BreweryId = 6, KindId = 1, Name = "Excelent", Description = "Excelentní jedenáctistupňový ležák byl poprvé uvařen v Plzni v roce 2008. Obsahuje 4,7 % alkoholu a díky unikátnímu trojitému chmelení, při němž plzeňští sládci používají odrůdu toho nejkvalitnějšího žateckého chmele – Žatecký poloraný červeňák, vyniká plnou chmelovou chutí. Každý doušek tak ve vás oživí chuť se napít znovu.", AlcoholContent = 4.7, PricePerHalfLitre = 16.90, ImgSrc = "https://www.prazdroj.cz/cospospohzeg/uploads/2021/08/Asset__66-1080x1080.png", ImgTitle = "Excelent" });

            builder.Entity<Pub>().HasData(new Pub { PubId = 1, Name = "Na Spilce", Location = "U Prazdroje 64/7, 301 00 Plzeň", PhoneNumber = "+420 724 617 355", Rating = 3.6 },
                new Pub { PubId = 2, Name = "Restaurace Plzeňka - Duli", Location = "Moskevská 13, 460 01 Liberec", PhoneNumber = "+420 485 100 738", Rating = 3.5 },
                new Pub { PubId = 3, Name = "Restaurant Hajnovka", Location = "Vinohradská 35/25, 120 00 Praha", PhoneNumber = "+420 224 218 386", Rating = 4.2 },
                new Pub { PubId = 4, Name = "Motorest H & H Hušek", Location = "Československé armády 3654/7, 466 05 Jablonec nad Nisou", PhoneNumber = "+420 483 305 408", Rating = 4.8 },
                new Pub { PubId = 5, Name = "Hotel Ještěd", Location = "Liberec XIX-Horní Hanychov 153, 46008 Liberec XIX-Horní Hanychov", PhoneNumber = "+420 485 104 291", Rating = 4.8 },
                new Pub { PubId = 6, Name = "Restaurace Prezidentská chata", Location = "Janov nad Nisou 524, 46811 Janov nad Nisou", PhoneNumber = "+420 604 551 469", Rating = 4.3 },
                new Pub { PubId = 7, Name = "Plzeňský restaurant Anděl", Location = "Nádražní 60/114, 15000 Praha 5 - Smíchov", PhoneNumber = "+420 608 181 852", Rating = 3.9 },
                new Pub { PubId = 8, Name = "Šenk a restaurace Lékárna", Location = "náměstí Republiky 97/9, 30100 Plzeň 3 - Vnitřní Město", PhoneNumber = "+420 735 123 648", Rating = 4.6 });

            builder.Entity<BeerPub>().HasData(new BeerPub { BeerId = 1, PubId = 1 },
                new BeerPub { BeerId = 2, PubId = 2 },
                new BeerPub { BeerId = 3, PubId = 2 },
                new BeerPub { BeerId = 4, PubId = 2 },
                new BeerPub { BeerId = 6, PubId = 2 },
                new BeerPub { BeerId = 1, PubId = 3 },
                new BeerPub { BeerId = 3, PubId = 3 },
                new BeerPub { BeerId = 5, PubId = 3 },
                new BeerPub { BeerId = 1, PubId = 4 },
                new BeerPub { BeerId = 1, PubId = 5 },
                new BeerPub { BeerId = 5, PubId = 5 },
                new BeerPub { BeerId = 1, PubId = 6 },
                new BeerPub { BeerId = 5, PubId = 6 },
                new BeerPub { BeerId = 1, PubId = 7 },
                new BeerPub { BeerId = 5, PubId = 7 },
                new BeerPub { BeerId = 1, PubId = 8 });
        }
    }
}
