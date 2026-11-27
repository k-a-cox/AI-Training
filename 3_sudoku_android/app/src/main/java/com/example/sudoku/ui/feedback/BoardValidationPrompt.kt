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
    onValidDismiss: () -> Unit,
    onInvalidDismiss: () -> Unit,
) {
    val message = when (validationAnswer) {
        BoardValidationResult.Valid -> "You win!"
        BoardValidationResult.Incomplete -> "The board isn't fully filled in yet."
        is BoardValidationResult.ConflictingCells -> "The board has conflicting values (highlighted). Please fix them."
        BoardValidationResult.ImpossibleSate -> "The game engine has messed up. Please click the \"Reset game\" button to fix."
    }
    val isValid = validationAnswer == BoardValidationResult.Valid
    if (isValid) {
        AlertDialog(
            onDismissRequest = onValidDismiss,
            title = { Text("Game Over") },
            text = { Text(message) },
            confirmButton = { TextButton(onClick = onConfirm) { Text("New game") } },
            dismissButton = { TextButton(onClick = onValidDismiss) { Text("Review board") } })
    } else {
        AlertDialog(
            onDismissRequest = onInvalidDismiss,
            title = { Text("Game needs more work") },
            text = { Text(message) },
            confirmButton = { },
            dismissButton = { TextButton(onClick = onInvalidDismiss) { Text("I'll fix that") } })

    }
}
