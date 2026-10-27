package com.example.sudoku.ui.feedback

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable


@Composable
fun LockGivens(
    isValid: Boolean = true,
    onConfirm: () -> Unit,
    onDismiss: () -> Unit,
) {
    val message = if (isValid) {
        "Are you sure you want to lock in the givens?"
    } else {
        "The current board is invalid. Please update the numbers."
    }
    AlertDialog(
        onDismissRequest = onDismiss,
        title = { Text("Confirm givens") },
        text = { Text(message) },
        confirmButton = { if (isValid) TextButton(onClick = onConfirm) { Text("Lock in givens") } },
        dismissButton = { TextButton(onClick = onDismiss) { Text("Continue updating") } }
    )
}
