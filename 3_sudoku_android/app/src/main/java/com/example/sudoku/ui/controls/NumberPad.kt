package com.example.sudoku.ui.controls

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.aspectRatio
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp

@Composable
fun NumberPad(
    onDigitClick: (Int) -> Unit,
    modifier: Modifier = Modifier
) {
    Column(modifier = modifier) {
        repeat(3) { row ->
            Row(modifier = Modifier.fillMaxWidth()) {
                repeat(3) { column ->
                    val digit = row * 3 + column + 1
                    Button(
                        onClick = { onDigitClick(digit) },
                        modifier = Modifier
                            .weight(1f)
                            .aspectRatio(1f)
                            .padding(2.dp)
                    ) {
                        Text(digit.toString())
                    }
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun NumberPadPreview() {
    NumberPad(
        onDigitClick = {},
        modifier = Modifier.width(180.dp)
    )
}