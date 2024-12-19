package main

import (
	"fmt"
	"strings"
)

func day19_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n\n")

	towels := make(map[string]bool)
	for _, towel := range strings.Split(lines[0], ", ") {
		towels[towel] = true
	}

	patterns := []string{}
	patterns = append(patterns, strings.Split(lines[1], "\n")...)

	output := 0

	for _, pattern := range patterns {
		matches := make([]bool, len(pattern)+1)
		matches[0] = true

		for i := 1; i <= len(pattern); i++ {
			for j := 0; j < i; j++ {
				if matches[j] && towels[pattern[j:i]] {
					matches[i] = true
					break
				}
			}
		}

		if matches[len(pattern)] {
			output++
		}
	}

	fmt.Println("Output Day 19 Part 1", output)
}

func day19_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n\n")

	towels := make(map[string]bool)
	for _, towel := range strings.Split(lines[0], ", ") {
		towels[towel] = true
	}

	patterns := []string{}
	patterns = append(patterns, strings.Split(lines[1], "\n")...)

	output := 0

	for _, pattern := range patterns {
		matches := make([]int, len(pattern)+1)
		matches[0] = 1

		for i := 1; i <= len(pattern); i++ {
			for j := 0; j < i; j++ {
				if matches[j] > 0 && towels[pattern[j:i]] {
					matches[i] += matches[j]
				}
			}
		}

		output += matches[len(pattern)]
	}

	fmt.Println("Output Day 19 Part 2", output)
}
