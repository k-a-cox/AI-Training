# Step 6: Add candidate notes

- [ ] Add an on-screen **Notes** toggle that changes the number buttons to toggle candidate digits 1–9.
- [ ] Allow notes only in empty, non-given cells.
- [ ] Show notes within their selected cell.
- [ ] Clear a cell's notes when entering a value there.
- [ ] Revisit erase behavior: currently `setValue(null)` also clears notes. Decide whether erase should clear notes only when a cell has notes and no value, and update the Notes-mode controls accordingly.
- [ ] Add unit tests for toggling notes, note eligibility, and clearing notes on value entry.

**Done when:** Candidate notes can be entered and edited using on-screen controls, without the keyboard.
