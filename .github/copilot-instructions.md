# Copilot Instructions

## Project Guidelines
- When repeated test-position setup exists, prefer a genuinely generic MakeBoard helper accepting a sequence of piece/square pairs (and side to move), rather than a helper with arguments hard-coded to a specific position or duplicating setup.
- Do not restart a full second-pass manual UI review of all engine tests after each incremental test change. Wait until the user says the final engine tests are finished, then loop back across the complete test suite and perform the requested manual UI sanity-check handoffs.
- Prefer plain-language chess terminology in explanations and project checklists; use “individual move(s)” instead of the technical term “ply/plies.”
