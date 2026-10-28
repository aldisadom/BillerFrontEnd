# Review rules for BillerFrontEnd

Failure here looks like:
- An amount, total, date or status on screen differs from what the API returned. Pages add up line totals themselves (`Invoices_id.razor:103`, `Invoices_add.razor:118`), and those must match Biller's `TotalPrice`. `Invoices_update.razor` is the exception: it shows the total after saving, from the current item prices, as `PUT /invoices` will save it (BIL-86).
- A document is generated in the wrong language or type (the language is fixed to LT in `Invoices_id.razor:160`, BIL-69), or two downloads get the same file name.
- An API error (400, 404) isn't shown to the user, or it ends the Blazor circuit. No page catches `ClientAPIException` today.
- A form sends a field the user didn't set, or drops one. Update requests replace the whole record, so a property a mapping method forgets is saved as empty.
- New AutoMapper code (a profile, a map, an `IMapper` injection) or another mapping library. AutoMapper is being removed (`docs/plans/BIL-91-remove-automapper.md`).
- A page builds an API URL itself instead of using a client.
- Paging or a filter shows the wrong page, or loses the filter after navigation.

Skip: `bin/`, `obj/`, `src/FrontEnd/wwwroot/bootstrap/`.

Tag each finding **Important**, **Nit**, or **Pre-existing** (the bug was there before this change). At most 2 Nits. No naming or formatting comments.

Every finding cites `file:line` and a concrete input that fails. No guesses from names.

Start with the number of Important findings. If there are none, say "No issues found."

When the same kind of bug appears in several places, report it once and list every place.

When a finding repeats across reviews, propose a rule for `CLAUDE.md` or a test instead of repeating it.
