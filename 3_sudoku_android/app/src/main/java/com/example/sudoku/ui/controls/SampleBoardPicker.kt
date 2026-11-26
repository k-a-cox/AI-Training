package com.example.sudoku.ui.controls

import androidx.compose.foundation.layout.Box
import androidx.compose.material3.Button
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import com.example.sudoku.domain.state.SudokuBoardState

@Composable
fun SampleBoardPicker(
    boards: List<Pair<String, SudokuBoardState>>,
    onBoardSelected: (SudokuBoardState) -> Unit,
    modifier: Modifier = Modifier,
) {
    var expanded by remember { mutableStateOf(false) }
    var label by remember { mutableStateOf("Load sample board") }

    Box(modifier = modifier) {
        Button(onClick = { expanded = true }) { Text(label) }
        DropdownMenu(expanded = expanded, onDismissRequest = { expanded = false }) {
            boards.forEach { (name, board) ->
                DropdownMenuItem(
                    text = { Text(name) },
                    onClick = {
                        label = name
                        onBoardSelected(board)
                        expanded = false
                    }
                )
            }
        }
    }
}