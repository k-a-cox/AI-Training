# Step 4: Review and confirm scanned clues

- [ ] Present recognized clues in the existing givens-entry board for review before confirmation.
- [ ] Allow the player to correct, add, or erase clues with the existing on-screen controls; make uncertain cells visible for review.
- [ ] Require the explicit **Confirm givens** action; do not lock or start play automatically after scanning.
- [ ] Reuse the normal conflict and uniqueness validation: reject conflicting, unsolvable, and multiple-solution givens; lock only an accepted unique puzzle.
- [ ] Add tests for correcting scanned clues, preserving editability after rejection, and confirming a valid scanned puzzle.

**Done when:** Scanned clues remain editable until the player explicitly confirms them through the normal validation flow.
