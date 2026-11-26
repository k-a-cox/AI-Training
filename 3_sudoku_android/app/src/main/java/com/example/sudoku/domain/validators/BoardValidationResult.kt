package com.example.sudoku.domain.validators

sealed class BoardValidationResult {
    object Valid : BoardValidationResult()
    object Incomplete : BoardValidationResult()
    data class InvalidRow(val rowIndex: Int) : BoardValidationResult()
    data class InvalidColumn(val columnIndex: Int) : BoardValidationResult()
    data class InvalidBox(val boxIndex: Int) : BoardValidationResult()
}
