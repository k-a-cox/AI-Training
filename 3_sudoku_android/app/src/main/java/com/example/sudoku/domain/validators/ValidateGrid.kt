package com.example.sudoku.domain.validators

import com.example.sudoku.domain.state.SudokuBoardState

object ValidateGrid {

    fun hasNoConflicts(boardState: SudokuBoardState): Boolean {
        for (index in 0 until 9) {
            if (!validateRow(boardState, index)) return false
        }
        for (index in 0 until 9) {
            if (!validateColumn(boardState, index)) return false
        }
        for (index in 0 until 9) {
            if (!validateBox(boardState, index)) return false
        }

        return true
    }

    fun findConflicts(boardState: SudokuBoardState): Set<Int> {
        val conflicts = mutableSetOf<Int>()

        for (index in 0 until 9) {
            conflicts.addAll(conflictsInRow(boardState, index))
            conflicts.addAll(conflictsInColumn(boardState, index))
            conflicts.addAll(conflictsInBox(boardState, index))
        }

        return conflicts
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

    fun conflictsInRow(boardState: SudokuBoardState, rowIndex: Int): Set<Int> {
        val seen = mutableSetOf<Int>()
        val dupesSet = mutableSetOf<Int>()
        for (col in 0 until 9) {
            val index = rowIndex * 9 + col
            val value = boardState.cells[index].value
            if (value != null) {
                if (value in seen) dupesSet.add(value)
                seen.add(value)
            }
        }

        val indexes = mutableSetOf<Int>()
        for (col in 0 until 9) {
            val index = rowIndex * 9 + col
            val value = boardState.cells[index].value
            if (value in dupesSet) {
                indexes.add(index)
            }
        }

        return indexes
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

    fun conflictsInColumn(boardState: SudokuBoardState, columnIndex: Int): Set<Int> {
        val seen = mutableSetOf<Int>()
        val dupesSet = mutableSetOf<Int>()
        for (row in 0 until 9) {
            val index = row * 9 + columnIndex
            val value = boardState.cells[index].value
            if (value != null) {
                if (value in seen) dupesSet.add(value)
                seen.add(value)
            }
        }

        val indexes = mutableSetOf<Int>()
        for (row in 0 until 9) {
            val index = row * 9 + columnIndex
            val value = boardState.cells[index].value
            if (value in dupesSet) {
                indexes.add(index)
            }
        }

        return indexes
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

    fun conflictsInBox(boardState: SudokuBoardState, boxIndex: Int): Set<Int> {
        val boxRow = boxIndex / 3
        val boxCol = boxIndex % 3

        val seen = mutableSetOf<Int>()
        val dupesSet = mutableSetOf<Int>()
        for (row in 0 until 3) {
            for (col in 0 until 3) {
                val index = (boxRow * 3 + row) * 9 + (boxCol * 3 + col)
                val value = boardState.cells[index].value
                if (value != null) {
                    if (value in seen) dupesSet.add(value)
                    seen.add(value)
                }
            }
        }

        val indexes = mutableSetOf<Int>()
        for (row in 0 until 3) {
            for (col in 0 until 3) {
                val index = (boxRow * 3 + row) * 9 + (boxCol * 3 + col)
                val value = boardState.cells[index].value
                if (value in dupesSet) {
                    indexes.add(index)
                }
            }
        }

        return indexes
    }
}