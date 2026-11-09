# BillerFrontEnd

Blazor Server UI for the Biller API. One owner, runs in docker compose next to Biller.

## Stack
- .NET 10 Blazor with interactive server rendering and QuickGrid. No Fluent UI: the old
  `Microsoft.Fast.Components.FluentUI` package was removed in BIL-94 (no component used it).
- All API calls go through the `BillerContracts.Clients` NuGet package (`src/FrontEnd/FrontEnd.csproj`),
  configured by `ClientsOptions`: `appsettings.LocalDocker.json` (`make run`, inside compose) uses
  `http://webapi:8070/...`; every other environment inherits `http://127.0.0.1:8090/...` (the API's
  host port) from `appsettings.json`.
- **AutoMapper is not allowed** (in any Biller repo), nor any other mapping library. The existing
  profiles in `src/FrontEnd/MappingProfiles/` are to be replaced by hand-written `ToUpdateRequest()`
  extension methods (plan: `docs/plans/BIL-91-remove-automapper.md`). Don't add maps or
  `IMapper` injections. Write an extension method that sets every property instead.
- `src/Domain`: `AppFilter` and `NotFoundException`. API errors arrive as `ClientAPIException` with an
  `ErrorModel` from BillerContracts; `AppErrorBoundary` (`Components/Layout/`) shows them in place of the
  page, with the text from `Capabilities/ErrorMessages.cs`.

## Where things go
- Pages: `src/FrontEnd/Components/Pages/<Resource>/`, named `<Resource>s.razor` (list), `<Resource>s_add`,
  `<Resource>s_id`, `<Resource>s_update`. A list of a parent's rows goes on the parent's `_id` page.
- Layout: `src/FrontEnd/Components/Layout/`.
- Server-paged grids: a `GridPaging` (`Capabilities/`) per grid with `PageSizeSelect`, and one
  `PagedGridsUrl` per page that keeps every grid's page and page size in the URL, as in `Invoices.razor`:
  `[SupplyParameterFromQuery]` properties (without them a query-only change doesn't reach the page),
  `GridPaging.Apply` in `OnParametersSetAsync` when `UrlChanged()`, `Rendered` after the first render,
  `Write` in each `ItemsProvider`.

## Conventions
- Call the API only through the clients; don't build URLs in pages.
- Biller owns validation and stored totals. A total shown on a page must match the API's `TotalPrice`.
  The one exception is `Invoices_update.razor`: it shows the total after saving, from the current
  item prices, because `PUT /invoices` copies line names and prices from the items (BIL-86).
- There are no tests yet (BIL-87), so check changed pages in the browser.
- Don't add comments (Razor `@* *@`, HTML `<!-- -->`, C# `//`) or hint text on pages without asking the
  owner first.
- A handler that must not run while data loads, or without a required value, checks that itself
  (`if (...) return;`). `disabled` on a button only changes what the user sees: a click can reach the
  server before the disabled render does.
- Plans for work only in this repo go in `docs/plans/BIL-<n>-<slug>.md` here. Plans that change
  several repos live in Biller's `docs/plans/`.
- Before reading, comparing or using another Biller repo, put it on the branch with this branch's
  name, else on up-to-date master:
  `bash ../Biller/.claude/skills/_sync-repos/scripts/sync-repos.sh --branch "$(git branch --show-current)" Biller BillerContracts BillioIntegrationTest`
  (add `--sync` to switch and fast-forward).

## Package limits
Every Biller repo has these limits. Keep them when updating packages.
- Target `net10.0`. A package whose newest version needs a newer .NET stays on the last version that
  supports `net10.0`, until all four repos move .NET together.
- QuestPDF: Community licence only (`LicenseType.Community`). Before an update, check that the
  business still qualifies (under USD 1M revenue, not public sector or publicly traded) and read the
  new version's licence.
- Assertions: AwesomeAssertions (Apache 2.0). Don't add FluentAssertions back: v8+ needs a paid
  licence for commercial use.
- Moq: below 4.20.0, pinned as `[4.18.4,4.20.0)` (4.20.0 added SponsorLink build-time telemetry).
- xUnit: `xunit.v3.mtp-off`, not `xunit.v3`. The .NET 10 SDK's `dotnet test` doesn't run xUnit's
  default Microsoft.Testing.Platform runner through VSTest, and the test scripts rely on VSTest.
- Before taking a new major version, read its release notes and check its licence.
- This repo only: AutoMapper stays at 13.0.1 until BIL-91 removes it. Don't update it: 15.0 and later
  need a commercial licence. 13.0.1 has a known high-severity advisory (NU1903,
  GHSA-rvv3-g6hj-g44x), one more reason to finish BIL-91.

## Running
Start Biller first (`docker compose up -d --build` in `../Biller`): this compose file joins Biller's
`billio_network`. Then `docker compose up -d --build` here (or `make run`). UI at http://localhost:8080.

## Before a PR
1. `dotnet build BillerFrontEnd.sln -c Release`. Paste the `Build succeeded` / error lines.
2. Open each changed page against a running Biller and say what you checked.
3. Run `/code-review` on the branch and tell it to follow `REVIEW.md`. Fix Important findings,
   answer the rest, at most 3 rounds.
4. Fill `.github/pull_request_template.md`.
