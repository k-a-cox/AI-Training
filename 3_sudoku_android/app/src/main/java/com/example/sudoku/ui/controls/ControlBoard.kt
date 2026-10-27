package com.example.sudoku.ui.controls

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.GamePhase


@Composable
fun ControlBoard(
    gamePhase: GamePhase,
    showNotes: Boolean,
    onAction: (ControlAction) -> Unit,
) {
    Column(horizontalAlignment = Alignment.CenterHorizontally) {
        NumberPad(
            showNotes = showNotes,
            onDigitClick = { digit -> onAction(ControlAction.Digit(digit)) },
        )
        Row(
            modifier = Modifier.padding(top = 8.dp),
            horizontalArrangement = Arrangement.spacedBy(8.dp)
        ) {
            when (gamePhase) {
                GamePhase.EnteringGivens -> {
                    ClearButton { onAction(ControlAction.Erase) }
                    Button(onClick = { onAction(ControlAction.AdvancePhase) }) {
                        Text("Lock in Givens")
                    }
                }

                GamePhase.Playing -> {
                    ClearButton { onAction(ControlAction.Erase) }
                    NotesButton { onAction(ControlAction.ToggleNotes) }
                    SubmitButton { onAction(ControlAction.AdvancePhase) }
                }

                GamePhase.Solved -> {
                    RestartButton { onAction(ControlAction.AdvancePhase) }
                }
            }
        }
    }
}
@Composable
fun RestartButton(onClick: () -> Unit) {
    Button(onClick = onClick) {
        Text("Restart")
    }
}

@Composable
fun ClearButton(onClick: () -> Unit) {
    Button(onClick = onClick) {
        Text("Clear")
    }
}

@Composable
fun NotesButton(onClick: () -> Unit) {
    Button(onClick = onClick) {
        Text("Notes")
    }
}

@Composable
fun SubmitButton(onClick: () -> Unit) {
    Button(onClick = onClick) {
        Text("Submit")
    }
}

@Composable
private fun ShowUI(gamePhase: GamePhase) {
    ControlBoard(
        gamePhase = gamePhase,
        showNotes = false,
        onAction = {}
    )
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