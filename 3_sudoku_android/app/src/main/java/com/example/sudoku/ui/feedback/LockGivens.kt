package com.example.sudoku.ui.feedback

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource
import com.example.sudoku.R
import com.example.sudoku.domain.validators.BoardValidationResult


@Composable
fun LockGivens(
    validationAnswer: BoardValidationResult = BoardValidationResult.Valid,
    onConfirm: () -> Unit,
    onDismiss: () -> Unit,
) {
    val message = when (validationAnswer) {
        BoardValidationResult.Valid -> stringResource(R.string.lockGivens_valid_body)
        BoardValidationResult.Incomplete -> stringResource(R.string.lockGivens_empty)
        BoardValidationResult.ImpossibleSate -> stringResource(R.string.BoardValidationResult_impossible_message)
        is BoardValidationResult.ConflictingCells -> stringResource(R.string.BoardValidationResult_conflicting_message)
    }

    val isValid = validationAnswer == BoardValidationResult.Valid

    AlertDialog(
        onDismissRequest = onDismiss,
        title = { Text(stringResource(R.string.lockGivens_title)) },
        text = { Text(message) },
        confirmButton = { if (isValid) TextButton(onClick = onConfirm) { Text(stringResource(R.string.lockGivens_confirm)) } },
        dismissButton = { TextButton(onClick = onDismiss) { Text(stringResource(R.string.lockGivens_dismiss)) } }
    )
}
