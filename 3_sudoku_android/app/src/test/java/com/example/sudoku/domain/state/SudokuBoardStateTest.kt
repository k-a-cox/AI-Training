package com.example.sudoku.domain.state

import com.example.sudoku.domain.InputMode
import org.junit.Assert.assertEquals
import org.junit.Assert.assertNull
import org.junit.Assert.assertTrue
import org.junit.Test

class SudokuBoardStateTest {

    private val defaultBoard = SudokuBoardState()
    private val defaultSelectedCell = 5
    private val boardWithSelectedCell = SudokuBoardState(selectedCell = defaultSelectedCell)

    private fun assertDefaultBoardState() {
        assertEquals(81, defaultBoard.cells.size)
        assertNull(defaultBoard.selectedCell)
        assertTrue(defaultBoard.cells.all { cell ->
            cell.value == null && !cell.isGiven && cell.notes.isEmpty()
        })
    }

    private fun assertBoardWithSelectedCellState() {
        assertEquals(81, boardWithSelectedCell.cells.size)
        assertEquals(defaultSelectedCell, boardWithSelectedCell.selectedCell)
        assertTrue(boardWithSelectedCell.cells.all { cell ->
            cell.value == null && !cell.isGiven && cell.notes.isEmpty()
        })
    }

    @Test
    fun defaultBoardState_has81CellsAndNoSelection() {
        assertDefaultBoardState()
    }

    @Test
    fun defaultBoardState_hasCellsWithMatchingIndexes() {
        assertEquals((0 until 81).toList(), defaultBoard.cells.map { it.index })
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsBoardWithFewerThan81Cells() {
        SudokuBoardState(cells = List(80) { index -> SudokuCellState(index = index) })
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsBoardWithMoreThan81Cells() {
        val cells =
            List(81) { index -> SudokuCellState(index = index) } + SudokuCellState(index = 0)

        SudokuBoardState(cells = cells)
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsSelectedIndexBelowZero() {
        SudokuBoardState(selectedCell = -1)
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsSelectedIndexPastLastCell() {
        SudokuBoardState(selectedCell = 81)
    }

    @Test
    fun boardWithSelectedCell_hasSelectedCell() {
        assertBoardWithSelectedCellState()
    }

    @Test
    fun selectCell_onDefaultBoard_selectsCell3() {
        val updatedBoard = defaultBoard.selectCell(3)

        assertEquals(3, updatedBoard.selectedCell)
        assertDefaultBoardState()
    }

    @Test
    fun selectCell_onBoardWithSelectedCell_selectsCell3() {
        val updatedBoard = boardWithSelectedCell.selectCell(3)

        assertEquals(3, updatedBoard.selectedCell)
        assertBoardWithSelectedCellState()
    }

    @Test
    fun selectCell_onBoardWithSelectedCell_deselectsCellWhenSelectedAgain() {
        val updatedBoard = boardWithSelectedCell.selectCell(defaultSelectedCell)

        assertNull(updatedBoard.selectedCell)
        assertBoardWithSelectedCellState()
    }

    @Test(expected = IllegalArgumentException::class)
    fun selectCell_rejectsNegativeIndex() {
        defaultBoard.selectCell(-1)
    }

    @Test(expected = IllegalArgumentException::class)
    fun selectCell_rejectsIndex81() {
        defaultBoard.selectCell(81)
    }

    ///Set Value
    @Test
    fun setValue_unselectedStaysUnchanged() {
        val originalBoard = defaultBoard
        val updatedBoard = originalBoard.setValue(3)

        assertEquals(originalBoard, updatedBoard)
    }

    @Test
    fun setValue_SelectedTo3() {
        val originalBoard = boardWithSelectedCell
        val updatedBoard = originalBoard.setValue(3)

        assertEquals(3, updatedBoard.cells[defaultSelectedCell].value)
        assertBoardWithSelectedCellState()

        for (index in originalBoard.cells.indices.filter { it != defaultSelectedCell }) {
            assertEquals(originalBoard.cells[index], updatedBoard.cells[index])
        }
    }

    @Test
    fun setValue_SelectedToNull() {
        val originalBoard = boardWithSelectedCell.setValue(3)
        val updatedBoard = originalBoard.setValue(null)

        assertEquals(3, originalBoard.cells[defaultSelectedCell].value)
        assertNull(updatedBoard.cells[defaultSelectedCell].value)

        for (index in originalBoard.cells.indices.filter { it != defaultSelectedCell }) {
            assertEquals(originalBoard.cells[index], updatedBoard.cells[index])
        }
    }

    @Test(expected = IllegalArgumentException::class)
    fun setValue_InvalidValue() {
        boardWithSelectedCell.setValue(80)
    }


    ///Toggle Note
    @Test
    fun toggleNote_unselectedStaysUnchanged() {
        val originalBoard = defaultBoard
        val updatedBoard = originalBoard.setValue(3, InputMode.Notes)

        assertEquals(originalBoard, updatedBoard)
    }

    @Test
    fun toggleNote_SelectedChanges() {
        val originalBoard = boardWithSelectedCell
        val updatedBoard = originalBoard.setValue(3, InputMode.Notes)

        assertEquals(setOf(3), updatedBoard.cells[defaultSelectedCell].notes)
        assertBoardWithSelectedCellState()

        for (index in originalBoard.cells.indices.filter { it != defaultSelectedCell }) {
            assertEquals(originalBoard.cells[index], updatedBoard.cells[index])
        }
    }

    @Test
    fun toggleNote_SelectedTwiceChangesNull() {
        val originalBoard = boardWithSelectedCell.setValue(3, InputMode.Notes)
        val updatedBoard = originalBoard.setValue(3, InputMode.Notes)

        assertTrue(originalBoard.cells[defaultSelectedCell].notes.isNotEmpty())
        assertTrue(updatedBoard.cells[defaultSelectedCell].notes.isEmpty())

        for (index in originalBoard.cells.indices.filter { it != defaultSelectedCell }) {
            assertEquals(originalBoard.cells[index], updatedBoard.cells[index])
        }

    }

    @Test(expected = IllegalArgumentException::class)
    fun toggleNote_InvalidValue() {
        boardWithSelectedCell.setValue(300, InputMode.Notes)
    }

}
