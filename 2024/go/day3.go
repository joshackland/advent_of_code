package main

import (
	"fmt"
	"regexp"
	"strconv"
)

func day3_1(input string) {
	output := 0

	re := regexp.MustCompile(`mul\((\d{1,3}),(\d{1,3})\)`)

	matches := re.FindAllStringSubmatch(input, -1)

	for _, match := range matches {
		num1, _ := strconv.Atoi(match[1])
		num2, _ := strconv.Atoi(match[2])
		output += num1 * num2
	}

	fmt.Println("Output Day 3 Part 1", output)
}

func day3_2(input string) {
	output := 0

	re := regexp.MustCompile(`mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)`)

	matches := re.FindAllStringSubmatch(input, -1)
	do := true
	for _, match := range matches {
		if match[0] == "do()" {
			do = true
			continue
		} else if match[0] == "don't()" {
			do = false
			continue
		} else if !do {
			continue
		}
		num1, _ := strconv.Atoi(match[1])
		num2, _ := strconv.Atoi(match[2])
		output += num1 * num2
	}

	fmt.Println("Output Day 3 Part 2", output)
}
