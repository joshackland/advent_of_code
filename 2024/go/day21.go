// my broken code which doesn't work :)
package main

import (
	"fmt"
	"strconv"
	"strings"
)

func day21_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	buttonGroups := make([][]rune, len(lines))

	for i, line := range lines {
		buttonGroups[i] = []rune(line)
	}

	invalidNumeric := Point{0, 3}
	numericKeypad := map[rune]Point{
		'7': Point{0, 0},
		'8': Point{1, 0},
		'9': Point{2, 0},
		'4': Point{0, 1},
		'5': Point{1, 1},
		'6': Point{2, 1},
		'1': Point{0, 2},
		'2': Point{1, 2},
		'3': Point{2, 2},
		'0': Point{1, 3},
		'A': Point{2, 3},
	}

	invalidRobot := Point{0, 0}
	robotKeypad := map[rune]Point{
		'^': Point{1, 0},
		'A': Point{2, 0},
		'<': Point{0, 1},
		'v': Point{1, 1},
		'>': Point{2, 1},
	}

	directions := map[rune]Point{
		'^': Point{0, -1},
		'<': Point{-1, 0},
		'v': Point{0, 1},
		'>': Point{1, 0},
	}

	// personState := Point{2, 0}
	robot2State := Point{2, 0}
	robot1State := Point{2, 0}
	doorState := Point{2, 3}

	for _, buttons := range buttonGroups {
		humanButtonsPressed := ""
		numericButtons := -1

		numString := ""

		for _, button := range buttons {
			if button >= '0' && button <= '9' {
				numString += string(button)
			}
		}

		numericButtons, _ = strconv.Atoi(numString)

		for _, button := range buttons {
			doorTarget := numericKeypad[button]
			doorDistance := Point{doorTarget.x - doorState.x, doorTarget.y - doorState.y}

			doorXDir := 1
			doorYDir := 1
			if doorDistance.x < 0 {
				doorXDir = -1
			}
			if doorDistance.y < 0 {
				doorYDir = -1
			}

			for doorState != doorTarget {
				robot1Button := '>'
				nextRobot1X := Point{doorState.x - doorXDir, doorState.y}
				if doorDistance.x != 0 && doorXDir < 0 && nextRobot1X != invalidNumeric {
					robot1Button = '<'
				} else {
					robot1Button = 'v'
					if doorYDir < 0 {
						robot1Button = '^'
					}
				}

				robot1Target := robotKeypad[robot1Button]
				robot1Distance := Point{robot1Target.x - robot1State.x, robot1Target.y - robot1State.y}

				robot1XDir := 1
				robot1YDir := 1
				if robot1Distance.x < 0 {
					robot1XDir = -1
				}
				if robot1Distance.y < 0 {
					robot1YDir = -1
				}

				robot1Dir := directions[robot1Button]
				doorState.x += robot1Dir.x
				doorState.y += robot1Dir.y

				for {
					robot2Button := '>'
					nextRobot2X := Point{robot1State.x - robot1XDir, robot1State.y}
					if robot1Distance.x != 0 && robot1XDir < 0 && nextRobot2X != invalidRobot {
						robot2Button = '<'
					} else {
						robot2Button = 'v'
						if robot1YDir < 0 {
							robot2Button = '^'
						}
					}

					robot2Target := robotKeypad[robot2Button]
					robot2Distance := Point{robot2Target.x - robot2State.x, robot2Target.y - robot2State.y}

					robot2XDir := 1
					robot2YDir := 1
					if robot2Distance.x < 0 {
						robot2XDir = -1
					}
					if robot2Distance.y < 0 {
						robot2YDir = -1
					}

					robot2Dir := directions[robot2Button]
					robot1State.x += robot2Dir.x
					robot1State.y += robot2Dir.y

					if robot1State == robot1Target {
						humanButtonsPressed += "A"
						break
					}

					for {
						humanButton := '>'
						nextHumanX := Point{robot2State.x - robot2XDir, robot2State.y}
						if robot2State.x != robot2Target.x && robot2XDir < 0 && nextHumanX != invalidRobot {
							humanButton = '<'
						} else {
							humanButton = 'v'
							if robot2YDir < 0 {
								humanButton = '^'
							}
						}

						humanButtonsPressed += string(humanButton)

						humanDir := directions[humanButton]
						robot2State.x += humanDir.x
						robot2State.y += humanDir.y

						if robot2State == robot2Target {
							humanButtonsPressed += "A"
							break
						}
					}

					robot1State.x += robot2Dir.x
					robot1State.y += robot2Dir.y
				}
			}
		}

		fmt.Println(humanButtonsPressed)
		fmt.Println(len(humanButtonsPressed))
		output += len(humanButtonsPressed) * numericButtons
	}

	fmt.Println("Output Day 21 Part 1", output)
}

func day21_2(input string) {
	// lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	fmt.Println("Output Day 21 Part 2", output)
}
