# BillerFrontEnd

[English](../README.md)

Biller žiniatinklio naudotojo sąsaja: Blazor Server programa naudotojams, pardavėjams, sąskaitų
serijoms, pirkėjams, prekėms ir sąskaitoms tvarkyti, įskaitant sąskaitų faktūrų ir aktų PDF
dokumentų atsisiuntimą. Savos duomenų bazės ji neturi. Kiekvienas puslapis duomenis skaito ir
rašo per Biller API.

Šis failas yra [README.md](../README.md) vertimas. Komandos, keliai ir pavadinimai palikti tokie,
kokie yra angliškame faile.

## Keturios repozitorijos

| Repozitorija | Kas tai |
|---|---|
| [Biller](https://github.com/aldisadom/Biller) | API, duomenų bazės migracijos ir PDF dokumentai |
| [BillerContracts](https://github.com/aldisadom/BillerContracts) | Užklausų ir atsakymų įrašai, išvardijimai (enum) ir tipizuoti HTTP klientai, skelbiami NuGet paketais `BillerContracts` ir `BillerContracts.Clients` |
| [BillerFrontEnd](https://github.com/aldisadom/BillerFrontEnd) | Blazor naudotojo sąsaja. API ji kviečia per `BillerContracts.Clients` |
| [BillioIntegrationTest](https://github.com/aldisadom/BillioIntegrationTest) | Testai, kurie kviečia veikiančią API per HTTP |

## Ko reikia

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) su Docker Compose
- Veikianti Biller API

## Paleidimas

Pirmiausia paleiskite Biller: šis compose failas prisijungia prie Biller tinklo `billio_network`,
kurį sukuria Biller compose failas.

```sh
docker compose up -d --build    # in ../Biller
docker compose up -d --build    # here, or: make run
```

Naudotojo sąsaja veikia adresu http://localhost:8080.

`make update` skirta kompiuteriui, kuriame sistema įdiegta ir kuriame aktyvi `master` šaka: ji
atstato aktyvią šaką į `origin/master` (sekamų failų pakeitimai ir vietiniai įrašai (commit)
prarandami, kaip ir failas, kurio git neseka, jei jo kelią seka `origin/master` arba vietinė
`master`), persijungia į `master` ir iš naujo paleidžia konteinerį. Kiti failai, kurių git neseka,
lieka. Toks failas projekte aplanke `src/` įtraukiamas į sąsają (pvz., `.razor` ar `.cs` failas
sukompiliuojamas), nebent `.dockerignore` jį praleidžia (`bin/`, `obj/` ir kt.). Žinoma problema:
ji atstato prieš atsisiųsdama (fetch), todėl įdiegia tą `master` šaką, kuri buvo atsisiųsta
praėjusį kartą, ir tik antras paleidimas gauna naujausią. Kitoje
šakoje pirmas paleidimas atstato tą šaką ir įdiegia vietinę `master` tokią, kokia ji yra.

Be Docker, paleista su `dotnet run --project src/FrontEnd`, sąsaja veikia adresu
http://localhost:5115 (`src/FrontEnd/Properties/launchSettings.json`) ir naudoja toliau nurodytus
`appsettings.json` adresus, todėl Biller vis tiek turi atsakyti per prievadą 8090, kurį naudoja
Biller Docker Compose rinkinys.

## Kompiliavimas ir testai

```sh
dotnet build BillerFrontEnd.sln -c Release
dotnet test BillerFrontEnd.sln --no-build -c Release
```

Testai tikrina `MappingProfiles/` metodus, kurie atsakymą paverčia atnaujinimo užklausa. Puslapiai testų dar neturi, todėl pakeistas puslapis
tikrinamas naršyklėje, kai veikia Biller.

## Konfigūracija

API adresai yra `ClientsOptions` sekcijoje, po vieną URL kiekvienam ištekliui:

- `src/FrontEnd/appsettings.LocalDocker.json` (Docker Compose): `http://webapi:8070/...`, API
  konteineris bendrame tinkle.
- `src/FrontEnd/appsettings.json` (visos kitos aplinkos): `http://127.0.0.1:8090/...`, API
  prievadas pagrindiniame kompiuteryje.

## Kur kas yra

- `src/FrontEnd/Components/Pages/`: po vieną aplanką kiekvienam ištekliui su jo puslapiais: sąrašu
  (`Customers.razor`), `_add`, `_id` (išsami informacija) ir `_update`. Aplankų pavadinimai
  nevienodi (`Customer/`, `Invoice/`, `Seller/`, bet `Items/`, `Users/`, `Series/`); failų
  pavadinimai prasideda daugiskaita. `Series/` turi tik pridėjimo ir atnaujinimo puslapius: serijų
  sąrašas yra pardavėjo išsamios informacijos puslapyje. `Users/` dar turi prisijungimo puslapį, o
  `Invoice/` – `InvoiceStatusLabel.razor`, komponentą, kurį naudoja keli puslapiai. `Home.razor` ir
  `Error.razor` yra šalia aplankų.
- `src/FrontEnd/Components/Layout/`: maketas ir klaidų rodymas.
- `src/FrontEnd/MappingProfiles/`: rankomis parašyti metodai, kurie atsakymą paverčia atnaujinimo
  užklausa.
- `src/FrontEnd/Capabilities/`: puslapiavimas, klaidų tekstai ir pagalbiniai klientų metodai.
- `src/Domain`: `AppFilter` ir `NotFoundException`.
- `tests/FrontEnd.Tests`: moduliniai testai.

Susitarimai, paketų apribojimai ir peržiūros taisyklės yra [CLAUDE.md](../CLAUDE.md) ir
[REVIEW.md](../REVIEW.md) failuose.

## Licencija

MIT, žr. [LICENSE.txt](../LICENSE.txt).
