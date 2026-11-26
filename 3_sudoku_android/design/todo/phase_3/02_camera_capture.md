# Step 2: Capture puzzle images

- [ ] Add a camera entry action and a way to capture or cancel a puzzle image.
- [ ] Request camera permission when needed and explain how to proceed if permission is denied or the camera is unavailable.
- [ ] Release camera resources appropriately when capture ends or the screen leaves the foreground.
- [ ] Preserve the current puzzle if capture is cancelled or fails, and allow the player to return to manual entry.
- [ ] Add relevant tests for permission, cancellation, and capture failure handling.

**Done when:** A player can capture an image or safely return to the existing entry flow without losing puzzle data.
