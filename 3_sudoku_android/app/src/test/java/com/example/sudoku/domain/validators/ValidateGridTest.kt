package com.example.sudoku.domain.validators

import com.example.sudoku.domain.samples.SampleBoards.boxGoodBoard
import com.example.sudoku.domain.samples.SampleBoards.columnGoodBoard
import com.example.sudoku.domain.samples.SampleBoards.emptyBoard
import com.example.sudoku.domain.samples.SampleBoards.invalidBoxBoard
import com.example.sudoku.domain.samples.SampleBoards.invalidColumnBoard
import com.example.sudoku.domain.samples.SampleBoards.invalidRowBoard
import com.example.sudoku.domain.samples.SampleBoards.rowGoodBoard
import com.example.sudoku.domain.samples.SampleBoards.validFullBoard
import org.junit.Assert.assertEquals
import org.junit.Assert.assertFalse
import org.junit.Assert.assertTrue
import org.junit.Test

class ValidateGridTest {
    @Test
    fun validateGrid_emptyBoard_isValidWhenFullIsNotRequired() {
        assertEquals(
            BoardValidationResult.Valid, ValidateGrid.validateGrid(emptyBoard, needsToBeFull = false)
        )
    }

    @Test
    fun validateGrid_emptyBoard_isIncompleteWhenFullIsRequired() {
        assertEquals(
            BoardValidationResult.Incomplete, ValidateGrid.validateGrid(emptyBoard, needsToBeFull = true)
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
            BoardValidationResult.Valid, ValidateGrid.validateGrid(validFullBoard, needsToBeFull = true)
        )
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
