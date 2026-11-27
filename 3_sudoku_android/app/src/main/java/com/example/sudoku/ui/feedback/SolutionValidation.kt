package com.example.sudoku.ui.feedback

import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource
import com.example.sudoku.R
import com.example.sudoku.domain.validators.BoardValidationResult


@Composable
fun SolutionValidation(
    validationAnswer: BoardValidationResult,
    onConfirm: () -> Unit,
    onValidDismiss: () -> Unit,
    onInvalidDismiss: () -> Unit,
) {
    val message = when (validationAnswer) {
        BoardValidationResult.Valid -> stringResource(R.string.solutionValidation_valid_solution)
        BoardValidationResult.Incomplete -> stringResource(R.string.BoardValidationResult_incomplete_message)
        is BoardValidationResult.ConflictingCells -> stringResource(R.string.BoardValidationResult_conflicting_message)
        BoardValidationResult.ImpossibleSate -> stringResource(R.string.BoardValidationResult_impossible_message)
    }
    val isValid = validationAnswer == BoardValidationResult.Valid
    if (isValid) {
        AlertDialog(
            onDismissRequest = onValidDismiss,
            title = { Text(stringResource(R.string.solutionValidation_valid_title)) },
            text = { Text(message) },
            confirmButton = { TextButton(onClick = onConfirm) { Text(stringResource(R.string.solutionValidation_valid_confirm)) } },
            dismissButton = { TextButton(onClick = onValidDismiss) { Text(stringResource(R.string.solutionValidation_valid_dismiss)) } })
    } else {
        AlertDialog(
            onDismissRequest = onInvalidDismiss,
            title = { Text(stringResource(R.string.solutionValidation_invalid_title)) },
            text = { Text(message) },
            confirmButton = { },
            dismissButton = { TextButton(onClick = onInvalidDismiss) { Text(stringResource(R.string.solutionValidation_invalid_dismiss)) } })

    }
}
