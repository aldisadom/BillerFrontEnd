# BillerFrontEnd

[Lietuviškai](docs/README_lt.md)

The web UI of Biller: a Blazor Server app for users, sellers, invoice series, customers, items and
invoices, including downloading invoice and act PDFs. It has no database of its own. Every page
reads and writes through the Biller API.

## The four repos

| Repo | What it is |
|---|---|
| [Biller](https://github.com/aldisadom/Biller) | The API, the database migrations and the PDFs |
| [BillerContracts](https://github.com/aldisadom/BillerContracts) | Request and response records, enums and typed HTTP clients, published as the `BillerContracts` and `BillerContracts.Clients` NuGet packages |
| [BillerFrontEnd](https://github.com/aldisadom/BillerFrontEnd) | The Blazor UI. It calls the API through `BillerContracts.Clients` |
| [BillioIntegrationTest](https://github.com/aldisadom/BillioIntegrationTest) | Tests that call a running API over HTTP |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) with Docker Compose
- A running Biller API

## Run

Start Biller first: this compose file joins Biller's `billio_network`, which Biller's compose creates.

```sh
docker compose up -d --build    # in ../Biller
docker compose up -d --build    # here, or: make run
```

The UI is at http://localhost:8080.

`make update` is for the PC the app is deployed on, with `master` checked out: it resets the
checked-out branch to `origin/master` (changes to tracked files and local commits are lost, and so
is an untracked file at a path `origin/master` or the local `master` tracks), switches to `master`
and starts the container again. Other untracked files stay. One in a project under `src/` is built
into the UI (an untracked `.razor` or `.cs` file is compiled in), unless `.dockerignore` leaves it
out (`bin/`, `obj/` and others). Known problem: it resets before it fetches, so it deploys
the master that was fetched the time before, and only a second run gets the newest one. On another
branch, the first run resets that branch and deploys the local `master` as it is.

Without Docker, `dotnet run --project src/FrontEnd` serves http://localhost:5115
(`src/FrontEnd/Properties/launchSettings.json`) and uses the `appsettings.json` addresses below, so
Biller still has to answer on port 8090, which is its Docker Compose stack.

## Build and test

```sh
dotnet build BillerFrontEnd.sln -c Release
dotnet test BillerFrontEnd.sln --no-build -c Release
```

The tests cover the mapping methods. Pages have no tests yet, so a changed page is checked in the
browser against a running Biller.

## Configuration

The API's addresses are in `ClientsOptions`, one URL per resource:

- `src/FrontEnd/appsettings.LocalDocker.json` (Docker Compose): `http://webapi:8070/...`, the API's
  container on the shared network.
- `src/FrontEnd/appsettings.json` (every other environment): `http://127.0.0.1:8090/...`, the API's
  port on the host.

## Where things go

- `src/FrontEnd/Components/Pages/`: one folder per resource with its pages: the list
  (`Customers.razor`), `_add`, `_id` (details) and `_update`. The folder names aren't uniform
  (`Customer/`, `Invoice/`, `Seller/`, but `Items/`, `Users/`, `Series/`); the files start with the
  plural. `Series/` has only the add and update pages: the series list is on the seller's details
  page. `Users/` also has the login page, and `Invoice/` has `InvoiceStatusLabel.razor`, a component
  the pages share. `Home.razor` and `Error.razor` are beside the folders.
- `src/FrontEnd/Components/Layout/`: the layout and the error display.
- `src/FrontEnd/MappingProfiles/`: hand-written methods that turn a response into an update request.
- `src/FrontEnd/Capabilities/`: paging, error texts and client helpers.
- `src/Domain`: `AppFilter` and `NotFoundException`.
- `tests/FrontEnd.Tests`: unit tests.

The conventions, package limits and review rules are in [CLAUDE.md](CLAUDE.md) and [REVIEW.md](REVIEW.md).

## Licence

MIT, see [LICENSE.txt](LICENSE.txt).
