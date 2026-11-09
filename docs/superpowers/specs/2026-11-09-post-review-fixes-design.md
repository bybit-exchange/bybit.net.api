# Post-review SDK Fixes Design

## Scope

Resolve all actionable findings from the final review of the `doris-net` branch:

1. Remove the ambiguous Spot-X constructor overloads.
2. Allow `TransferSubMemberSave` to send multiple sub-member IDs.
3. Keep every `X-BAPI-*` authentication header off public requests while preserving signed-request headers.
4. Replace the newly added weak request collections with documented element types without removing existing weak overloads.
5. Add local contract coverage for the newly added Event, Referral, Tax, and Pre-upgrade services.
6. Put the changelog title and introduction before release entries.

No response-model redesign, validation framework, live API request, or unrelated generated-code cleanup is included.

## API design

### Spot-X constructors

Reorder the public-only constructor parameters from `url, recvWindow, debugMode` to `url, debugMode, recvWindow`, including the `HttpClient` variant. This keeps parameterless construction, positional `url` construction, and existing named arguments working while ensuring that `(apiKey, apiSecret)` and `(httpClient, apiKey, apiSecret)` resolve only to authenticated constructors.

### Transferable sub-members

Keep the single-string `TransferSubMemberSave` entry point for source compatibility and add a collection overload. The string overload delegates to the collection overload as a one-element request. The collection overload serializes each ID as its own element in `subMemberIds`.

### Public request headers

Move `X-BAPI-SIGN-TYPE`, `X-BAPI-TIMESTAMP`, and `X-BAPI-RECV-WINDOW` into the same signed-request branch as `X-BAPI-SIGN` and `X-BAPI-API-KEY`. Public requests retain only non-authentication headers such as `User-Agent`. Signed JSON and multipart requests retain the full authentication header set.

### Typed request collections

Add request item models for the documented object arrays:

- `DemoApplyMoneyItem`: `coin`, `amountStr`.
- `TaxReportItem`: `type`, `number`.

Add strongly typed overloads while preserving the existing `List<object>` overloads. Replace the Alpha scalar object collections with typed collection overloads while retaining their existing weak overloads:

- LP order statuses: integers.
- LP token codes: strings.
- Prediction token IDs: strings.

All overloads must serialize to the same documented JSON field names.

## Tests

Follow red-green-refactor for each behavior:

- Compile and execute the common two-string and `HttpClient` plus two-string Spot-X authenticated constructors.
- Assert public Spot-X requests contain no header whose name starts with `X-BAPI-`.
- Assert signed requests still contain the complete five-header authentication set.
- Assert one and multiple sub-member IDs serialize as JSON arrays with separate elements.
- Assert typed Demo, Tax, and Alpha inputs serialize with the documented names and primitive types.
- Exercise every new Event, Referral, Tax, and Pre-upgrade method against a recording handler, checking HTTP method, path, query, and body as applicable.

All tests are local and use synthetic credentials. After targeted tests pass, run the complete non-integration suite, `git diff --check main`, and a consumer compilation check for the Spot-X overloads.

## Documentation and compatibility

Move `# Changelog` and its introduction to the beginning of `CHANGELOG.md`; keep release contents unchanged. Changes to Spot-X positional `recvWindow`/`debugMode` arguments affect only staged, unpublished code, and named arguments remain compatible. Existing weak request overloads remain available, so released caller source is not intentionally broken.

No user-provided API key or secret is stored, logged, or used.
