package com.example.sudoku.domain.validators

import com.example.sudoku.domain.samples.PuzzleSampleBoards
import org.junit.Assert.assertEquals
import org.junit.Test

class SubmitBoardValidatorTest {
    @Test
    fun emptyBoard_isIncomplete() {
        assertEquals(BoardValidationResult.Incomplete, SubmitBoardValidator.validateSolution(boardWithValues()))
    }

    @Test
    fun incompleteConflictFreeBoard_isIncomplete() {
        assertEquals(
            BoardValidationResult.Incomplete,
            SubmitBoardValidator.validateSolution(PuzzleSampleBoards.uniqueEasyBoard)
        )
    }

    @Test
    fun incompleteBoardWithConflicts_reportsIncompleteFirst() {
        val board = boardWithValues(0 to 5, 1 to 5)

        assertEquals(BoardValidationResult.Incomplete, SubmitBoardValidator.validateSolution(board))
        assertEquals(BoardValidationResult.Incomplete, SubmitBoardValidator.validateGrid(board))
    }

    @Test
    fun solvedBoard_isValid() {
        assertEquals(BoardValidationResult.Valid, SubmitBoardValidator.validateSolution(PuzzleSampleBoards.solvedBoard))
        assertEquals(BoardValidationResult.Valid, SubmitBoardValidator.validateGrid(PuzzleSampleBoards.solvedBoard))
    }

    @Test
    fun fullBoardWithConflicts_reportsAllConflictingCells() {
        val solved = PuzzleSampleBoards.solvedBoard
        val board = solved.copy(cells = solved.cells.map {
            if (it.index == 0) it.copy(value = 3) else it
        })
        val expected = BoardValidationResult.ConflictingCells(setOf(0, 1, 72))

        assertEquals(expected, SubmitBoardValidator.validateSolution(board))
        assertEquals(expected, SubmitBoardValidator.validateGrid(board))
    }
}
