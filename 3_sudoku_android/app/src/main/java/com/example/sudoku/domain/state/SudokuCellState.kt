package com.example.sudoku.domain.state

import com.example.sudoku.domain.InputMode

data class SudokuCellState(
    val index: Int,
    val value: Int? = null,
    val isGiven: Boolean = false,
    val notes: Set<Int> = emptySet(),
) {
    init {
        require(index in 0..80) {
            "Index must be between 0 and 80."
        }
        require(!isGiven || value != null) {
            "A given cell must have a value."
        }
        require(value == null || value in 1..9) {
            "Value must be between 1 and 9."
        }
        require(notes.all { it in 1..9 }) {
            "Notes must be between 1 and 9."
        }
        require(value == null || notes.isEmpty()) {
            "A cell cannot have both a value and notes."
        }
    }

    fun setValue(value: Int?, inputMode: InputMode = InputMode.Value): SudokuCellState {
        require(value == null || value in 1..9)

        if (isGiven) return this

        return when (inputMode) {
            InputMode.Value -> {
                copy(value = value, notes = emptySet())
            }

            InputMode.Notes -> {
                toggleNote(value)
            }
        }
    }

    private fun toggleNote(note: Int?): SudokuCellState {

        if (value != null) return this

        if (note == null) return copy(notes = emptySet())

        val updatedNotes = if (note in notes) {
            notes - note
        } else {
            notes + note
        }

        return copy(notes = updatedNotes)
    }
}