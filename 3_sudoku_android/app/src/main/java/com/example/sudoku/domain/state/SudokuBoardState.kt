package com.example.sudoku.domain.state

import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.InputMode

data class SudokuBoardState(
    val cells: List<SudokuCellState> = List(81) { index -> SudokuCellState(index = index) },
    val selectedCell: Int? = null,
    val phase: GamePhase = GamePhase.EnteringGivens
) {
    init {
        require(cells.size == 81) {
            "Board must have 81 cells."
        }
        require(selectedCell == null || selectedCell in cells.indices) {
            "Selected cell must be within the range of cells."
        }
    }

    fun selectCell(index: Int): SudokuBoardState {
        require(index in cells.indices)

        return copy(
            selectedCell = if (selectedCell == index) null else index
        )
    }

    fun setValue(value: Int?, inputMode: InputMode = InputMode.Value): SudokuBoardState {
        val index = selectedCell ?: return this

        val cell = cells[index]
        val updatedCell = cell.setValue(value, inputMode)

        return copy(
            cells = cells.toMutableList().also { it[index] = updatedCell })
    }

    fun togglePhase(): SudokuBoardState {
        return copy(
            phase = when (phase) {
                GamePhase.EnteringGivens -> GamePhase.Playing
                GamePhase.Playing -> GamePhase.Solved
                GamePhase.Solved -> GamePhase.EnteringGivens
            }
        )
    }
}