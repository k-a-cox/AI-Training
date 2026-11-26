package com.example.sudoku.ui.controls

import com.example.sudoku.domain.state.SudokuBoardState

sealed interface ControlAction {
    data class Digit(val value: Int) : ControlAction
    data class PickBoard(val value: SudokuBoardState) : ControlAction
    data object Erase : ControlAction
    data object ToggleNotes : ControlAction
    data object SubmitBoard : ControlAction
    data object Restart : ControlAction
    data object LockGivens : ControlAction
}
