package com.example.sudoku.ui.controls

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.aspectRatio
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp

@Composable
fun NumberPad(
    showNotes: Boolean, onDigitClick: (Int) -> Unit, modifier: Modifier = Modifier
) {
    Column(modifier = modifier) {
        repeat(3) { row ->
            Row(modifier = Modifier.fillMaxWidth()) {
                repeat(3) { column ->
                    val digit = row * 3 + column + 1
                    OutlinedButton(
                        onClick = { onDigitClick(digit) },
                        modifier = Modifier
                            .weight(1f)
                            .aspectRatio(1f)
                            .padding(2.dp)
                            .getModifier(showNotes),
                        colors = ButtonDefaults.buttonColors(
                            containerColor = getBackgroundColor(showNotes),
                            contentColor = getTextColor(showNotes)
                        )
                    ) {
                        Text(digit.toString())
                    }
                }
            }
        }
    }
}

private fun getBackgroundColor(showNotes: Boolean): Color {
    return when (showNotes) {
        false -> Color.Blue
        true -> Color.Magenta
    }
}

private fun getTextColor(showNotes: Boolean): Color {
    return when (showNotes) {
        false -> Color.White
        true -> Color.Black
    }
}

private fun Modifier.getModifier(showNotes: Boolean): Modifier {
    return when (showNotes) {
        false -> this.width(180.dp)
        true -> this.border(2.dp, Color.Black)
    }
}

@Preview(showBackground = true)
@Composable
fun Value() {
    NumberPad(
        showNotes = false, onDigitClick = {}, modifier = Modifier.width(180.dp)
    )
}


@Preview(showBackground = true)
@Composable
fun Notes() {
    NumberPad(
        showNotes = true, onDigitClick = {}, modifier = Modifier.width(180.dp)
    )
}