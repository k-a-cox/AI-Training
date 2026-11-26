package com.example.sudoku.ui.board

import androidx.compose.foundation.layout.width
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.test.hasClickAction
import androidx.compose.ui.test.junit4.createComposeRule
import androidx.compose.ui.test.onNodeWithText
import androidx.compose.ui.test.performClick
import androidx.compose.ui.unit.dp
import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.sudoku.domain.state.SudokuCellState
import org.junit.Assert.assertEquals
import org.junit.Assert.assertTrue
import org.junit.Rule
import org.junit.Test
import org.junit.runner.RunWith

@RunWith(AndroidJUnit4::class)
class SudokuCellTest {
    @get:Rule
    val composeRule = createComposeRule()

    @Test
    fun notesKeepTheirPositionsAndCellRemainsClickable() {
        var notes by mutableStateOf(setOf(1, 5, 9))
        var clicks = 0
        composeRule.setContent {
            SudokuCell(
                state = SudokuCellState(index = 0, notes = notes),
                isSelected = false,
                onClick = { clicks++ },
                modifier = Modifier.width(48.dp)
            )
        }

        fun bounds(digit: Int) = composeRule
            .onNodeWithText(digit.toString(), useUnmergedTree = true)
            .fetchSemanticsNode().boundsInRoot

        val one = bounds(1)
        val five = bounds(5)
        val nine = bounds(9)
        assertTrue(one.center.x < five.center.x && five.center.x < nine.center.x)
        assertTrue(one.center.y < five.center.y && five.center.y < nine.center.y)
        composeRule.onNodeWithText("2", useUnmergedTree = true).assertDoesNotExist()

        composeRule.onNode(hasClickAction()).performClick()
        assertEquals(1, clicks)

        composeRule.runOnIdle { notes = setOf(5) }
        composeRule.onNodeWithText("1", useUnmergedTree = true).assertDoesNotExist()
        composeRule.onNodeWithText("9", useUnmergedTree = true).assertDoesNotExist()
        assertEquals(five, bounds(5))
    }

    @Test
    fun valueIsDisplayedInsteadOfNotes() {
        composeRule.setContent {
            SudokuCell(
                state = SudokuCellState(index = 0, value = 7),
                isSelected = false,
                onClick = {},
                modifier = Modifier.width(48.dp)
            )
        }

        composeRule.onNodeWithText("7", useUnmergedTree = true).assertExists()
    }
}
