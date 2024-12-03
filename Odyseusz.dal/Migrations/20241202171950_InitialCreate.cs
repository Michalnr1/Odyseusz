using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Odyseusz.dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kraje",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KodKraju = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kraje", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "Adresy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miejscowosc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrDomu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrLokalu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NazwaKraju = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresy_Kraje_NazwaKraju",
                        column: x => x.NazwaKraju,
                        principalTable: "Kraje",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DaneOsobowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaneOsobowe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaneOsobowe_Adresy_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adresy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZgloszeniaPodrozy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZgloszenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaneOsoboweId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZgloszeniaPodrozy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZgloszeniaPodrozy_DaneOsobowe_DaneOsoboweId",
                        column: x => x.DaneOsoboweId,
                        principalTable: "DaneOsobowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtapyPodrozy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPrzyjazdu = table.Column<DateOnly>(type: "date", nullable: false),
                    DataWyjazdu = table.Column<DateOnly>(type: "date", nullable: false),
                    OrganizatorPobytu = table.Column<int>(type: "int", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    ZgloszeniePodrozyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapyPodrozy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtapyPodrozy_Adresy_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adresy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EtapyPodrozy_ZgloszeniaPodrozy_ZgloszeniePodrozyId",
                        column: x => x.ZgloszeniePodrozyId,
                        principalTable: "ZgloszeniaPodrozy",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Kraje",
                columns: new[] { "Nazwa", "KodKraju" },
                values: new object[,]
                {
                    { "Afganistan", "AF" },
                    { "Albania", "AL" },
                    { "Algieria", "DZ" },
                    { "Andora", "AD" },
                    { "Angola", "AO" },
                    { "Antigua i Barbuda", "AG" },
                    { "Arabia Saudyjska", "SA" },
                    { "Argentyna", "AR" },
                    { "Armenia", "AM" },
                    { "Australia", "AU" },
                    { "Austria", "AT" },
                    { "Azerbejdżan", "AZ" },
                    { "Bahamy", "BS" },
                    { "Bahrajn", "BH" },
                    { "Bangladesz", "BD" },
                    { "Barbados", "BB" },
                    { "Belgia", "BE" },
                    { "Belize", "BZ" },
                    { "Benin", "BJ" },
                    { "Bhutan", "BT" },
                    { "Białoruś", "BY" },
                    { "Bośnia i Hercegowina", "BA" },
                    { "Botswana", "BW" },
                    { "Brazylia", "BR" },
                    { "Brunei", "BN" },
                    { "Bułgaria", "BG" },
                    { "Burkina Faso", "BF" },
                    { "Burundi", "BI" },
                    { "Chile", "CL" },
                    { "Chiny", "CN" },
                    { "Chorwacja", "HR" },
                    { "Cypr", "CY" },
                    { "Czarnogóra", "ME" },
                    { "Czechy", "CZ" },
                    { "Dania", "DK" },
                    { "Demokratyczna Republika Konga", "CD" },
                    { "Dominika", "DM" },
                    { "Dominikana", "DO" },
                    { "Dżibuti", "DJ" },
                    { "Egipt", "EG" },
                    { "Ekwador", "EC" },
                    { "Erytrea", "ER" },
                    { "Estonia", "EE" },
                    { "Eswatini", "SZ" },
                    { "Etiopia", "ET" },
                    { "Fidżi", "FJ" },
                    { "Filipiny", "PH" },
                    { "Finlandia", "FI" },
                    { "Francja", "FR" },
                    { "Gabon", "GA" },
                    { "Gambia", "GM" },
                    { "Ghana", "GH" },
                    { "Grecja", "GR" },
                    { "Grenada", "GD" },
                    { "Gruzja", "GE" },
                    { "Guatemala", "GT" },
                    { "Gujana", "GY" },
                    { "Gwinea", "GN" },
                    { "Gwinea Bissau", "GW" },
                    { "Gwinea Równikowa", "GQ" },
                    { "Haiti", "HT" },
                    { "Hiszpania", "ES" },
                    { "Holandia", "NL" },
                    { "Honduras", "HN" },
                    { "Indie", "IN" },
                    { "Indonezja", "ID" },
                    { "Irak", "IQ" },
                    { "Iran", "IR" },
                    { "Irlandia", "IE" },
                    { "Islandia", "IS" },
                    { "Izrael", "IL" },
                    { "Jamajka", "JM" },
                    { "Japonia", "JP" },
                    { "Jemen", "YE" },
                    { "Jordania", "JO" },
                    { "Kambodża", "KH" },
                    { "Kamerun", "CM" },
                    { "Kanada", "CA" },
                    { "Katar", "QA" },
                    { "Kazachstan", "KZ" },
                    { "Kenia", "KE" },
                    { "Kirgistan", "KG" },
                    { "Kiribati", "KI" },
                    { "Kolumbia", "CO" },
                    { "Komory", "KM" },
                    { "Kongo", "CG" },
                    { "Korea Południowa", "KR" },
                    { "Korea Północna", "KP" },
                    { "Kostaryka", "CR" },
                    { "Kuba", "CU" },
                    { "Kuwejt", "KW" },
                    { "Laos", "LA" },
                    { "Lesotho", "LS" },
                    { "Liban", "LB" },
                    { "Liberia", "LR" },
                    { "Libia", "LY" },
                    { "Liechtenstein", "LI" },
                    { "Litwa", "LT" },
                    { "Luksemburg", "LU" },
                    { "Łotwa", "LV" },
                    { "Madagaskar", "MG" },
                    { "Malawi", "MW" },
                    { "Malediwy", "MV" },
                    { "Malezja", "MY" },
                    { "Mali", "ML" },
                    { "Malta", "MT" },
                    { "Maroko", "MA" },
                    { "Mauritania", "MR" },
                    { "Mauritius", "MU" },
                    { "Meksyk", "MX" },
                    { "Micronezja", "FM" },
                    { "Mołdawia", "MD" },
                    { "Monako", "MC" },
                    { "Mongolia", "MN" },
                    { "Mozambik", "MZ" },
                    { "Myanmar", "MM" },
                    { "Namibia", "NA" },
                    { "Nauru", "NR" },
                    { "Nepal", "NP" },
                    { "Niemcy", "DE" },
                    { "Niger", "NE" },
                    { "Nigeria", "NG" },
                    { "Nikaragua", "NI" },
                    { "Norwegia", "NO" },
                    { "Nowa Zelandia", "NZ" },
                    { "Oman", "OM" },
                    { "Pakistan", "PK" },
                    { "Palau", "PW" },
                    { "Palestyna", "PS" },
                    { "Panama", "PA" },
                    { "Papua-Nowa Gwinea", "PG" },
                    { "Paragwaj", "PY" },
                    { "Peru", "PE" },
                    { "Polska", "PL" },
                    { "Portugalia", "PT" },
                    { "Republika Południowej Afryki", "ZA" },
                    { "Republika Środkowoafrykańska", "CF" },
                    { "Republika Zielonego Przylądka", "CV" },
                    { "Rosja", "RU" },
                    { "Rumunia", "RO" },
                    { "Rwanda", "RW" },
                    { "Saint Kitts i Nevis", "KN" },
                    { "Saint Lucia", "LC" },
                    { "Saint Vincent i Grenadyny", "VC" },
                    { "Salwador", "SV" },
                    { "Samoa", "WS" },
                    { "San Marino", "SM" },
                    { "Senegal", "SN" },
                    { "Serbia", "RS" },
                    { "Seychele", "SC" },
                    { "Sierra Leone", "SL" },
                    { "Singapur", "SG" },
                    { "Słowacja", "SK" },
                    { "Słowenia", "SI" },
                    { "Somalia", "SO" },
                    { "Sri Lanka", "LK" },
                    { "Stany Zjednoczone", "US" },
                    { "Sudan", "SD" },
                    { "Surinam", "SR" },
                    { "Syria", "SY" },
                    { "Szwajcaria", "CH" },
                    { "Szwecja", "SE" },
                    { "Tadżykistan", "TJ" },
                    { "Tajlandia", "TH" },
                    { "Tanzania", "TZ" },
                    { "Timor Wschodni", "TL" },
                    { "Togo", "TG" },
                    { "Tokelau", "TK" },
                    { "Trynidad i Tobago", "TT" },
                    { "Tunisia", "TN" },
                    { "Turcja", "TR" },
                    { "Turkmenistan", "TM" },
                    { "Tuvalu", "TV" },
                    { "Uganda", "UG" },
                    { "Urugwaj", "UY" },
                    { "Uzbekistan", "UZ" },
                    { "Vanuatu", "VU" },
                    { "Venezuela", "VE" },
                    { "Watykan", "VA" },
                    { "Węgry", "HU" },
                    { "Wielka Brytania", "GB" },
                    { "Wietnam", "VN" },
                    { "Włochy", "IT" },
                    { "Zambia", "ZM" },
                    { "Zimbabwe", "ZW" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresy_NazwaKraju",
                table: "Adresy",
                column: "NazwaKraju");

            migrationBuilder.CreateIndex(
                name: "IX_DaneOsobowe_AdresId",
                table: "DaneOsobowe",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapyPodrozy_AdresId",
                table: "EtapyPodrozy",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapyPodrozy_ZgloszeniePodrozyId",
                table: "EtapyPodrozy",
                column: "ZgloszeniePodrozyId");

            migrationBuilder.CreateIndex(
                name: "IX_ZgloszeniaPodrozy_DaneOsoboweId",
                table: "ZgloszeniaPodrozy",
                column: "DaneOsoboweId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtapyPodrozy");

            migrationBuilder.DropTable(
                name: "ZgloszeniaPodrozy");

            migrationBuilder.DropTable(
                name: "DaneOsobowe");

            migrationBuilder.DropTable(
                name: "Adresy");

            migrationBuilder.DropTable(
                name: "Kraje");
        }
    }
}
