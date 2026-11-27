package com.example.sudoku.domain.validators

import com.example.sudoku.domain.state.SudokuBoardState

object SubmitBoardValidator {
    
    fun validateSolution(boardState: SudokuBoardState): BoardValidationResult {
        return validateGrid(boardState)
    }

    fun validateGrid(boardState: SudokuBoardState): BoardValidationResult {
        if (!boardState.cells.all { it.value != null }) return BoardValidationResult.Incomplete
        if (!ValidateGrid.hasNoConflicts(boardState)) {
            return BoardValidationResult.ConflictingCells(ValidateGrid.findConflicts(boardState))
        }
        return BoardValidationResult.Valid
    }

}