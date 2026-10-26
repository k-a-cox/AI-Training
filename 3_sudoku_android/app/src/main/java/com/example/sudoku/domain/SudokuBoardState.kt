package com.example.sudoku.domain

data class SudokuBoardState(
    val cells: List<SudokuCellState> = List(81) { index -> SudokuCellState(index = index) },
    val selectedCell: Int? = null,
    val phase: GamePhase = GamePhase.EnteringGivens,
    val inputModeState: InputMode = InputMode.Value
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

    fun setValue(value: Int?, inputMode: InputMode = inputModeState): SudokuBoardState {
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

    fun toggleInputMode(): SudokuBoardState {
        return copy(
            inputModeState = when (inputModeState) {
                InputMode.Value -> InputMode.Notes
                InputMode.Notes -> InputMode.Value
            }
        )
    }

//    fun toggleNote(note: Int): SudokuBoardState {
//        val index = selectedCell ?: return this
//
//        val cell = cells[index]
//        val updatedCell = cell.toggleNote(note)
//        if (updatedCell == cell) return this
//
//        return copy(
//            cells = cells.toMutableList().also { it[index] = updatedCell }
//        )
//    }
}