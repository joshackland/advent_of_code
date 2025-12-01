package main

import (
	"fmt"
	"strings"
)

func day25_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n\n")
	output := 0

	locks := [][5]int{}
	keys := [][5]int{}

	for _, line := range lines {
		group := strings.Split(strings.TrimSpace(line), "\n")
		key := false
		vals := [5]int{}
		if group[0][0] == '.' {
			key = true
		}
		for i := 1; i < 6; i++ {
			for j := 0; j < 5; j++ {
				if group[i][j] == '#' {
					vals[j]++
				}
			}
		}

		if key {
			keys = append(keys, vals)
		} else {
			locks = append(locks, vals)
		}
	}

	for _, lock := range locks {
		for _, key := range keys {
			fits := true
			for col := range lock {
				if lock[col]+key[col] > 5 {
					fits = false
					break
				}
			}
			if fits {
				output++
			}
		}
	}

	fmt.Println("Output Day 25 Part 1", output)
}

func day25_2(input string) {
	// lines := strings.Split(strings.TrimSpace(input), "\n")
	output := 0

	fmt.Println("Output Day 25 Part 2", output)
}
