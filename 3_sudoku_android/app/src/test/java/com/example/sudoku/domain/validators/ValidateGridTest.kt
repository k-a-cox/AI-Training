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

internal fun boardWithValues(vararg values: Pair<Int, Int>): SudokuBoardState {
    val valuesByIndex = values.toMap()
    return SudokuBoardState(
        cells = List(81) { index ->
            SudokuCellState(index = index, value = valuesByIndex[index])
        }
    )
}

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
    fun validateGrid_rowOnlyConflicts_areDetected() {
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
    fun validateGrid_columnOnlyConflicts_areDetected() {
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
    fun validateGrid_boxOnlyConflicts_areDetected() {
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

    @Test
    fun emptyAndValidBoards_haveNoConflictingIndexes() {
        assertEquals(emptySet<Int>(), ValidateGrid.findConflicts(emptyBoard))
        assertEquals(emptySet<Int>(), ValidateGrid.findConflicts(PuzzleSampleBoards.solvedBoard))
    }

    @Test
    fun conflictsInRow_returnsEveryOccurrenceOfEveryDuplicate() {
        val board = boardWithValues(0 to 5, 1 to 5, 2 to 5, 4 to 8, 7 to 8, 9 to 5)

        assertEquals(setOf(0, 1, 2, 4, 7), ValidateGrid.conflictsInRow(board, 0))
        assertEquals(emptySet<Int>(), ValidateGrid.conflictsInRow(board, 1))
        assertFalse(ValidateGrid.validateRow(board, 0))
        assertTrue(ValidateGrid.validateRow(board, 1))
    }

    @Test
    fun conflictsInColumn_returnsEveryOccurrenceOfEveryDuplicate() {
        val board = boardWithValues(0 to 5, 27 to 5, 72 to 5, 9 to 8, 36 to 8, 1 to 5)

        assertEquals(setOf(0, 9, 27, 36, 72), ValidateGrid.conflictsInColumn(board, 0))
        assertEquals(emptySet<Int>(), ValidateGrid.conflictsInColumn(board, 1))
        assertFalse(ValidateGrid.validateColumn(board, 0))
        assertTrue(ValidateGrid.validateColumn(board, 1))
    }

    @Test
    fun conflictsInBox_returnsEveryOccurrenceOfEveryDuplicate() {
        val board = boardWithValues(60 to 5, 70 to 5, 80 to 5, 62 to 8, 79 to 8, 59 to 5)

        assertEquals(setOf(60, 62, 70, 79, 80), ValidateGrid.conflictsInBox(board, 8))
        assertEquals(emptySet<Int>(), ValidateGrid.conflictsInBox(board, 7))
        assertFalse(ValidateGrid.validateBox(board, 8))
        assertTrue(ValidateGrid.validateBox(board, 7))
    }

    @Test
    fun unitConflictChecks_coverEveryRowColumnAndBox() {
        for (unit in 0 until 9) {
            val rowStart = unit * 9
            val rowBoard = boardWithValues(rowStart to 4, rowStart + 8 to 4)
            assertEquals(setOf(rowStart, rowStart + 8), ValidateGrid.conflictsInRow(rowBoard, unit))
            assertFalse(ValidateGrid.validateRow(rowBoard, unit))

            val columnBoard = boardWithValues(unit to 4, unit + 72 to 4)
            assertEquals(setOf(unit, unit + 72), ValidateGrid.conflictsInColumn(columnBoard, unit))
            assertFalse(ValidateGrid.validateColumn(columnBoard, unit))

            val boxStart = (unit / 3) * 27 + (unit % 3) * 3
            val boxBoard = boardWithValues(boxStart to 4, boxStart + 20 to 4)
            assertEquals(setOf(boxStart, boxStart + 20), ValidateGrid.conflictsInBox(boxBoard, unit))
            assertFalse(ValidateGrid.validateBox(boxBoard, unit))
        }
    }

    @Test
    fun findConflicts_combinesAllUnitsWithoutRepeatedIndexes() {
        val board = boardWithValues(0 to 5, 1 to 5, 9 to 5, 40 to 8, 44 to 8)

        assertFalse(ValidateGrid.hasNoConflicts(board))
        assertEquals(setOf(0, 1, 9, 40, 44), ValidateGrid.findConflicts(board))
    }

    @Test
    fun unitConflictChecks_ignoreEmptyCellsAndDistinctValues() {
        val board = boardWithValues(0 to 1, 1 to 2, 9 to 3)

        assertEquals(emptySet<Int>(), ValidateGrid.conflictsInRow(board, 0))
        assertEquals(emptySet<Int>(), ValidateGrid.conflictsInColumn(board, 0))
        assertEquals(emptySet<Int>(), ValidateGrid.conflictsInBox(board, 0))
        assertTrue(ValidateGrid.hasNoConflicts(board))
    }
}
