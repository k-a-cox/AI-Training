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
import androidx.compose.ui.graphics.lerp
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.state.SudokuCellState

@Composable
fun SudokuCell(
    state: SudokuCellState, isSelected: Boolean, onClick: () -> Unit, modifier: Modifier = Modifier
) {
    val backgroundColor = getBackgroundColor(state, isSelected)
    val fontColor = getFontColor(state, isSelected)

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
                fontWeight = if (state.isGiven) FontWeight.Bold else FontWeight.Normal,
                color = fontColor
            )
        } else if (state.notes.isNotEmpty()) {
            Text(
                text = "*" + state.notes.joinToString(", "),
                style = MaterialTheme.typography.bodySmall,
                color = fontColor,
                fontWeight = FontWeight.Normal,
                modifier = Modifier.align(Alignment.Center)
            )
        }
    }
}

internal enum class CellVisualState { Empty, Given, Selected, Notes }

fun getBackgroundColor(state: SudokuCellState, isSelected: Boolean): Color {
    return when {
        isSelected && state.isGiven -> lerp(Color.Red, Color.DarkGray, 0.5f)
        isSelected -> Color.Red.copy(alpha = 0.5f)
        state.isGiven -> Color.DarkGray.copy(alpha = 0.5f)
        else -> Color.White
    }
}

fun getFontColor(state: SudokuCellState, isSelected: Boolean): Color {
    return when {
        state.isGiven -> Color.Black
        isSelected -> Color.Black
        state.notes.isNotEmpty() -> Color.Gray
        else -> Color.Black
    }
}

@Composable
private fun ShowCell(
    value: Int? = null,
    isSelected: Boolean = false,
    isGiven: Boolean = false,
    notes: Set<Int> = emptySet()
) {
    SudokuCell(
        state = SudokuCellState(index = 0, value = value, isGiven = isGiven, notes = notes),
        isSelected = isSelected,
        onClick = {},
        modifier = Modifier.width(48.dp)
    )
}

@Preview(showBackground = true)
@Composable
fun Empty() {
    ShowCell()
}

@Preview(showBackground = true)
@Composable
fun Selected() {
    ShowCell(isSelected = true)
}

@Preview(showBackground = true)
@Composable
fun Value() {
    ShowCell(value = 2)
}

@Preview(showBackground = true)
@Composable
fun ValueSelected() {
    ShowCell(value = 2, isSelected = true)
}

@Preview(showBackground = true)
@Composable
fun Given() {
    ShowCell(value = 2, isGiven = true)
}

@Preview(showBackground = true)
@Composable
fun GivenSelected() {
    ShowCell(value = 2, isGiven = true, isSelected = true)
}


@Preview(showBackground = true)
@Composable
fun Notes() {
    ShowCell(notes = setOf(1, 2, 3, 4, 5, 6, 7, 8, 9))
}

@Preview(showBackground = true)
@Composable
fun NotesSelected() {
    ShowCell(notes = setOf(1, 2, 3, 4, 5, 6, 7, 8, 9), isSelected = true)
}
