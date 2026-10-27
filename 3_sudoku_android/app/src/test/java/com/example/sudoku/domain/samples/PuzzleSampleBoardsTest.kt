package com.example.sudoku.domain.samples

import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.validators.IsValidBoard
import com.example.sudoku.domain.validators.ValidateGrid
import org.junit.Assert.assertEquals
import org.junit.Assert.assertTrue
import org.junit.Test

class PuzzleSampleBoardsTest {

    private val boards = mapOf(
        "uniqueEasy" to PuzzleSampleBoards.uniqueEasyBoard,
        "uniqueMedium" to PuzzleSampleBoards.uniqueMediumBoard,
        "uniqueSparse" to PuzzleSampleBoards.uniqueSparseBoard,
        "multipleSolutions" to PuzzleSampleBoards.multipleSolutionsBoard,
        "noSolution" to PuzzleSampleBoards.noSolutionBoard,
    )

    private fun filledCount(board: SudokuBoardState) = board.cells.count { it.value != null }

    @Test
    fun sampleBoards_fillBetween30And50Percent() {
        boards.forEach { (name, board) ->
            val percent = filledCount(board) * 100 / 81
            assertTrue("$name is $percent% filled", percent in 30..50)
        }
    }

    @Test
    fun sampleBoards_haveNoRowColumnOrBoxConflicts() {
        boards.forEach { (name, board) ->
            assertEquals(
                name,
                IsValidBoard.Valid,
                ValidateGrid.validateGrid(board, needsToBeFull = false)
            )
        }
    }

    @Test
    fun sampleBoards_areNotLockedAsGivens() {
        boards.forEach { (name, board) ->
            assertTrue(name, board.cells.none { it.isGiven })
        }
    }
}
