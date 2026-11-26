package com.example.sudoku.domain.validators

import com.example.sudoku.domain.state.SudokuBoardState

class SubmitBoardValidator {

    // Starting from solvedBoard, clear these cells before locking givens,
    // then enter the replacement values and submit. Reset between cases.
    // Coordinates are 1-based.
    //
    //Row: swap R4C1 and R5C1 → first invalid row is index 3.
    //Column: swap R1C4 and R1C5 → first invalid column is index 3.
    //Box: swap R2C4 with R2C9, and R5C4 with R5C9 → first invalid box is index 1 (top middle).
    fun validateSolution(boardState: SudokuBoardState): BoardValidationResult {
        return validateGrid(boardState)
    }

    fun validateGrid(boardState: SudokuBoardState): BoardValidationResult {
        if (boardState.cells.all { it.value != null }) return BoardValidationResult.Incomplete
        return ValidateGrid.hasAnyConflicts(boardState)
    }

}