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
    onDigitClick: (Int) -> Unit,
    onEraserClick: () -> Unit,
    onNotesClick: () -> Unit,
    onPhaseGameClick: () -> Unit,
    gamePhase: GamePhase,
    showNotes: Boolean = false,
) {

    Column(horizontalAlignment = Alignment.CenterHorizontally) {
        NumberPad(showNotes = showNotes, onDigitClick = { digit ->
            onDigitClick(digit)
        })
        when (gamePhase) {
            GamePhase.EnteringGivens -> {
                Row(
                    modifier = Modifier.padding(top = 8.dp),
                    horizontalArrangement = Arrangement.spacedBy(8.dp)
                ) {
                    Button(onClick = { onEraserClick() }) {
                        Text("Clear")
                    }
                    Button(onClick = { onPhaseGameClick() }) {
                        Text("Lock in Givens")
                    }
                }
            }

            GamePhase.Playing -> {
                Row(
                    modifier = Modifier.padding(top = 8.dp),
                    horizontalArrangement = Arrangement.spacedBy(8.dp)
                ) {
                    ClearButton(onClick = { onEraserClick() })
                    NotesButton(onClick = { onNotesClick() })
                    SubmitButton(onClick = { onPhaseGameClick() })
                }
            }

            GamePhase.Solved -> {
                Row(
                    modifier = Modifier.padding(top = 8.dp),
                    horizontalArrangement = Arrangement.spacedBy(8.dp)
                ) {
                    RestartButton(onClick = { onPhaseGameClick() })
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
        onDigitClick = {},
        onEraserClick = {},
        onNotesClick = {},
        onPhaseGameClick = {},
        gamePhase = gamePhase,
        showNotes = false
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