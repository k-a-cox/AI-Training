# Step 3: Recognize the grid and clues

- [ ] Detect a Sudoku grid in a captured image and map its cells to the correct board positions.
- [ ] Recognize clue digits 1–9 and distinguish them from empty or uncertain cells.
- [ ] Report unreadable images or uncertain cells for retry or correction rather than silently accepting guesses as givens.
- [ ] Add tests using representative puzzle images for cell mapping, digit recognition, empty cells, and failed or uncertain recognition.

**Done when:** A captured puzzle can produce a reviewable 9×9 set of clues, with recognition problems visible to the player.
