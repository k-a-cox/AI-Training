# Step 1: Check conflicts on full boards

- [x] For a full board, report duplicates in each affected row, column, and 3×3 box.
- [x] Identify the conflicting numbers and cell locations, returned as cell indexes.
- [x] Highlight the conflicting cells on the board, both when Verify finds conflicts and when Confirm givens rejects conflicting givens.
- [x] Accept a full board as solved only when it has no row, column, or box conflicts.
- [x] Add unit tests for row conflicts, column conflicts, box conflicts, the reported conflicting cell indexes, and a valid completed board.

**Done when:** Full invalid boards identify and highlight their conflicts and a valid full board is accepted.
