package main

import (
	"fmt"
	"sort"
	"strconv"
	"strings"
)

func day24_1(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n\n")
	output := 0

	initialValues := strings.Split(lines[0], "\n")
	gateConnections := strings.Split(lines[1], "\n")

	wireValues := make(map[string]int)
	gates := make(map[string][3]string)

	for _, value := range initialValues {
		parts := strings.Split(value, ": ")
		wire := parts[0]
		value, _ := strconv.Atoi(parts[1])
		wireValues[wire] = value
	}

	for _, value := range gateConnections {
		parts := strings.Split(value, "-> ")
		output := parts[1]
		gateParts := strings.Split(parts[0], " ")
		input1 := gateParts[0]
		input2 := gateParts[2]
		operator := gateParts[1]

		gates[output] = [3]string{input1, input2, operator}
	}

	var calculateWireValue func(string) int
	calculateWireValue = func(wire string) int {
		if value, exists := wireValues[wire]; exists {
			return value
		}

		input1, input2, operator := gates[wire][0], gates[wire][1], gates[wire][2]

		val1 := calculateWireValue(input1)
		val2 := calculateWireValue(input2)

		var result int

		if operator == "AND" {
			result = val1 & val2
		} else if operator == "OR" {
			result = val1 | val2
		} else if operator == "XOR" {
			result = val1 ^ val2
		}

		wireValues[wire] = result
		return result
	}

	outputBits := make(map[int]int)
	for wire := range gates {
		if strings.HasPrefix(wire, "z") {
			bitPosition, _ := strconv.Atoi(wire[1:])
			outputBits[bitPosition] = calculateWireValue(wire)
		}
	}

	for bit, value := range outputBits {
		output += value << bit
	}

	fmt.Println("Output Day 24 Part 1", output)
}

func day24_2(input string) {
	lines := strings.Split(strings.TrimSpace(input), "\n")
	wires := make(map[string]int)
	operations := [][]string{}
	wrong := make(map[string]bool)
	highestZ := "z00"

	process := func(op string, op1, op2 int) int {
		switch op {
		case "AND":
			return op1 & op2
		case "OR":
			return op1 | op2
		case "XOR":
			return op1 ^ op2
		default:
			return 0
		}
	}

	// Parse the input
	for _, line := range lines {
		if strings.Contains(line, ":") {
			parts := strings.Split(line, ": ")
			wire := parts[0]
			value, _ := strconv.Atoi(parts[1])
			wires[wire] = value
		} else if strings.Contains(line, "->") {
			parts := strings.Fields(line)
			op1, op, op2, res := parts[0], parts[1], parts[2], parts[4]
			operations = append(operations, []string{op1, op, op2, res})
			if strings.HasPrefix(res, "z") && strings.Compare(res, highestZ) > 0 {
				highestZ = res
			}
		}
	}

	// Determine wrong gates
	for _, opData := range operations {
		op1, op, op2, res := opData[0], opData[1], opData[2], opData[3]
		if strings.HasPrefix(res, "z") && op != "XOR" && res != highestZ {
			wrong[res] = true
		}
		if op == "XOR" && !strings.HasPrefix(op1, "x") && !strings.HasPrefix(op1, "y") &&
			!strings.HasPrefix(op2, "x") && !strings.HasPrefix(op2, "y") && !strings.HasPrefix(res, "x") &&
			!strings.HasPrefix(res, "y") && !strings.HasPrefix(res, "z") {
			wrong[res] = true
		}
		if op == "AND" && op1 != "x00" && op2 != "x00" {
			for _, subOpData := range operations {
				subOp1, subOp, subOp2, _ := subOpData[0], subOpData[1], subOpData[2], subOpData[3]
				if (res == subOp1 || res == subOp2) && subOp != "OR" {
					wrong[res] = true
				}
			}
		}
		if op == "XOR" {
			for _, subOpData := range operations {
				subOp1, subOp, subOp2, _ := subOpData[0], subOpData[1], subOpData[2], subOpData[3]
				if (res == subOp1 || res == subOp2) && subOp == "OR" {
					wrong[res] = true
				}
			}
		}
	}

	// Simulate the circuit
	for len(operations) > 0 {
		op1, op, op2, res := operations[0][0], operations[0][1], operations[0][2], operations[0][3]
		operations = operations[1:]
		if val1, ok1 := wires[op1]; ok1 {
			if val2, ok2 := wires[op2]; ok2 {
				wires[res] = process(op, val1, val2)
			} else {
				operations = append(operations, []string{op1, op, op2, res})
			}
		} else {
			operations = append(operations, []string{op1, op, op2, res})
		}
	}

	// Gather the output bits
	bits := []string{}
	for wire, value := range wires {
		if strings.HasPrefix(wire, "z") {
			bits = append(bits, fmt.Sprintf("%d", value))
		}
	}
	sort.Sort(sort.Reverse(sort.StringSlice(bits)))

	// Gather the wrong wires
	wrongWires := []string{}
	for wire := range wrong {
		wrongWires = append(wrongWires, wire)
	}
	sort.Strings(wrongWires)
	output := strings.Join(wrongWires, ",")

	fmt.Println("Output Day 24 Part 2", output)
}
