package com.example.sudoku.ui.feedback

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import com.example.sudoku.domain.validators.BoardValidationResult


@Composable
fun LockGivens(
    validationAnswer: BoardValidationResult = BoardValidationResult.Valid,
    onConfirm: () -> Unit,
    onDismiss: () -> Unit,
) {
    val message = when (validationAnswer) {
        BoardValidationResult.Valid -> "Are you sure you want to lock in the givens?"
        BoardValidationResult.Incomplete -> "The current board needs givens. Please add some."
        BoardValidationResult.ImpossibleSate -> "The game engine has messed up. Please click the \"Reset game\" button to fix."
        is BoardValidationResult.ConflictingCells -> "The board has conflicting values (highlighted). Please fix them."
    }

    val isValid = validationAnswer == BoardValidationResult.Valid

    AlertDialog(
        onDismissRequest = onDismiss,
        title = { Text("Confirm givens") },
        text = { Text(message) },
        confirmButton = { if (isValid) TextButton(onClick = onConfirm) { Text("Lock in givens") } },
        dismissButton = { TextButton(onClick = onDismiss) { Text("Continue updating") } }
    )
}
