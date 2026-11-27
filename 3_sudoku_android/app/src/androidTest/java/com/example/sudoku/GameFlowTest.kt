package com.example.sudoku

import androidx.compose.ui.semantics.Role
import androidx.compose.ui.semantics.SemanticsProperties
import androidx.compose.ui.test.SemanticsMatcher
import androidx.compose.ui.test.assert
import androidx.compose.ui.test.assertAll
import androidx.compose.ui.test.assertCountEquals
import androidx.compose.ui.test.hasClickAction
import androidx.compose.ui.test.hasText
import androidx.compose.ui.test.junit4.createAndroidComposeRule
import androidx.compose.ui.test.onNodeWithText
import androidx.compose.ui.test.performClick
import androidx.test.ext.junit.runners.AndroidJUnit4
import org.junit.Rule
import org.junit.Test
import org.junit.runner.RunWith

private fun hasRole(role: Role): SemanticsMatcher =
    SemanticsMatcher.expectValue(SemanticsProperties.Role, role)

@RunWith(AndroidJUnit4::class)
class GameFlowTest {
    @get:Rule
    val composeRule = createAndroidComposeRule<MainActivity>()

    private fun text(resourceId: Int): String = composeRule.activity.getString(resourceId)

    private fun pressDigit(digit: Int) {
        composeRule.onNode(hasRole(Role.Button) and hasText(digit.toString())).performClick()
    }

    private fun enterGiven5AtIndex0AndStartPlaying() {
        composeRule.onAllNodes(hasClickAction())[0].performClick()
        pressDigit(5)
        confirmGivens()
    }

    private fun startPlayingWithOneEmptyCell() {
        composeRule.onNodeWithText(text(R.string.controlAction_pick_board)).performClick()
        composeRule.onNodeWithText(text(R.string.sample_solved_board)).performClick()
        composeRule.onAllNodes(hasClickAction())[0].performClick()
        composeRule.onNodeWithText(text(R.string.controlAction_erase)).performClick()
        composeRule.onAllNodes(hasClickAction())[0].performClick()
        confirmGivens()
    }

    private fun confirmGivens() {
        composeRule.onNodeWithText(text(R.string.controlAction_lock_givens)).performClick()
        composeRule.onNodeWithText(text(R.string.lockGivens_confirm)).performClick()
    }

    @Test
    fun enteringAndErasingDigitUpdatesSelectedCell() {
        val cells = composeRule.onAllNodes(hasClickAction())
        val firstCell = cells[0]

        firstCell.performClick()
        composeRule.onNodeWithText("5").performClick()
        firstCell.assert(hasText("5"))

        composeRule.onNodeWithText(text(R.string.controlAction_erase)).performClick()
        firstCell.assert(!hasText("5"))
    }

    @Test
    fun notesModeAddsAndRemovesCandidates() {
        enterGiven5AtIndex0AndStartPlaying()

        val emptyCell = composeRule.onAllNodes(hasClickAction())[1]
        emptyCell.performClick()
        composeRule.onNodeWithText(text(R.string.controlAction_toggle_notes)).performClick()
        pressDigit(5)
        pressDigit(9)
        emptyCell.assert(hasText("5"))
        emptyCell.assert(hasText("9"))

        pressDigit(5)
        emptyCell.assert(!hasText("5"))
        emptyCell.assert(hasText("9"))

        composeRule.onNodeWithText(text(R.string.controlAction_toggle_notes)).performClick()
        pressDigit(4)
        emptyCell.assert(hasText("4"))
        emptyCell.assert(!hasText("9"))
    }

    @Test
    fun givensCanBeEnteredAndConfirmed() {
        val givenCell = composeRule.onAllNodes(hasClickAction())[0]
        givenCell.performClick()
        pressDigit(5)
        givenCell.assert(hasText("5"))

        composeRule.onNodeWithText(text(R.string.controlAction_lock_givens)).performClick()
        composeRule.onNodeWithText(text(R.string.lockGivens_valid_body))
            .assertExists()
        composeRule.onNodeWithText(text(R.string.lockGivens_confirm)).performClick()

        composeRule.onNodeWithText(text(R.string.controlAction_toggle_notes)).assertExists()
        composeRule.onNodeWithText(text(R.string.controlAction_lock_givens)).assertDoesNotExist()
        givenCell.assert(hasText("5"))
    }

