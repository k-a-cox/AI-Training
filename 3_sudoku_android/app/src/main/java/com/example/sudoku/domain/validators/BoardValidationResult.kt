package com.example.sudoku.domain.validators

sealed class BoardValidationResult {
    object Valid : BoardValidationResult()
    object Incomplete : BoardValidationResult()
    object ImpossibleSate : BoardValidationResult()
    data class ConflictingCells(val cellIndexes: Set<Int>) : BoardValidationResult()
}
