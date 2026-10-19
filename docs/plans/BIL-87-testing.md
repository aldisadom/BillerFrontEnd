# Plan: Billio front-end testing

Jira: [BIL-87](https://aldisadom.atlassian.net/browse/BIL-87). Epic: BIL-89 (Billeris).

## Goal

- The front end has automated tests that run in CI on every PR.
- Page logic (loading, mapping, button actions, navigation, errors) is tested without a running API.
- The mapping extension methods are tested. The HTTP clients come from the `BillerContracts.Clients` package and are tested there (`Clients.Tests`), so this repo doesn't test them again.
- AutoMapper is removed first (see [BIL-91-remove-automapper.md](BIL-91-remove-automapper.md)); it is not allowed in any Biller repo.
- Later, a few end-to-end tests cover the main flows against the real stack.

## Current state (BillerFrontEnd master @ 44f9223)

| Where | What |
|---|---|
| `src/FrontEnd` | Blazor Server (.NET 8, `InteractiveServer`), FluentUI 3.7.8, QuickGrid, AutoMapper 13 (to be removed) |
| Pages | 24 `.razor` pages under `Components/Pages/{Customer,Invoice,Items,Seller,Users}`. Largest: `Invoices_update` (288 lines), `Invoices_add` (272) |
| API clients | `BillerContracts.Clients` 1.1.1 NuGet package, tested in BillerContracts (`Clients.Tests`). Pages inject its `I*Client` interfaces, so they're easy to mock |
| `MappingProfiles` | One AutoMapper profile per entity, to be replaced by hand-written extension methods |
| Tests | None |
| CI (`.github/workflows/build.yml`) | Restore and build only |
| Claude skills | None in this repo |

## Design decisions

1. **bUnit for component tests.** It renders Razor components in memory, lets tests click buttons and read the markup, and has a fake `NavigationManager`. No browser needed.
2. **Same test conventions as Biller** (see Biller's `_write-tests` skill): xUnit, strict Moq mocks with `Verify`, hand-built test data, theories with a `Label`. Check that the bUnit version supports xUnit v3; fall back to xUnit v2 in this repo if not.
3. **End-to-end tests come later and use Playwright for .NET**, against `docker compose` (Biller + front end). They go in a separate project so the fast tests stay fast.
4. **Test the behaviour, not the markup.** Assert on what is shown (names, prices, totals) and what was called, not on exact HTML.

## Step 1: Test project

- `tests/FrontEnd.Tests/FrontEnd.Tests.csproj`: xUnit, `bunit`, `Moq`, `coverlet.collector`. References `FrontEnd`.
- Add it to `BillerFrontEnd.sln`.
- A small base class that registers mocks for every `I*Client` and the FluentUI services, so each test only sets up what it uses.

## Step 2: Mapping methods (quick win)

- Done together with BIL-91 ([BIL-91-remove-automapper.md](BIL-91-remove-automapper.md)): one test per `ToUpdateRequest()` method, checking every property, like Biller's `MappingTestHelper`.
- No AutoMapper configuration tests: AutoMapper is not allowed.

## Step 3: Pages (bUnit), in priority order

| Page | Cases |
|---|---|
| `Invoices_add` | Loads sellers/customers/items, adding and removing lines, total, submit calls `IInvoiceClient.Add` with the right request, navigation after save, API error is shown |
| `Invoices_update` | Loads the invoice, edits a line, submit sends `InvoiceUpdateRequest`, delete |
| `Users_login` | Wrong password shows an error, success navigates |
| `*_update` pages (Items, Customers, Sellers, Users) | Invalid id string throws `NotFoundException`, loads and binds values, Update calls the client and navigates, Delete calls the client and navigates |
| `*_add` pages | Submit sends the filled request |
| List pages (`Items`, `Invoices`, …) | Paging calls the client with the right page, empty list |

Writing these may show logic worth moving out of the `.razor` files into small classes (e.g. invoice line totals). Do that only where a test needs it.

## Step 4: CI

Add to `build.yml` after Build:
```yaml
      - name: Test
        run: dotnet test BillerFrontEnd.sln --no-build --configuration Release --collect:"XPlat Code Coverage"
```

## Step 5: Claude skills for this repo

- Copy and adapt Biller's `_write-tests` and `_test-coverage` skills into `.claude/skills/`, with a bUnit section (rendering, `Find`/`Click`, `FakeNavigationManager`, waiting for async rendering).

## Step 6: End-to-end tests (later phase)

- `tests/FrontEnd.E2E` with `Microsoft.Playwright`.
- Flows: log in → create seller → customer → item → invoice → download PDF; edit item and see the change.
- CI job that starts Biller (`docker-compose.ci.yml`), the front end, runs migrations, then the tests. Could share setup with the BillioIntegrationTest repo.

## Out of scope

- Visual/screenshot comparison.
- Rewriting pages for testability beyond what tests need.

## Order of work

1. Test project, AutoMapper removal with its mapping tests (BIL-91, [BIL-91-remove-automapper.md](BIL-91-remove-automapper.md)), CI step (one PR, so CI runs tests from the start).
2. `Invoices_add` and `Invoices_update`.
3. The other pages.
4. Skills.
5. E2E.
