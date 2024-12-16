package main

import (
	"fmt"
	"strings"
)

type PointD15 struct {
	x int
	y int
}

func day15_1(input string) {
	split := strings.Split(strings.TrimSpace(input), "\n\n")
	grid := make([][]rune, len(strings.Split(split[0], "\n")))

	output := 0

	var robot PointD15
	posFound := false
	instructions := []rune{}

	for i, line := range strings.Split(split[0], "\n") {
		grid[i] = []rune(line)

		if !posFound {
			for j := 0; j < len(line); j++ {
				if line[j] == '@' {
					robot = PointD15{
						i, j,
					}
					posFound = true
					break
				}
			}
		}
	}

	for _, line := range strings.Split(split[1], "\n") {
		for _, instruction := range line {
			instructions = append(instructions, instruction)
		}
	}

	directions := map[rune]PointD15{
		'^': PointD15{0, -1},
		'>': PointD15{1, 0},
		'v': PointD15{0, 1},
		'<': PointD15{-1, 0},
	}

	for _, instruction := range instructions {
		dir := directions[instruction]
		// fmt.Println(robot, string(instruction), dir)
		// for _, line := range grid {
		// 	fmt.Println(string(line))
		// }

		nextPosition := PointD15{robot.x + dir.x, robot.y + dir.y}

		if grid[nextPosition.y][nextPosition.x] == '#' {
			continue
		} else if grid[nextPosition.y][nextPosition.x] == '.' {
			grid[robot.y][robot.x] = '.'
			robot = PointD15{nextPosition.x, nextPosition.y}
			grid[robot.y][robot.x] = '@'
		} else if grid[nextPosition.y][nextPosition.x] == 'O' {
			nextBox := PointD15{nextPosition.x + dir.x, nextPosition.y + dir.y}
			for grid[nextBox.y][nextBox.x] == 'O' {
				nextBox = PointD15{nextBox.x + dir.x, nextBox.y + dir.y}
			}
			if grid[nextBox.y][nextBox.x] == '#' {
				continue
			} else if grid[nextBox.y][nextBox.x] == '.' {
				grid[nextBox.y][nextBox.x] = 'O'
				grid[robot.y][robot.x] = '.'
				robot = PointD15{nextPosition.x, nextPosition.y}
				grid[robot.y][robot.x] = '@'
			}
		}
	}

	for y := 1; y < len(grid); y++ {
		for x := 1; x < len(grid[y]); x++ {
			if grid[y][x] == 'O' {
				output += 100*y + x
			}
		}
	}

	fmt.Println("Output Day 15 Part 1", output)
}

