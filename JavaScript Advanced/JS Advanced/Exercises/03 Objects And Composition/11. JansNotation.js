function solve(inputArr) {
    let operands = [];
    let wasWrong = false;

    for (let i = 0; i < inputArr.length; i++) {
        if (typeof inputArr[i] === 'number') {
            operands.push(inputArr[i]);
        }
        else {
            let operator = inputArr[i];
            if (operands.length < 2) {
                console.log('Error: not enough operands!');
                wasWrong = true;
                break;
            }
            let operand2 = operands.pop();
            let operand1 = operands.pop();
            let result = applyOperation(operand1, operand2, operator);
            operands.push(result);
        }
    }

    if (operands.length > 1 && wasWrong === false) {
        console.log('Error: too many operands!');
    }
    else if (wasWrong === false) {
        console.log(operands[0]);
    }

    function applyOperation(operand1, operand2, operator) {
        const arithmeticOperator = {
            '+': () => operand1 + operand2,
            '-': () => operand1 - operand2,
            '/': () => operand1 / operand2,
            '*': () => operand1 * operand2,
        }

        return arithmeticOperator[operator]();
    }
}

solve([3,
    4,
    '+']
);
console.log();
solve([5,
    3,
    4,
    '*',
    '-']
);
console.log();
solve([7,
    33,
    8,
    '-']
);
console.log();
solve([15,
    '/']
);