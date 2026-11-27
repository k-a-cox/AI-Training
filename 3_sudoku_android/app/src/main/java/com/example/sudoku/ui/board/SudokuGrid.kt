package com.example.sudoku.ui.board

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.samples.PuzzleSampleBoards
import com.example.sudoku.domain.samples.SampleBoards
import com.example.sudoku.domain.state.SudokuBoardState

private const val BOX_SIZE = 3
private const val BOXES_PER_GRID = 3
private const val GRID_SIZE = BOXES_PER_GRID * BOX_SIZE

@Composable
fun SudokuGrid(
    board: SudokuBoardState,
    cellsToHighlight: Set<Int>,
    onCellSelected: (Int) -> Unit,
    modifier: Modifier = Modifier,
) {
    Column(
        modifier = modifier
            .border(4.dp, Color.Black)
    ) {
        repeat(BOXES_PER_GRID) { boxRow ->
            Row {
                repeat(BOXES_PER_GRID) { boxColumn ->
                    SudokuBox(
                        board = board,
                        boxRow = boxRow,
                        boxColumn = boxColumn,
                        cellsToHighlight = cellsToHighlight,
                        onCellSelected = onCellSelected,
                        modifier = Modifier.weight(1f),
                    )
                }
            }
        }
    }
}

@Composable
private fun SudokuBox(
    board: SudokuBoardState,
    boxRow: Int,
    boxColumn: Int,
    cellsToHighlight: Set<Int>,
    onCellSelected: (Int) -> Unit,
    modifier: Modifier = Modifier,
) {
    Column(
        modifier = modifier
            .border(2.dp, Color.Black)
    ) {
        repeat(BOX_SIZE) { rowInBox ->
            Row {
                repeat(BOX_SIZE) { columnInBox ->
                    val row = boxRow * BOX_SIZE + rowInBox
                    val column = boxColumn * BOX_SIZE + columnInBox
                    val cellIndex = row * GRID_SIZE + column
                    SudokuCell(
                        board.cells[cellIndex],
                        isSelected = board.selectedCell == cellIndex,
                        hasConflict = cellIndex in cellsToHighlight,
                        onClick = { onCellSelected(cellIndex) },
                        modifier = Modifier.weight(1f),
                    )
                }
            }
        }
    }
}

@Preview
@Composable
fun SampleGame() {
    SudokuGrid(
        SampleBoards.partialGameBoard,
        cellsToHighlight = emptySet(),
        onCellSelected = { }
    )
}

@Preview
@Composable
fun SampleGame2() {
    SudokuGrid(
        PuzzleSampleBoards.uniqueEasyBoard,
        cellsToHighlight = emptySet(),
        onCellSelected = { }
    )
}

@Preview
@Composable
fun SampleGame3() {
    SudokuGrid(
        PuzzleSampleBoards.uniqueEasyBoard,
        cellsToHighlight = setOf(1, 2, 4, 80, 24, 59),
        onCellSelected = { }
    )
}