func day15_2(input string) {
	split := strings.Split(strings.TrimSpace(input), "\n\n")
	grid := make([][]rune, len(strings.Split(split[0], "\n")))

	output := 0

	var robot PointD15
	instructions := []rune{}

	for i, line := range strings.Split(split[0], "\n") {
		gridLine := []rune{}

		for _, r := range line {
			if r == '#' {
				gridLine = append(gridLine, '#')
				gridLine = append(gridLine, '#')
			} else if r == 'O' {
				gridLine = append(gridLine, '[')
				gridLine = append(gridLine, ']')
			} else if r == '.' {
				gridLine = append(gridLine, '.')
				gridLine = append(gridLine, '.')
			} else if r == '@' {
				robot = PointD15{
					len(gridLine),
					i,
				}
				gridLine = append(gridLine, '@')
				gridLine = append(gridLine, '.')
			}
		}
		grid[i] = gridLine
	}

	for _, line := range strings.Split(split[1], "\n") {
		for _, instruction := range line {
			instructions = append(instructions, instruction)
		}
	}

	directions := map[rune]PointD15{
		'^': PointD15{0, -1},
		'>': PointD15{1, 0},
		'v': PointD15{0, 1},
		'<': PointD15{-1, 0},
	}

	// for _, line := range grid {
	// 	fmt.Println(string(line))
	// }
	for _, instruction := range instructions {
		dir := directions[instruction]
		// fmt.Println(string(instruction))

		nextPosition := PointD15{robot.x + dir.x, robot.y + dir.y}

		if grid[nextPosition.y][nextPosition.x] == '#' {
			continue
		} else if grid[nextPosition.y][nextPosition.x] == '.' {
			grid[robot.y][robot.x] = '.'
			robot = PointD15{nextPosition.x, nextPosition.y}
			grid[robot.y][robot.x] = '@'
		} else if grid[nextPosition.y][nextPosition.x] == '[' || grid[nextPosition.y][nextPosition.x] == ']' {
			boxes := [][][2]PointD15{}

			if grid[nextPosition.y][nextPosition.x] == '[' {
				boxes = append(boxes, [][2]PointD15{{
					PointD15{nextPosition.x, nextPosition.y},
					PointD15{nextPosition.x + 1, nextPosition.y},
				}})
			} else {
				boxes = append(boxes, [][2]PointD15{{
					PointD15{nextPosition.x - 1, nextPosition.y},
					PointD15{nextPosition.x, nextPosition.y},
				}})
			}

			currentBoxes := boxes[0]

			wallFound := false
			for len(currentBoxes) > 0 && !wallFound {
				nextBoxes := [][2]PointD15{}
				for _, box := range currentBoxes {
					if grid[box[0].y+dir.y][box[0].x+dir.x] == '#' || grid[box[1].y+dir.y][box[1].x+dir.x] == '#' {
						wallFound = true
						break
					}

					if instruction == '^' || instruction == 'v' {
						if grid[box[0].y+dir.y][box[0].x+dir.x] == '[' {
							nextBoxes = append(nextBoxes, [2]PointD15{
								PointD15{box[0].x, box[0].y + dir.y},
								PointD15{box[0].x + 1, box[0].y + dir.y},
							})
						} else if grid[box[0].y+dir.y][box[0].x+dir.x] == ']' {
							nextBoxes = append(nextBoxes, [2]PointD15{
								PointD15{box[0].x - 1, box[0].y + dir.y},
								PointD15{box[0].x, box[0].y + dir.y},
							})
						}
						if grid[box[1].y+dir.y][box[1].x+dir.x] == '[' {
							nextBoxes = append(nextBoxes, [2]PointD15{
								PointD15{box[1].x, box[1].y + dir.y},
								PointD15{box[1].x + 1, box[1].y + dir.y},
							})
						}

					} else if instruction == '<' || instruction == '>' {
						if instruction == '<' {
							if grid[box[0].y+dir.y][box[0].x+dir.x] == ']' {
								nextBoxes = append(nextBoxes, [2]PointD15{
									PointD15{box[0].x + dir.x*2, box[0].y + dir.y},
									PointD15{box[0].x + dir.x, box[0].y + dir.y},
								})
							}
						} else {
							if grid[box[1].y+dir.y][box[1].x+dir.x] == '[' {
								nextBoxes = append(nextBoxes, [2]PointD15{
									PointD15{box[1].x + dir.x, box[1].y + dir.y},
									PointD15{box[1].x + dir.x*2, box[1].y + dir.y},
								})
							}
						}
					}
				}

				if len(boxes) > 0 {
					boxes = append(boxes, nextBoxes)
				}
				currentBoxes = nextBoxes
			}

			if wallFound {
				continue
			}

			if len(boxes) > 0 {
				for i := len(boxes) - 1; i >= 0; i-- {
					for _, box := range boxes[i] {
						grid[box[0].y][box[0].x] = '.'
						grid[box[1].y][box[1].x] = '.'
						grid[box[0].y+dir.y][box[0].x+dir.x] = '['
						grid[box[1].y+dir.y][box[1].x+dir.x] = ']'
						// for _, line := range grid {
						// 	fmt.Println(string(line))
						// }
					}
				}
			}

			grid[robot.y][robot.x] = '.'
			robot = PointD15{nextPosition.x, nextPosition.y}
			grid[robot.y][robot.x] = '@'
			// for _, line := range grid {
			// 	fmt.Println(string(line))
			// }
		}

		// for _, line := range grid {
		// 	fmt.Println(string(line))
		// }
	}

	for y := 1; y < len(grid); y++ {
		for x := 1; x < len(grid[y]); x++ {
			if grid[y][x] == '[' {
				output += 100*y + x
				x++
			}
		}
	}

	fmt.Println("Output Day 15 Part 2", output)
}
