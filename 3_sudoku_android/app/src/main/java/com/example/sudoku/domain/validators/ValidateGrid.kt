package com.example.sudoku.domain.validators

import com.example.sudoku.domain.state.SudokuBoardState

enum class IsValidBoard { Valid, Incomplete, InvalidRow, InvalidColumn, InvalidBox }

object ValidateGrid {

    fun validateGrid(boardState: SudokuBoardState, needsToBeFull: Boolean): IsValidBoard {
        if (needsToBeFull && !isBoardFull(boardState)) {
            return IsValidBoard.Incomplete
        }
        for (index in 0 until 9) {
            if (!validateRow(boardState, index)) return IsValidBoard.InvalidRow
        }
        for (index in 0 until 9) {
            if (!validateColumn(boardState, index)) return IsValidBoard.InvalidColumn
        }
        for (index in 0 until 9) {
            if (!validateBox(boardState, index)) return IsValidBoard.InvalidBox
        }

        return IsValidBoard.Valid
    }

    fun validateRow(boardState: SudokuBoardState, rowIndex: Int): Boolean {
        val seen = mutableSetOf<Int>()
        for (col in 0 until 9) {
            val index = rowIndex * 9 + col
            val value = boardState.cells[index].value
            if (value != null) {
                if (value in seen) return false
                seen.add(value)
            }
        }
        return true
    }

    fun validateColumn(
        boardState: SudokuBoardState,
        columnIndex: Int,
    ): Boolean {
        val seen = mutableSetOf<Int>()
        for (row in 0 until 9) {
            val index = row * 9 + columnIndex
            val value = boardState.cells[index].value
            if (value != null) {
                if (value in seen) return false
                seen.add(value)
            }
        }
        return true
    }

    fun validateBox(boardState: SudokuBoardState, boxIndex: Int): Boolean {
        val boxRow = boxIndex / 3
        val boxCol = boxIndex % 3

        val seen = mutableSetOf<Int>()
        for (row in 0 until 3) {
            for (col in 0 until 3) {
                val index = (boxRow * 3 + row) * 9 + (boxCol * 3 + col)
                val value = boardState.cells[index].value
                if (value != null) {
                    if (value in seen) return false
                    seen.add(value)
                }
            }
        }
        return true
    }

    fun isBoardFull(boardState: SudokuBoardState): Boolean {
        return boardState.cells.all { it.value != null }
    }
}