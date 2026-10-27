package com.example.sudoku.app

import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.ui.controls.ControlAction

fun GameState.reduce(action: ControlAction): GameState = when (action) {
    is ControlAction.Digit -> copy(board = board.setValue(action.value))
    ControlAction.Erase -> copy(board = board.setValue(null))
    ControlAction.ToggleNotes -> copy(notesOn = !notesOn)
    is ControlAction.PickBoard -> copy(board = action.value)
    ControlAction.Restart -> copy(board = SudokuBoardState(), notesOn = false)
    ControlAction.LockGivens -> copy(showLockGivensDialog = true)
    ControlAction.SubmitBoard -> copy(showLockGivensDialog = true) // todo: replace with Verify
}
