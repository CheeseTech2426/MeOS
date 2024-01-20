using MeOS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeOS.MeScript {
    internal class VariableHandler {
        private string variableA = "";
        private string variableB = "";
        private string variableC = "";
        private string variableD = "";
        private string variableE = "";
        private string variableF = "";
        private string variableG = "";
        private string variableH = "";
        private string variableI = "";
        private string variableJ = "";
        private string variableK = "";
        private string variableL = "";
        private string variableM = "";
        private string variableN = "";
        private string variableO = "";
        private string variableP = "";
        private string variableQ = "";
        private string variableR = "";
        private string variableS = "";
        private string variableT = "";
        private string variableU = "";
        private string variableV = "";
        private string variableW = "";
        private string variableX = "";
        private string variableY = "";
        private string variableZ = "";
        private string userInput = "";

        public string GetUserInput() {
            return userInput;
        }

        public void SetUserInput(string userInput, string variable){
            SetVariable(variable, userInput);
        }

        public string GetVariable(string variableName) {
            switch (variableName) {
                case "a":
                    return variableA;
                case "b":
                    return variableB;
                case "c":
                    return variableC;
                case "d":
                    return variableD;
                case "e":
                    return variableE;
                case "f":
                    return variableF;
                case "g":
                    return variableG;
                case "h":
                    return variableH;
                case "i":
                    return variableI;
                case "j":
                    return variableJ;
                case "k":
                    return variableK;
                case "l":
                    return variableL;
                case "m":
                    return variableM;
                case "n":
                    return variableN;
                case "o":
                    return variableO;
                case "p":
                    return variableP;
                case "q":
                    return variableQ;
                case "r":
                    return variableR;
                case "s":
                    return variableS;
                case "t":
                    return variableT;
                case "u":
                    return variableU;
                case "v":
                    return variableV;
                case "w":
                    return variableW;
                case "x":
                    return variableX;
                case "y":
                    return variableY;
                case "z":
                    return variableZ;
                case "temp":
                    return Interpreter.temp;
                default:
                    Console.Beep();
                    Console.WriteLine($"MeScript: Invalid variable name '{variableName}'.");
                    return null; // Or throw an exception if needed
            }
        }

        public void SetVariable(string variableToSet, string value) {

            if (variableToSet == "a") {
                variableA = value;
            } else if (variableToSet == "b") {
                variableB = value;
            } else if (variableToSet == "c") {
                variableC = value;
            } else if (variableToSet == "d") {
                variableD = value;
            } else if (variableToSet == "e") {
                variableE = value;
            } else if (variableToSet == "f") {
                variableF = value;
            } else if (variableToSet == "g") {
                variableG = value;
            } else if (variableToSet == "h") {
                variableH = value;
            } else if (variableToSet == "i") {
                variableI = value;
            } else if (variableToSet == "j") {
                variableJ = value;
            } else if (variableToSet == "k") {
                variableK = value;
            } else if (variableToSet == "l") {
                variableL = value;
            } else if (variableToSet == "m") {
                variableM = value;
            } else if (variableToSet == "n") {
                variableN = value;
            } else if (variableToSet == "o") {
                variableO = value;
            } else if (variableToSet == "p") {
                variableP = value;
            } else if (variableToSet == "q") {
                variableQ = value;
            } else if (variableToSet == "r") {
                variableR = value;
            } else if (variableToSet == "s") {
                variableS = value;
            } else if (variableToSet == "t") {
                variableT = value;
            } else if (variableToSet == "u") {
                variableU = value;
            } else if (variableToSet == "v") {
                variableV = value;
            } else if (variableToSet == "w") {
                variableW = value;
            } else if (variableToSet == "x") {
                variableX = value;
            } else if (variableToSet == "y") {
                variableY = value;
            } else if (variableToSet == "z") {
                variableZ = value;
            } else if (variableToSet == "temp") {
                Interpreter.temp = value;
            } else {
                Console.WriteLine($"MeScript: Variable '{variableToSet}' not found.");
            }

        }

    }
}
