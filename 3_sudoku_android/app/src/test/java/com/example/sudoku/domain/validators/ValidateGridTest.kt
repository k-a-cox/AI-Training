package com.example.sudoku.domain.validators

import com.example.sudoku.domain.samples.PuzzleSampleBoards
import com.example.sudoku.domain.samples.SampleBoards.boxGoodBoard
import com.example.sudoku.domain.samples.SampleBoards.columnGoodBoard
import com.example.sudoku.domain.samples.SampleBoards.emptyBoard
import com.example.sudoku.domain.samples.SampleBoards.invalidBoxBoard
import com.example.sudoku.domain.samples.SampleBoards.invalidColumnBoard
import com.example.sudoku.domain.samples.SampleBoards.invalidRowBoard
import com.example.sudoku.domain.samples.SampleBoards.rowGoodBoard
import com.example.sudoku.domain.samples.SampleBoards.validFullBoard
import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.state.SudokuCellState
import org.junit.Assert.assertEquals
import org.junit.Assert.assertFalse
import org.junit.Assert.assertTrue
import org.junit.Test

class ValidateGridTest {
    @Test
    fun validateGrid_emptyBoard_isValid() {
        assertEquals(
            true,
            ValidateGrid.hasNoConflicts(emptyBoard)
        )
    }

    @Test
    fun validateGrid_reportsRowColumnAndBoxRepeats() {
        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(invalidRowBoard)
        )
        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(invalidColumnBoard)
        )
        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(invalidBoxBoard)
        )
    }

    @Test
    fun validateGrid_validFullBoard_isValid() {
        assertEquals(
            true,
            ValidateGrid.hasNoConflicts(validFullBoard)
        )
    }

    @Test
    fun validateGrid_incompleteConflictingBoard_reportsConflict() {
        val board = SudokuBoardState(
            cells = List(81) { index ->
                SudokuCellState(index = index, value = 5)
            }
        ).withValues(0 to null)

        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(board)
        )
    }

    @Test
    fun validateGrid_incompleteSolvedBoard_isValid() {
        val board = PuzzleSampleBoards.solvedBoard.withValues(0 to null)

        assertEquals(
            true,
            ValidateGrid.hasNoConflicts(board)
        )
    }

    @Test
    fun validateGrid_rowOnlyConflicts_reportsFirstInvalidRow() {
        val board = PuzzleSampleBoards.solvedBoard.withValues(27 to 4, 36 to 8)

        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(board)
        )
        for (index in 0 until 9) {
            assertTrue(ValidateGrid.validateColumn(board, index))
            assertTrue(ValidateGrid.validateBox(board, index))
        }
    }

    @Test
    fun validateGrid_columnOnlyConflicts_reportsFirstInvalidColumn() {
        val board = PuzzleSampleBoards.solvedBoard.withValues(3 to 7, 4 to 6)

        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(board)
        )
        for (index in 0 until 9) {
            assertTrue(ValidateGrid.validateRow(board, index))
            assertTrue(ValidateGrid.validateBox(board, index))
        }
    }

    @Test
    fun validateGrid_boxOnlyConflicts_reportsFirstInvalidBox() {
        val board = PuzzleSampleBoards.solvedBoard.withValues(
            12 to 8, 17 to 1, 39 to 1, 44 to 8
        )

        assertEquals(
            false,
            ValidateGrid.hasNoConflicts(board)
        )
        for (index in 0 until 9) {
            assertTrue(ValidateGrid.validateRow(board, index))
            assertTrue(ValidateGrid.validateColumn(board, index))
        }
    }

    @Test
    fun validateGrid_solvedSampleBoard_isValid() {
        assertEquals(
            true,
            ValidateGrid.hasNoConflicts(PuzzleSampleBoards.solvedBoard)
        )
    }

    private fun SudokuBoardState.withValues(vararg changes: Pair<Int, Int?>): SudokuBoardState {
        val values = changes.toMap()
        return copy(cells = cells.map { cell ->
            if (cell.index in values) cell.copy(value = values[cell.index]) else cell
        })
    }

    @Test
    fun validateRow() {
        assertFalse(ValidateGrid.validateRow(invalidRowBoard, 0))
        assertTrue(ValidateGrid.validateRow(rowGoodBoard, 1))
        assertTrue(ValidateGrid.validateRow(rowGoodBoard, 0))
    }

    @Test
    fun validateColumn() {
        assertFalse(ValidateGrid.validateColumn(invalidColumnBoard, 0))
        assertTrue(ValidateGrid.validateColumn(columnGoodBoard, 0))
        assertTrue(ValidateGrid.validateColumn(columnGoodBoard, 1))
    }

    @Test
    fun validateBox() {
        assertTrue(ValidateGrid.validateBox(boxGoodBoard, 0))
        assertTrue(ValidateGrid.validateBox(invalidColumnBoard, 0))
        assertTrue(ValidateGrid.validateBox(invalidRowBoard, 0))
        assertFalse(ValidateGrid.validateBox(invalidBoxBoard, 0))
    }
}
