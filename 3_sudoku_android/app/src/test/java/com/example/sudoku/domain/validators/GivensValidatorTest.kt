package com.example.sudoku.domain.validators

import com.example.sudoku.domain.samples.PuzzleSampleBoards
import org.junit.Assert.assertEquals
import org.junit.Assert.assertFalse
import org.junit.Assert.assertTrue
import org.junit.Test

class GivensValidatorTest {

    @Test
    fun emptyBoard_isIncomplete() {
        val board = boardWithValues()

        assertTrue(GivensValidator.startingOnlyHasValues(board))
        assertEquals(BoardValidationResult.Incomplete, GivensValidator.validateGrid(board))
        assertEquals(BoardValidationResult.Incomplete, GivensValidator.validateGivens(board))
    }

    @Test
    fun conflictFreeGivens_areValid() {
        val board = PuzzleSampleBoards.uniqueEasyBoard

        assertTrue(GivensValidator.startingOnlyHasValues(board))
        assertEquals(BoardValidationResult.Valid, GivensValidator.validateGrid(board))
        assertEquals(BoardValidationResult.Valid, GivensValidator.validateGivens(board))
    }

    @Test
    fun conflictingGivens_reportAllConflictingCells() {
        val board = boardWithValues(0 to 5, 1 to 5, 9 to 5, 40 to 8, 44 to 8)
        val expected = BoardValidationResult.ConflictingCells(setOf(0, 1, 9, 40, 44))

        assertEquals(expected, GivensValidator.validateGrid(board))
        assertEquals(expected, GivensValidator.validateGivens(board))
    }

    @Test
    fun alreadyLockedGivens_areAnImpossibleState() {
        val board = GivensValidator.lockInGivens(boardWithValues(0 to 5, 1 to 5))

        assertFalse(GivensValidator.startingOnlyHasValues(board))
        assertEquals(BoardValidationResult.ImpossibleSate, GivensValidator.validateGivens(board))
    }

    @Test
    fun notesInGivensEntry_areAnImpossibleState() {
        val board = boardWithValues(0 to 5).let { original ->
            original.copy(cells = original.cells.map {
                if (it.index == 1) it.copy(notes = setOf(2, 3)) else it
            })
        }

        assertFalse(GivensValidator.startingOnlyHasValues(board))
        assertEquals(BoardValidationResult.ImpossibleSate, GivensValidator.validateGivens(board))
    }

    @Test
    fun notesOnAnOtherwiseEmptyBoard_areAnImpossibleState() {
        val board = boardWithValues().let { original ->
            original.copy(cells = original.cells.map {
                if (it.index == 0) it.copy(notes = setOf(1)) else it
            })
        }

        assertFalse(GivensValidator.startingOnlyHasValues(board))
        assertEquals(BoardValidationResult.ImpossibleSate, GivensValidator.validateGivens(board))
    }

    @Test
    fun lockInGivens_locksOnlyFilledCellsAndPreservesBoardState() {
        val board = boardWithValues(0 to 5, 80 to 9).copy(selectedCell = 0)
        val locked = GivensValidator.lockInGivens(board)

        assertEquals(0, locked.selectedCell)
        assertEquals(board.cells.map { it.value }, locked.cells.map { it.value })
        assertEquals(setOf(0, 80), locked.cells.filter { it.isGiven }.map { it.index }.toSet())
        assertTrue(locked.cells.all { it.notes.isEmpty() })
        assertTrue(board.cells.none { it.isGiven })
    }
}
