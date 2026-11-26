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
    fun validateGrid_emptyBoard_isValidWhenFullIsNotRequired() {
        assertEquals(
            BoardValidationResult.Valid,
            ValidateGrid.validateGrid(emptyBoard, needsToBeFull = false)
        )
    }

    @Test
    fun validateGrid_emptyBoard_isIncompleteWhenFullIsRequired() {
        assertEquals(
            BoardValidationResult.Incomplete,
            ValidateGrid.validateGrid(emptyBoard, needsToBeFull = true)
        )
    }

    @Test
    fun validateGrid_reportsRowColumnAndBoxRepeats() {
        assertEquals(
            BoardValidationResult.InvalidRow(0),
            ValidateGrid.validateGrid(invalidRowBoard, needsToBeFull = false)
        )
        assertEquals(
            BoardValidationResult.InvalidColumn(0),
            ValidateGrid.validateGrid(invalidColumnBoard, needsToBeFull = false)
        )
        assertEquals(
            BoardValidationResult.InvalidBox(0),
            ValidateGrid.validateGrid(invalidBoxBoard, needsToBeFull = false)
        )
    }

    @Test
    fun validateGrid_validFullBoard_isValid() {
        assertEquals(
            BoardValidationResult.Valid,
            ValidateGrid.validateGrid(validFullBoard, needsToBeFull = true)
        )
    }

    @Test
    fun validateGrid_incompleteConflictingBoard_reportsIncomplete() {
        val board = SudokuBoardState(
            cells = List(81) { index ->
                SudokuCellState(index = index, value = 5)
            }
        ).withValues(0 to null)

        assertEquals(
            BoardValidationResult.Incomplete,
            ValidateGrid.validateGrid(board, needsToBeFull = true)
        )
    }

    @Test
    fun validateGrid_incompleteSolvedBoard_reportsIncomplete() {
        val board = PuzzleSampleBoards.solvedBoard.withValues(0 to null)

        assertEquals(
            BoardValidationResult.Incomplete,
            ValidateGrid.validateGrid(board, needsToBeFull = true)
        )
    }

    @Test
    fun validateGrid_rowOnlyConflicts_reportsFirstInvalidRow() {
        val board = PuzzleSampleBoards.solvedBoard.withValues(27 to 4, 36 to 8)

        assertEquals(
            BoardValidationResult.InvalidRow(3),
            ValidateGrid.validateGrid(board, needsToBeFull = true)
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
            BoardValidationResult.InvalidColumn(3),
            ValidateGrid.validateGrid(board, needsToBeFull = true)
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
            BoardValidationResult.InvalidBox(1),
            ValidateGrid.validateGrid(board, needsToBeFull = true)
        )
        for (index in 0 until 9) {
            assertTrue(ValidateGrid.validateRow(board, index))
            assertTrue(ValidateGrid.validateColumn(board, index))
        }
    }

    @Test
    fun validateGrid_solvedSampleBoard_isValid() {
        assertEquals(
            BoardValidationResult.Valid,
            ValidateGrid.validateGrid(PuzzleSampleBoards.solvedBoard, needsToBeFull = true)
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

    @Test
    fun isBoardFull() {
        assertFalse(ValidateGrid.isBoardFull(emptyBoard))
        assertTrue(ValidateGrid.isBoardFull(validFullBoard))
    }
}
