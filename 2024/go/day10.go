package main

import (
	"fmt"
	"strings"
)

func day10_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]int, len(lines))

	for i, line := range lines {
		grid[i] = make([]int, len(line))

		for j, char := range line {
			grid[i][j] = int(char - '0')
		}
	}

	directions := [][2]int{
		{-1, 0},
		{1, 0},
		{0, -1},
		{0, 1},
	}

	var explore func(row int, col int, height int, reachableNine map[[2]int]bool)
	explore = func(row int, col int, height int, reachableNine map[[2]int]bool) {
		if row < 0 || row >= len(grid) || col < 0 || col >= len(grid[row]) {
			return
		}

		if grid[row][col] != height {
			return
		}

		currentPosition := [2]int{row, col}

		if height == 9 {
			reachableNine[currentPosition] = true
			return
		}

		for _, dir := range directions {
			nextRow := row + dir[0]
			nextColumn := col + dir[1]
			explore(nextRow, nextColumn, height+1, reachableNine)
		}
	}

	output := 0

	for row := 0; row < len(grid); row++ {
		for col := 0; col < len(grid[row]); col++ {
			if grid[row][col] == 0 {
				reachableNine := make(map[[2]int]bool)
				explore(row, col, 0, reachableNine)

				output += len(reachableNine)
			}
		}
	}

	fmt.Println("Output Day 10 Part 1", output)
}

func day10_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]int, len(lines))

	for i, line := range lines {
		grid[i] = make([]int, len(line))

		for j, char := range line {
			grid[i][j] = int(char - '0')
		}
	}

	directions := [][2]int{
		{-1, 0},
		{1, 0},
		{0, -1},
		{0, 1},
	}

	var explore func(row int, col int, height int) int
	explore = func(row int, col int, height int) int {
		if row < 0 || row >= len(grid) || col < 0 || col >= len(grid[row]) {
			return 0
		}

		if grid[row][col] != height {
			return 0
		}

		if height == 9 {
			return 1
		}

		trails := 0

		for _, dir := range directions {
			nextRow := row + dir[0]
			nextColumn := col + dir[1]
			trails += explore(nextRow, nextColumn, height+1)
		}

		return trails
	}

	output := 0

	for row := 0; row < len(grid); row++ {
		for col := 0; col < len(grid[row]); col++ {
			if grid[row][col] == 0 {
				output += explore(row, col, 0)
			}
		}
	}

	fmt.Println("Output Day 10 Part 2", output)
}
