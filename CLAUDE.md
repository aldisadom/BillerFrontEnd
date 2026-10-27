# BillerFrontEnd

Blazor Server UI for the Biller API. One owner, runs in docker compose next to Biller.

## Stack
- .NET 8 Blazor with interactive server rendering, QuickGrid and Fluent UI.
- All API calls go through the `BillerContracts.Clients` NuGet package (`src/FrontEnd/FrontEnd.csproj`),
  configured by `ClientsOptions` in `appsettings*.json` (`http://webapi:8070/...` inside compose).
- **AutoMapper is not allowed** (in any Biller repo), nor any other mapping library. The existing
  profiles in `src/FrontEnd/MappingProfiles/` are to be replaced by hand-written `ToUpdateRequest()`
  extension methods (plan: `docs/plans/BIL-91-remove-automapper.md`). Don't add maps or
  `IMapper` injections. Write an extension method that sets every property instead.
- `src/Domain`: `AppFilter` and `NotFoundException`. API errors arrive as `ClientAPIException` with an
  `ErrorModel` from BillerContracts; `AppErrorBoundary` (`Components/Layout/`) shows them in place of the
  page, with the text from `Capabilities/ErrorMessages.cs`.

## Where things go
- Pages: `src/FrontEnd/Components/Pages/<Resource>/`, named `<Resource>s.razor` (list), `<Resource>s_add`,
  `<Resource>s_id`, `<Resource>s_update`, plus filters such as `Items_customerId.razor`.
- Layout: `src/FrontEnd/Components/Layout/`.

## Conventions
- Call the API only through the clients; don't build URLs in pages.
- Biller owns validation and stored totals. A total shown on a page must match the API's `TotalPrice`.
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

## Running
Start Biller first (`docker compose up -d --build` in `../Biller`): this compose file joins Biller's
`billio_network`. Then `docker compose up -d --build` here (or `make run`). UI at http://localhost:8080.

## Before a PR
1. `dotnet build BillerFrontEnd.sln -c Release`. Paste the `Build succeeded` / error lines.
2. Open each changed page against a running Biller and say what you checked.
3. Run `/code-review` on the branch and tell it to follow `REVIEW.md`. Fix Important findings,
   answer the rest, at most 3 rounds.
4. Fill `.github/pull_request_template.md`.
