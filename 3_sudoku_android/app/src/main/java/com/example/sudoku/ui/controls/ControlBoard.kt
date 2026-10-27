package com.example.sudoku.ui.controls

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.RowScope
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.GamePhase
import com.example.sudoku.domain.samples.PuzzleSampleBoards


@Composable
fun ControlBoard(
    gamePhase: GamePhase,
    showNotes: Boolean,
    onAction: (ControlAction) -> Unit,
) {
    Column(horizontalAlignment = Alignment.CenterHorizontally) {
        when (gamePhase) {
            GamePhase.EnteringGivens -> {
                DigitPad(showNotes, onAction)
                ButtonRow {
                    ButtonWrapper("Clear cell", ControlAction.Erase, onAction)
                    ButtonWrapper("Lock in Givens", ControlAction.LockGivens, onAction)
                }
                ButtonRow {
                    ButtonWrapper("New Game", ControlAction.Restart, onAction)
                }
            }

            GamePhase.Playing -> {
                DigitPad(showNotes, onAction)
                ButtonRow {
                    ButtonWrapper("Clear cell", ControlAction.Erase, onAction)
                    ButtonWrapper("Toggle Notes", ControlAction.ToggleNotes, onAction)
                }
                ButtonRow {
                    ButtonWrapper("Submit", ControlAction.SubmitBoard, onAction)
                    ButtonWrapper("New Game", ControlAction.Restart, onAction)
                }
            }

            GamePhase.Solved -> {
                ButtonRow {
                    ButtonWrapper("New Game", ControlAction.Restart, onAction)
                }
            }
        }
        ButtonRow {
            SampleBoardPicker(
                boards = PuzzleSampleBoards.all,
                onBoardSelected = { board -> onAction(ControlAction.PickBoard(board)) },
            )
        }
    }
}

@Composable
private fun DigitPad(showNotes: Boolean, onAction: (ControlAction) -> Unit) {
    NumberPad(
        showNotes = showNotes,
        onDigitClick = { digit -> onAction(ControlAction.Digit(digit)) },
        modifier = Modifier.fillMaxWidth(0.9f),
    )
}

@Composable
private fun ButtonRow(content: @Composable RowScope.() -> Unit) {
    Row(
        modifier = Modifier.padding(top = 8.dp),
        horizontalArrangement = Arrangement.spacedBy(8.dp),
        content = content
    )
}


@Composable
fun ButtonWrapper(
    message: String,
    action: ControlAction,
    onAction: (ControlAction) -> Unit,
) {
    Button(onClick = { onAction(action) }) {
        Text(message)
    }
}

@Composable
private fun ShowUI(gamePhase: GamePhase) {
    ControlBoard(
        gamePhase = gamePhase, showNotes = false, onAction = {})
}

@Preview
@Composable
fun Givens() {
    ShowUI(gamePhase = GamePhase.EnteringGivens)
}

@Preview
@Composable
fun Playing() {
    ShowUI(gamePhase = GamePhase.Playing)
}

@Preview
@Composable
fun Solved() {
    ShowUI(gamePhase = GamePhase.Solved)
}