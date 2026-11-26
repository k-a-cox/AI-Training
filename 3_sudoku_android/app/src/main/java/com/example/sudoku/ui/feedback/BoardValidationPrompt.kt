package com.example.sudoku.ui.feedback

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import com.example.sudoku.domain.validators.BoardValidationResult


@Composable
fun BoardValidationPrompt(
    validationAnswer: BoardValidationResult,
    onConfirm: () -> Unit,
    onDismiss: () -> Unit,
) {
    val isValid = validationAnswer == BoardValidationResult.Valid
    val ordinals = listOf(
        "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "ninth"
    )
    val boxes = listOf(
        "top left",
        "top middle",
        "top right",
        "middle left",
        "center",
        "middle right",
        "bottom left",
        "bottom middle",
        "bottom right"
    )

    val message = when (validationAnswer) {
        BoardValidationResult.Valid -> "You win!"
        BoardValidationResult.Incomplete -> "The board isn't fully filled in yet."
        is BoardValidationResult.InvalidRow -> "The ${ordinals[validationAnswer.rowIndex]} row is invalid. Please fix this row."
        is BoardValidationResult.InvalidColumn -> "The ${ordinals[validationAnswer.columnIndex]} column is invalid. Please fix this column."
        is BoardValidationResult.InvalidBox -> "The ${boxes[validationAnswer.boxIndex]} box is invalid. Please fix this box."
    }
    if (isValid) {
        AlertDialog(
            onDismissRequest = onDismiss,
            title = { Text("Game Over") },
            text = { Text(message) },
            confirmButton = { TextButton(onClick = onConfirm) { Text("Start over?") } },
            dismissButton = { TextButton(onClick = onDismiss) { Text("Review board") } })
    } else {
        AlertDialog(
            onDismissRequest = onDismiss,
            title = { Text("Game needs more work") },
            text = { Text(message) },
            confirmButton = { },
            dismissButton = { TextButton(onClick = onDismiss) { Text("I'll fix that") } })

    }
}
