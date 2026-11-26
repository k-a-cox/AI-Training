# Step 5: Integrate solver progress and controls

- [ ] Add a user action to start the automatic solver and display its ordered progress and explanations.
- [ ] Show solved, invalid, and stuck outcomes distinctly without presenting a stuck puzzle as unsolvable.
- [ ] Allow an active solver run to be cancelled.
- [ ] Invalidate or cancel stale solver progress when the player manually edits the puzzle.
- [ ] Keep solver presentation in the UI layer and solving rules in the solver domain.
- [ ] Add relevant UI or state tests for starting, progress, outcomes, cancellation, and manual edits during a run.

**Done when:** Players can follow and cancel solver progress, and manual edits cannot be overwritten by stale results.
