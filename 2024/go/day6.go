package main

import (
	"fmt"
	"strings"
)

func day6_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]rune, len(lines))

	for i, line := range lines {
		grid[i] = []rune(line)
	}
	output := 0

	guardRow := -1
	guardCol := -1
	guardDirection := 0

	for row, _ := range grid {
		if guardRow >= 0 {
			break
		}
		for col, _ := range grid[row] {
			if grid[row][col] == '^' {
				guardRow = row
				guardCol = col
				break
			}
		}
	}

	directions := [][2]int{
		{-1, 0},
		{0, 1},
		{1, 0},
		{0, -1},
	}

	visitedLocations := make(map[[2]int]bool)

	for {
		visitedLocations[[2]int{guardRow, guardCol}] = true

		currentDirection := directions[guardDirection]
		nextGuardRow := guardRow + currentDirection[0]
		nextGuardCol := guardCol + currentDirection[1]

		if nextGuardRow < 0 || nextGuardRow >= len(grid) || nextGuardCol < 0 || nextGuardCol >= len(grid[0]) {
			break
		}

		if grid[nextGuardRow][nextGuardCol] == '#' {
			guardDirection = (guardDirection + 1) % 4
			currentDirection = directions[guardDirection]
			nextGuardRow = guardRow + currentDirection[0]
			nextGuardCol = guardCol + currentDirection[1]

			if nextGuardRow < 0 || nextGuardRow >= len(grid) || nextGuardCol < 0 || nextGuardCol >= len(grid[0]) {
				break
			}
		}
		guardRow = nextGuardRow
		guardCol = nextGuardCol
	}
	output = len(visitedLocations)

	fmt.Println("Output Day 6 Part 1", output)
}

func day6_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]rune, len(lines))

	for i, line := range lines {
		grid[i] = []rune(line)
	}
	output := 0

	guardRow := -1
	guardCol := -1
	guardDirection := 0

	for row, _ := range grid {
		if guardRow >= 0 {
			break
		}
		for col, _ := range grid[row] {
			if grid[row][col] == '^' {
				guardRow = row
				guardCol = col
				break
			}
		}
	}

	directions := [][2]int{
		{-1, 0},
		{0, 1},
		{1, 0},
		{0, -1},
	}

	for row := 0; row < len(grid); row++ {
		for col := 0; col < len(grid[row]); col++ {
			if grid[row][col] != '.' {
				continue
			}

			grid[row][col] = '#'

			visitedLocations := make(map[[3]int]bool)
			currentRow := guardRow
			currentCol := guardCol
			currentDirection := guardDirection

			loopDetected := false

			for {
				guardState := [3]int{currentRow, currentCol, currentDirection}
				if visitedLocations[guardState] {
					loopDetected = true
					break
				}

				visitedLocations[guardState] = true

				nextGuardRow := currentRow + directions[currentDirection][0]
				nextGuardCol := currentCol + directions[currentDirection][1]

				if nextGuardRow < 0 || nextGuardRow >= len(grid) || nextGuardCol < 0 || nextGuardCol >= len(grid[0]) {
					break
				}

				if grid[nextGuardRow][nextGuardCol] == '#' {
					currentDirection = (currentDirection + 1) % 4
				} else {
					currentRow = nextGuardRow
					currentCol = nextGuardCol
				}
			}

			if loopDetected {
				output++
			}

			grid[row][col] = '.'
		}
	}

	fmt.Println("Output Day 6 Part 2", output)
}
