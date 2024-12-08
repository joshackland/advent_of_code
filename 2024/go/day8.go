package main

import (
	"fmt"
	"strings"
)

func day8_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]rune, len(lines))

	for i, line := range lines {
		grid[i] = []rune(line)
	}

	antennas := make(map[rune][][2]int)
	antiNodes := make(map[[2]int]bool)

	for row, _ := range grid {
		for col, _ := range grid[row] {
			if grid[row][col] != '.' {
				antennas[grid[row][col]] = append(antennas[grid[row][col]], [2]int{row, col})
			}
		}
	}

	for _, positions := range antennas {
		for i := 0; i < len(positions); i++ {
			for j := i + 1; j < len(positions); j++ {
				pos1 := positions[i]
				pos2 := positions[j]

				deltaRow := pos2[0] - pos1[0]
				deltaCol := pos2[1] - pos1[1]

				antiNode1 := [2]int{pos1[0] - deltaRow, pos1[1] - deltaCol}
				antiNode2 := [2]int{pos2[0] + deltaRow, pos2[1] + deltaCol}

				if antiNode1[0] >= 0 && antiNode1[0] < len(grid) && antiNode1[1] >= 0 && antiNode1[1] < len(grid[0]) {
					antiNodes[antiNode1] = true
				}

				if antiNode2[0] >= 0 && antiNode2[0] < len(grid) && antiNode2[1] >= 0 && antiNode2[1] < len(grid[0]) {
					antiNodes[antiNode2] = true
				}
			}
		}
	}

	output := len(antiNodes)

	fmt.Println("Output Day 8 Part 1", output)
}

func day8_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	grid := make([][]rune, len(lines))

	for i, line := range lines {
		grid[i] = []rune(line)
	}

	antennas := make(map[rune][][2]int)
	antiNodes := make(map[[2]int]bool)

	for row, _ := range grid {
		for col, _ := range grid[row] {
			if grid[row][col] != '.' {
				antennas[grid[row][col]] = append(antennas[grid[row][col]], [2]int{row, col})
			}
		}
	}

	for _, positions := range antennas {
		for i := 0; i < len(positions); i++ {
			for j := i + 1; j < len(positions); j++ {
				pos1 := positions[i]
				pos2 := positions[j]

				deltaRow := pos2[0] - pos1[0]
				deltaCol := pos2[1] - pos1[1]

				antiNode1Row := pos1[0]
				antiNode1Col := pos1[1]

				for antiNode1Row >= 0 && antiNode1Row < len(grid) && antiNode1Col >= 0 && antiNode1Col < len(grid[0]) {
					antiNodes[[2]int{antiNode1Row, antiNode1Col}] = true

					antiNode1Row -= deltaRow
					antiNode1Col -= deltaCol
				}

				antiNode2Row := pos2[0]
				antiNode2Col := pos2[1]

				for antiNode2Row >= 0 && antiNode2Row < len(grid) && antiNode2Col >= 0 && antiNode2Col < len(grid[0]) {
					antiNodes[[2]int{antiNode2Row, antiNode2Col}] = true

					antiNode2Row += deltaRow
					antiNode2Col += deltaCol
				}
			}
		}
	}

	output := len(antiNodes)

	fmt.Println("Output Day 8 Part 2", output)
}
