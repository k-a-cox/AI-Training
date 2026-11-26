package com.example.sudoku.ui.feedback

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import com.example.sudoku.domain.validators.BoardValidationResult


@Composable
fun LockGivens(
    isValid: Boolean = true,
    validationAnswer: BoardValidationResult = BoardValidationResult.Valid,
    onConfirm: () -> Unit,
    onDismiss: () -> Unit,
) {
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

    val message = if (isValid) {
        "Are you sure you want to lock in the givens?"
    } else {
        when (validationAnswer) {
            BoardValidationResult.Valid,
            BoardValidationResult.Incomplete -> "The current board needs givens. Please add some."

            is BoardValidationResult.InvalidRow ->
                "The ${ordinals[validationAnswer.rowIndex]} row is invalid. Please fix this row."

            is BoardValidationResult.InvalidColumn ->
                "The ${ordinals[validationAnswer.columnIndex]} column is invalid. Please fix this column."

            is BoardValidationResult.InvalidBox ->
                "The ${boxes[validationAnswer.boxIndex]} box is invalid. Please fix this box."
        }
    }
    AlertDialog(
        onDismissRequest = onDismiss,
        title = { Text("Confirm givens") },
        text = { Text(message) },
        confirmButton = { if (isValid) TextButton(onClick = onConfirm) { Text("Lock in givens") } },
        dismissButton = { TextButton(onClick = onDismiss) { Text("Continue updating") } }
    )
}
