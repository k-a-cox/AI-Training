# Step 1: Define the scan pipeline

- [ ] Choose an Android-compatible approach for capturing an image and recognizing a Sudoku grid and its digits.
- [ ] Define a testable recognition result that maps clues to the 81 board cells and distinguishes empty or uncertain cells from recognized digits.
- [ ] Keep image recognition separate from board validation and the Compose UI.
- [ ] Define failure feedback for an unreadable or undetected puzzle so the player can retry or return to manual entry.
- [ ] Add focused tests for recognition-result mapping and failure handling.

**Done when:** The camera-to-clue pipeline has a testable contract and does not treat uncertain recognition as confirmed givens.
