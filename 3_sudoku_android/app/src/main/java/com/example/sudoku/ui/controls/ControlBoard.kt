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


@Composable
fun ControlBoard(onDigitClick: (Int) -> Unit, onEraserClick: () -> Unit) {

    Column(horizontalAlignment = Alignment.CenterHorizontally) {
        NumberPad(onDigitClick = { digit ->
            onDigitClick(digit)
        })
        Row(
            modifier = Modifier.padding(top = 8.dp),
            horizontalArrangement = Arrangement.spacedBy(8.dp)
        ) {
            Button(onClick = { onEraserClick() }) {
                Text("Clear")
            }
            Button(onClick = { /*TODO - state change to toggle onDigitClick to call back to setNote instead of setValue*/ }) {
                Text("Notes")
            }
        }
    }
}

@Preview
@Composable
fun ControlBoardPreview() {
    ControlBoard(onDigitClick = {}, onEraserClick = {})
}