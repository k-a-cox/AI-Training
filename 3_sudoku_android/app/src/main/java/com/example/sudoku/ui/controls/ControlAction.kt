package com.example.sudoku.ui.controls

sealed interface ControlAction {
    data class Digit(val value: Int) : ControlAction
    data object Erase : ControlAction
    data object ToggleNotes : ControlAction
    data object AdvancePhase : ControlAction
}
