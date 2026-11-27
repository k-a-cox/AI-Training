package com.example.sudoku.ui.board

import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.test.assertCountEquals
import androidx.compose.ui.test.hasClickAction
import androidx.compose.ui.test.junit4.createComposeRule
import androidx.compose.ui.test.performClick
import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.sudoku.domain.state.SudokuBoardState
import org.junit.Assert.assertEquals
import org.junit.Assert.assertNull
import org.junit.Rule
import org.junit.Test
import org.junit.runner.RunWith

@RunWith(AndroidJUnit4::class)
class SudokuGridTest {
    @get:Rule
    val composeRule = createComposeRule()

    @Test
    fun gridDisplays81ClickableCells() {
        composeRule.setContent {
            SudokuGrid(
                board = SudokuBoardState(),
                cellsToHighlight = emptySet(),
                onCellSelected = {},
                modifier = Modifier.fillMaxWidth()
            )
        }

        composeRule.onAllNodes(hasClickAction()).assertCountEquals(81)
    }

    @Test
    fun clickingCellsUpdatesSelectionAndDeselectsTheSameCell() {
        var board by mutableStateOf(SudokuBoardState())

        composeRule.setContent {
            SudokuGrid(
                board = board,
                cellsToHighlight = emptySet(),
                onCellSelected = { index ->
                    board = board.selectCell(index)
                },
                modifier = Modifier.fillMaxWidth()
            )
        }

        val cells = composeRule.onAllNodes(hasClickAction())
        cells[40].performClick()
        composeRule.runOnIdle {
            assertEquals(40, board.selectedCell)
        }

        cells[40].performClick()
        composeRule.runOnIdle {
            assertNull(board.selectedCell)
        }
    }
}