    @Test
    fun incompleteBoardIsReportedBeforeConflicts() {
        enterGiven5AtIndex0AndStartPlaying()

        composeRule.onAllNodes(hasClickAction())[1].performClick()
        pressDigit(5)
        composeRule.onNodeWithText(text(R.string.controlAction_submit_board)).performClick()

        composeRule.onNodeWithText(text(R.string.BoardValidationResult_incomplete_message))
            .assertExists()
        composeRule.onNodeWithText(
            text(R.string.BoardValidationResult_conflicting_message)
        ).assertDoesNotExist()
    }

    @Test
    fun fullBoardWithConflictsIsReported() {
        startPlayingWithOneEmptyCell()

        val emptyCell = composeRule.onAllNodes(hasClickAction())[0]
        emptyCell.performClick()
        pressDigit(3)
        emptyCell.assert(hasText("3"))
        composeRule.onNodeWithText(text(R.string.controlAction_submit_board)).performClick()

        composeRule.onNodeWithText(
            text(R.string.BoardValidationResult_conflicting_message)
        ).assertExists()
        composeRule.onNodeWithText(text(R.string.solutionValidation_invalid_dismiss)).performClick()
        emptyCell.assert(hasText("3"))
    }

    @Test
    fun confirmedGivenCannotBeChangedOrErased() {
        enterGiven5AtIndex0AndStartPlaying()

        val givenCell = composeRule.onAllNodes(hasClickAction())[0]
        pressDigit(4)
        givenCell.assert(hasText("5"))
        composeRule.onNodeWithText(text(R.string.controlAction_erase)).performClick()
        givenCell.assert(hasText("5"))

        composeRule.onNodeWithText(text(R.string.controlAction_toggle_notes)).performClick()
        pressDigit(4)
        givenCell.assert(hasText("5"))
        givenCell.assert(!hasText("4"))
        composeRule.onNodeWithText(text(R.string.controlAction_toggle_notes)).performClick()

        val emptyCell = composeRule.onAllNodes(hasClickAction())[1]
        emptyCell.performClick()
        pressDigit(4)
        emptyCell.assert(hasText("4"))
    }

    @Test
    fun solvedBoardCannotBeEditedAfterReview() {
        startPlayingWithOneEmptyCell()

        val solvedCell = composeRule.onAllNodes(hasClickAction())[0]
        solvedCell.performClick()
        pressDigit(5)
        composeRule.onNodeWithText(text(R.string.controlAction_submit_board)).performClick()
        composeRule.onNodeWithText(text(R.string.solutionValidation_valid_solution)).assertExists()
        composeRule.onNodeWithText(text(R.string.solutionValidation_valid_dismiss)).performClick()

        solvedCell.performClick()
        solvedCell.assert(hasText("5"))
        composeRule.onNode(hasRole(Role.Button) and hasText("5")).assertDoesNotExist()
        composeRule.onNodeWithText(text(R.string.controlAction_erase)).assertDoesNotExist()
    }

    @Test
    fun confirmingNewGameAfterWinningResetsBoard() {
        startPlayingWithOneEmptyCell()

        composeRule.onAllNodes(hasClickAction())[0].performClick()
        pressDigit(5)
        composeRule.onNodeWithText(text(R.string.controlAction_submit_board)).performClick()
        composeRule.onNodeWithText(text(R.string.solutionValidation_valid_solution)).assertExists()
        composeRule.onNodeWithText(text(R.string.solutionValidation_valid_confirm)).performClick()

        val cells = composeRule.onAllNodes(hasClickAction() and !hasRole(Role.Button))
        cells.assertCountEquals(81)
        cells.assertAll(!SemanticsMatcher.keyIsDefined(SemanticsProperties.Text))
        composeRule.onNodeWithText(text(R.string.controlAction_lock_givens)).assertExists()
    }

}
