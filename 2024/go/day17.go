package main

import (
	"fmt"
	"math"
	"slices"
	"strconv"
	"strings"
)

func day17_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	output := []string{}

	var A, B, C int

	fmt.Sscanf(lines[0], "Register A: %d", &A)
	fmt.Sscanf(lines[1], "Register B: %d", &B)
	fmt.Sscanf(lines[2], "Register C: %d", &C)

	programStr := strings.TrimPrefix(lines[4], "Program: ")
	programParts := strings.Split(programStr, ",")
	program := make([]int, len(programParts))

	for i, part := range programParts {
		program[i], _ = strconv.Atoi(part)
	}

	ip := 0

	getComboValue := func(operand int) int {
		if operand == 4 {
			return A
		} else if operand == 5 {
			return B
		} else if operand == 6 {
			return C
		}
		return operand
	}

	for ip < len(program) {
		opcode := program[ip]
		operand := program[ip+1]

		if opcode == 0 {
			A = A / int(math.Pow(2, float64(getComboValue(operand))))
		} else if opcode == 1 {
			B = B ^ operand
		} else if opcode == 2 {
			B = getComboValue(operand) % 8
		} else if opcode == 3 {
			if A != 0 {
				ip = operand
				continue
			}
		} else if opcode == 4 {
			B = B ^ C
		} else if opcode == 5 {
			output = append(output, strconv.Itoa(getComboValue(operand)%8))
		} else if opcode == 6 {
			B = A / int(math.Pow(2, float64(getComboValue(operand))))
		} else if opcode == 7 {
			C = A / int(math.Pow(2, float64(getComboValue(operand))))
		}

		ip += 2
	}

	fmt.Println("Output Day 17 Part 1", strings.Join(output, ","))
}

func day17_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")

	// Parse registers and program
	A, B, C, program := parseRegisters(lines)

	if true { // Part 2 logic
		A = 0
		for pos := len(program) - 1; pos >= 0; pos-- {
			A <<= 3 // Shift left by 3 bits
			for !slices.Equal(runProgram(A, B, C, program), program[pos:]) {
				A++
			}
		}

		fmt.Println("Output Day 17 Part 2", A)
	}
}

// Parse the input to extract registers and the program
func parseRegisters(lines []string) (int, int, int, []int) {
	A, B, C := 0, 0, 0
	program := []int{}

	for _, line := range lines {
		if strings.HasPrefix(line, "Register A:") {
			A, _ = strconv.Atoi(strings.TrimSpace(line[12:]))
		} else if strings.HasPrefix(line, "Register B:") {
			B, _ = strconv.Atoi(strings.TrimSpace(line[12:]))
		} else if strings.HasPrefix(line, "Register C:") {
			C, _ = strconv.Atoi(strings.TrimSpace(line[12:]))
		} else if strings.HasPrefix(line, "Program:") {
			programStr := strings.TrimSpace(line[9:])
			programParts := strings.Split(programStr, ",")
			for _, part := range programParts {
				val, _ := strconv.Atoi(part)
				program = append(program, val)
			}
		}
	}

	return A, B, C, program
}

// Simulate the program execution with given registers and program
func runProgram(A, B, C int, program []int) []int {
	output := []int{}
	ip := 0

	// Run through each instruction
	for ip < len(program) {
		opcode := program[ip]
		operand := program[ip+1]

		// Resolve combo operands
		value := operand
		if operand == 4 {
			value = A
		} else if operand == 5 {
			value = B
		} else if operand == 6 {
			value = C
		}

		// Execute instructions
		switch opcode {
		case 0: // adv - divide A by 2^value
			A >>= value
		case 1: // bxl - XOR B with literal
			B ^= operand
		case 2: // bst - set B to value mod 8
			B = value % 8
		case 3: // jnz - jump if A is not zero
			if A != 0 {
				ip = operand - 2
			}
		case 4: // bxc - XOR B with C
			B ^= C
		case 5: // out - output value mod 8
			output = append(output, value%8)
		case 6: // bdv - divide A by 2^value, store in B
			B = A >> value
		case 7: // cdv - divide A by 2^value, store in C
			C = A >> value
		}

		ip += 2
	}

	return output
}
