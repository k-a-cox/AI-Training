package com.example.sudoku.domain.validators

import com.example.sudoku.domain.state.SudokuBoardState

object GivensValidator {

    fun validateGivens(boardState: SudokuBoardState): BoardValidationResult {
        if (!startingOnlyHasValues(boardState)) return BoardValidationResult.ImpossibleSate
        return validateGrid(boardState)
    }

    fun startingOnlyHasValues(boardState: SudokuBoardState): Boolean {
        if (boardState.cells.any { it.isGiven }) return false;
        if (boardState.cells.any { it.notes.isNotEmpty() }) return false;
        return true
    }

    fun validateGrid(boardState: SudokuBoardState): BoardValidationResult {
        if (boardState.cells.all { it.value == null }) return BoardValidationResult.Incomplete
        if (!ValidateGrid.hasNoConflicts(boardState)) return BoardValidationResult.ConflictingCells(
            ValidateGrid.findConflicts(boardState)
        )
        return BoardValidationResult.Valid
    }

    fun lockInGivens(boardState: SudokuBoardState): SudokuBoardState {
        val lockedCells = boardState.cells.map { cell ->
            if (cell.value != null) cell.copy(isGiven = true) else cell
        }
        return boardState.copy(cells = lockedCells)
    }

}