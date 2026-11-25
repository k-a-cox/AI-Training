package com.example.sudoku.app

import com.example.sudoku.domain.samples.PuzzleSampleBoards
import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.ui.controls.ControlAction
import org.junit.Assert.assertEquals
import org.junit.Test

class GameReducerTest {

    @Test
    fun digit_setsValueInSelectedCell() {
        val selectedIndex = 5
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex))

        val updated = state.reduce(ControlAction.Digit(7))

        assertEquals(7, updated.board.cells[selectedIndex].value)
    }

    @Test
    fun digit_withNotesOn_togglesNoteInsteadOfSettingValue() {
        val selectedIndex = 5
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex))
            .reduce(ControlAction.ToggleNotes)

        val withNote = state.reduce(ControlAction.Digit(7))
        val withoutNote = withNote.reduce(ControlAction.Digit(7))

        assertEquals(null, withNote.board.cells[selectedIndex].value)
        assertEquals(setOf(7), withNote.board.cells[selectedIndex].notes)
        assertEquals(emptySet<Int>(), withoutNote.board.cells[selectedIndex].notes)
    }

    @Test
    fun digit_afterTurningNotesOff_setsValueAndClearsNotes() {
        val selectedIndex = 5
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex))
            .reduce(ControlAction.ToggleNotes)
            .reduce(ControlAction.Digit(7))
            .reduce(ControlAction.ToggleNotes)

        val updated = state.reduce(ControlAction.Digit(3))

        assertEquals(3, updated.board.cells[selectedIndex].value)
        assertEquals(emptySet<Int>(), updated.board.cells[selectedIndex].notes)
    }

    @Test
    fun digit_withNoSelectedCell_leavesBoardUnchanged() {
        val selectedIndex = null
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex))

        val updated = state.reduce(ControlAction.Digit(7))

        assertEquals(state, updated)
    }

    @Test
    fun erase_clearsSelectedCellValue() {
        val selectedIndex = 5
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex).setValue(7))

        val updated = state.reduce(ControlAction.Erase)

        assertEquals(null, updated.board.cells[selectedIndex].value)
    }

    @Test
    fun erase_withNotesOn_clearsSelectedCellNotes() {
        val selectedIndex = 5
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex))
            .reduce(ControlAction.ToggleNotes)
            .reduce(ControlAction.Digit(7))

        val updated = state.reduce(ControlAction.Erase)

        assertEquals(emptySet<Int>(), updated.board.cells[selectedIndex].notes)
    }

    @Test
    fun toggleNotes_flipsNotesOn() {
        val state = GameState()
        val updated = state.reduce(ControlAction.ToggleNotes)

        assertEquals(true, updated.notesOn)
    }

    @Test
    fun toggleNotes_twice_returnsToOriginalState() {
        val state = GameState()
        val updated = state.reduce(ControlAction.ToggleNotes).reduce(ControlAction.ToggleNotes)

        assertEquals(false, updated.notesOn)
    }

    @Test
    fun pickBoard_replacesBoard() {
        val selectedIndex = 5
        val state = GameState(board = SudokuBoardState(selectedCell = selectedIndex).setValue(7))
        val updated = state.reduce(ControlAction.PickBoard(PuzzleSampleBoards.uniqueEasyBoard))

        assertEquals(PuzzleSampleBoards.uniqueEasyBoard, updated.board)
    }

    @Test
    fun restart_returnsEmptyBoardInEnteringGivensPhase() {
        val state = GameState().reduce(ControlAction.PickBoard(PuzzleSampleBoards.uniqueEasyBoard))
        val updated = state.reduce(ControlAction.Restart)

        assertEquals(SudokuBoardState(), updated.board)
    }

    @Test
    fun restart_turnsNotesOff() {
        val state = GameState(notesOn = true)
        val updated = state.reduce(ControlAction.Restart)

        assertEquals(false, updated.notesOn)
    }

    @Test
    fun lockGivens_showsLockGivensDialog() {
        val state = GameState().reduce(ControlAction.PickBoard(PuzzleSampleBoards.uniqueEasyBoard))
        val updated = state.reduce(ControlAction.LockGivens)

        assertEquals(true, updated.showLockGivensDialog)
    }

    @Test
    fun lockGivens_doesNotChangeBoard() {
        val state = GameState().reduce(ControlAction.PickBoard(PuzzleSampleBoards.uniqueEasyBoard))
        val updated = state.reduce(ControlAction.LockGivens)

        assertEquals(state.board, updated.board)
    }
}
