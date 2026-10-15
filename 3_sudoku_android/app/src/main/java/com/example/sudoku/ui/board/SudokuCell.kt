package com.example.sudoku.ui.board

import androidx.compose.foundation.background
import androidx.compose.foundation.border
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.aspectRatio
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.width
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.SudokuCellState

@Composable
fun SudokuCell(
    state: SudokuCellState,
    isSelected: Boolean,
    onClick: () -> Unit,
    modifier: Modifier = Modifier
) {
    val backgroundColor =
        if (isSelected) {
            Color.Blue.copy(alpha = 0.5f)
        } else if (state.isGiven) {
            Color.Gray.copy(alpha = 0.5f)
        } else {
            Color.White
        }

    Box(
        modifier = modifier
            .fillMaxWidth()
            .aspectRatio(1f)
            .background(backgroundColor)
            .border(0.5.dp, Color.Gray)
            .clickable(onClick = onClick),
        contentAlignment = Alignment.Center
    ) {
        if (state.value != null) {
            Text(
                text = state.value.toString(),
                style = MaterialTheme.typography.titleMedium,
                fontWeight = if (state.isGiven) FontWeight.Bold else FontWeight.Normal
            )
        } else if (state.notes.isNotEmpty()) {
            Text(
                text = "*"+state.notes.joinToString(", "),
                style = MaterialTheme.typography.bodySmall,
                color = Color.Gray,
                fontWeight = FontWeight.Normal,
                modifier = Modifier.align(Alignment.Center)
            )
        }
    }
}

@Preview(showBackground = true)
@Composable
fun Empty() {
    SudokuCell(
        state = SudokuCellState(index = 0),
        isSelected = false,
        onClick = {},
        modifier = Modifier.width(48.dp)
    )
}

@Preview(showBackground = true)
@Composable
fun Given() {
    SudokuCell(
        state = SudokuCellState(index = 0,value = 2, isGiven = true),
        isSelected = false,
        onClick = {},
        modifier = Modifier.width(48.dp)
    )
}

@Preview(showBackground = true)
@Composable
fun Selected() {
    SudokuCell(
        state = SudokuCellState(index = 0,value = 4),
        isSelected = true,
        onClick = {},
        modifier = Modifier.width(48.dp)
    )
}

@Preview(showBackground = true)
@Composable
fun Notes() {
    SudokuCell(
        state = SudokuCellState(index = 0,notes = setOf(1,2,3,4,5,6,7,8,9)),
        isSelected = false,
        onClick = {},
        modifier = Modifier.width(48.dp)
    )
}