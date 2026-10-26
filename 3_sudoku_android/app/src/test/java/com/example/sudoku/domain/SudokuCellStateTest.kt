package com.example.sudoku.domain

import org.junit.Assert.assertEquals
import org.junit.Assert.assertFalse
import org.junit.Assert.assertNull
import org.junit.Assert.assertTrue
import org.junit.Test

class SudokuCellStateTest {
    private val defaultCell = SudokuCellState(index = 0)
    private val cellWithValue3 = SudokuCellState(index = 0, value = 3)
    private val cellWithNotes258 = SudokuCellState(index = 0, notes = setOf(2, 5, 8))
    private val givenCellWithValue3 = SudokuCellState(index = 0, value = 3, isGiven = true)

    private fun assertDefaultCellState() {
        assertEquals(0, defaultCell.index)
        assertNull(defaultCell.value)
        assertFalse(defaultCell.isGiven)
        assertTrue(defaultCell.notes.isEmpty())
    }

    private fun assertCellWithValue3State() {
        assertEquals(0, cellWithValue3.index)
        assertEquals(3, cellWithValue3.value)
        assertFalse(cellWithValue3.isGiven)
        assertTrue(cellWithValue3.notes.isEmpty())
    }

    private fun assertCellWithNotes258State() {
        assertEquals(0, cellWithNotes258.index)
        assertNull(cellWithNotes258.value)
        assertFalse(cellWithNotes258.isGiven)
        assertEquals(setOf(2, 5, 8), cellWithNotes258.notes)
    }

    private fun assertGivenCellWithValue3State() {
        assertEquals(0, givenCellWithValue3.index)
        assertEquals(3, givenCellWithValue3.value)
        assertTrue(givenCellWithValue3.isGiven)
        assertTrue(givenCellWithValue3.notes.isEmpty())
    }

    @Test
    fun defaultCellState_hasExpectedDefaults() {
        assertDefaultCellState()
    }

    @Test(expected = IllegalArgumentException::class)
    fun givenCell_hasValue() {
        SudokuCellState(index = 0, isGiven = true)
    }

    @Test
    fun givenCellWithValue3State_hasExpectedProperties() {
        assertGivenCellWithValue3State()
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsNegativeIndex() {
        SudokuCellState(index = -1)
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsIndexPastLastCell() {
        SudokuCellState(index = 81)
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsValue0() {
        SudokuCellState(index = 0, value = 0)
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsValue10() {
        SudokuCellState(index = 0, value = 10)
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsNote0() {
        SudokuCellState(index = 0, notes = setOf(0))
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsNote10() {
        SudokuCellState(index = 0, notes = setOf(10))
    }

    @Test(expected = IllegalArgumentException::class)
    fun constructor_rejectsCellWithBothValueAndNotes() {
        SudokuCellState(index = 0, value = 3, notes = setOf(2))
    }

    @Test
    fun setValue_clearsValueWhenNull() {
        val updatedCell = cellWithValue3.setValue(null, InputMode.Value)

        assertNull(updatedCell.value)
        assertCellWithValue3State()
    }

    @Test
    fun setValue_addsValueToDefaultCell() {
        val updatedCell = defaultCell.setValue(4)

        assertEquals(4, updatedCell.value)
        assertTrue(updatedCell.notes.isEmpty())
        assertDefaultCellState()
    }

    @Test
    fun setValue_addsValueToCellWithNotes_clearsNotes() {
        val updatedCell = cellWithNotes258.setValue(4)

        assertEquals(4, updatedCell.value)
        assertTrue(updatedCell.notes.isEmpty())
        assertCellWithNotes258State()
    }

    @Test(expected = IllegalArgumentException::class)
    fun setValue_rejectsValue0() {
        defaultCell.setValue(0)
    }

    @Test(expected = IllegalArgumentException::class)
    fun setValue_rejectsValue10() {
        defaultCell.setValue(10)
    }

    @Test
    fun setValue_rejectsValueWhenCellIsGiven() {
        val updatedCell = givenCellWithValue3.setValue(5)

        assertEquals(givenCellWithValue3, updatedCell)
    }

    @Test
    fun toggleNote_removesNoteWhenPresent() {
        val updatedCell = cellWithNotes258.setValue(5, InputMode.Notes)

        assertEquals(setOf(2, 8), updatedCell.notes)
        assertCellWithNotes258State()
    }

    @Test
    fun toggleNote_addsNoteWhenNotPresent() {
        val updatedCell = cellWithNotes258.setValue(3, InputMode.Notes)

        assertEquals(setOf(2, 3, 5, 8), updatedCell.notes)
        assertCellWithNotes258State()
    }

    @Test
    fun toggleNote_rejectsNoteWithValue() {
        val updatedCell = cellWithValue3.setValue(5, InputMode.Notes)
        assertEquals(cellWithValue3, updatedCell)
    }
}
