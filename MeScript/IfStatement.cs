using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.MeScript {
    internal class IfStatement {

        public int Process(string[] tokens, int currentIndex, string[] allLines) {
            if (tokens.Length >= 4 && EvaluateCondition(tokens[1], tokens[2], tokens[3])) {
                currentIndex++; // Move to the next line
                int endifIndex = FindEndIf(currentIndex, allLines);
                while (currentIndex < allLines.Length && !allLines[currentIndex].StartsWith("endif")) {
                    ProcessLine(allLines[currentIndex]);
                    currentIndex++;
                }
            } else {
                // Skip lines until the corresponding endif
                int nestedIfCount = 0;
                while (currentIndex < allLines.Length) {
                    if (allLines[currentIndex].StartsWith("if")) {
                        nestedIfCount++;
                    } else if (allLines[currentIndex].StartsWith("endif")) {
                        if (nestedIfCount == 0) {
                            break; // Found the corresponding endif
                        } else {
                            nestedIfCount--;
                        }
                    }
                    currentIndex++;
                }
            }

            return currentIndex;
        }

        private void ProcessLine(string line) {
            Console.WriteLine("Line: " + line);
        }

        private bool EvaluateCondition(string left, string op, string right) {
            // Assuming variables are accessed using "get" command, e.g., "get x"
            if (left.StartsWith("get") && right.StartsWith("get")) {
                char leftVariable = left.Length > 4 ? left[4] : '\0';
                char rightVariable = right.Length > 4 ? right[4] : '\0';

                // Perform the comparison based on the variables
                switch (op) {
                    case "==":
                        return leftVariable == rightVariable;
                    case "!=":
                        return leftVariable != rightVariable;
                    default:
                        return false;
                }
            }

            return false;
        }

        private int FindEndIf(int startIndex, string[] allLines) {
            int nestedIfCount = 0;
            int index = startIndex;

            while (index < allLines.Length) {
                if (allLines[index].StartsWith("if")) {
                    nestedIfCount++;
                } else if (allLines[index].StartsWith("endif")) {
                    if (nestedIfCount == 0) {
                        return index; // Found the corresponding endif
                    } else {
                        nestedIfCount--;
                    }
                }
                index++;
            }

            // Return -1 if corresponding endif is not found
            return -1;
        }
    }
}
