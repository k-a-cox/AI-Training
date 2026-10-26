package com.example.sudoku.ui.board

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.sudoku.domain.state.SudokuBoardState
import com.example.sudoku.domain.state.SudokuCellState

private const val BOX_SIZE = 3
private const val BOXES_PER_GRID = 3
private const val GRID_SIZE = BOXES_PER_GRID * BOX_SIZE

@Composable
fun SudokuGrid(
    board: SudokuBoardState,
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
                        onCellSelected = onCellSelected,
                        boxRow = boxRow,
                        boxColumn = boxColumn,
                        modifier = Modifier.weight(1f)
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
    onCellSelected: (Int) -> Unit,
    modifier: Modifier = Modifier
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
                        onClick = { onCellSelected(cellIndex) },
                        modifier = Modifier.weight(1f)
                    )
                }
            }
        }
    }
}

private val sampleValues = mapOf(
    0 to 5, 1 to 3, 4 to 7, 8 to 2,
    9 to 6, 11 to 2, 13 to 1, 17 to 8,
    18 to 1, 20 to 8, 24 to 7,
    27 to 8, 30 to 6, 35 to 3,
    36 to 4, 40 to 5, 44 to 1,
    54 to 7, 58 to 1, 62 to 6,
    72 to 3, 76 to 8, 80 to 9
)

private val sampleGivenIndices = setOf(
    0, 4, 11, 18, 30, 40, 54, 76
)

private val sampleNotes = mapOf(
    3 to setOf(2, 4, 7),
    5 to setOf(3, 6, 8),
    6 to setOf(2, 5),
    10 to setOf(4, 7, 8),
    12 to setOf(2, 3, 6),
    14 to setOf(5, 8),
    16 to setOf(2, 4, 6),
    19 to setOf(3, 5, 7),
    21 to setOf(2, 6, 8),
    22 to setOf(4, 5),
    25 to setOf(3, 7, 8),
    26 to setOf(2, 5, 6)
)

fun getSampleBoard(): SudokuBoardState {
    return SudokuBoardState(
        cells = List(81) { index ->
            SudokuCellState(
                index = index,
                value = sampleValues[index],
                isGiven = index in sampleGivenIndices,
                notes = sampleNotes[index].orEmpty()
            )
        },
        selectedCell = 39
    )
}

@Preview
@Composable
fun SampleGame() {
    SudokuGrid(
        getSampleBoard(),
        onCellSelected = { }
    )
}
