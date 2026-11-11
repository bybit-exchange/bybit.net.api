# SDK Review Fixes Design

## Scope

Fix the three issues identified while reviewing the `doris-net` branch:

1. One-argument P2P file uploads select a JSON overload instead of the existing multipart implementation.
2. Strategy order-list filters use incorrectly capitalized query parameter names.
3. Spot-X public endpoints require callers to supply API credentials during service construction.

No endpoint response typing, validation framework, or unrelated generated service cleanup is included.

## Design

### P2P upload

Remove the newly generated `UploadChatFile(string upload_file)` JSON overload and its duplicate endpoint constant. Keep the existing overloads that accept a `Stream` or a file path. A one-argument file-path call will then resolve to `UploadChatFile(string filePath, string contentType = "application/octet-stream")` and send signed multipart form data.

### Strategy query parameters

Change the public parameter names and outgoing query keys in `QueryStrategyOrderList` to the documented lower-camel-case names: `beginTimeE0`, `endTimeE0`, and `strategyType`. This is a source-level correction to code that has not yet been released.

### Spot-X construction

Add public-only constructors accepting an optional URL, receive window, and debug flag, with both internally-created and caller-provided `HttpClient` variants. Preserve the current authenticated constructors for private Spot-X endpoints. This mirrors existing mixed public/private services such as `BybitEarnService`.

## Tests

Use request-recording HTTP handlers to test observable behavior:

- Calling `UploadChatFile(path)` sends `multipart/form-data`, includes the `upload_file` part and file bytes, and signs the exact multipart body.
- `QueryStrategyOrderList` emits the three documented lower-camel-case query keys and does not emit capitalized variants.
- A public-only Spot-X constructor can call a public endpoint without adding API-key or signature headers.

Each regression test must fail against the current implementation before production code changes. After implementation, run the targeted tests and the complete non-integration suite.

## Compatibility and safety

The removed P2P overload is staged, unpublished code and duplicates an existing file-path API, so removing it does not break a released contract. Existing authenticated Spot-X construction remains unchanged. Tests use local handlers and temporary files only; they make no external API calls and persist no credentials.
