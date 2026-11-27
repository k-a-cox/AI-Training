package com.example.sudoku.app

import com.example.sudoku.domain.InputMode
import com.example.sudoku.ui.controls.ControlAction

fun GameState.reduce(action: ControlAction): GameState = when (action) {
    is ControlAction.Digit -> copy(
        board = board.setValue(
            action.value,
            inputMode = if (notesOn) InputMode.Notes else InputMode.Value
        )
    )

    ControlAction.Erase -> copy(board = board.clearValue())
    ControlAction.ToggleNotes -> copy(notesOn = !notesOn)
    is ControlAction.PickBoard -> newGame().copy(board = action.value)
    ControlAction.Restart -> newGame()
    ControlAction.LockGivens -> copy(showLockGivensDialog = true)
    ControlAction.SubmitBoard -> copy(showSubmitBoardDialog = true)
}
