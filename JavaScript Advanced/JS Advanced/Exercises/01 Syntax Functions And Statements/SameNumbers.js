function solve(num) {
    let numString = String(num);
    let isSame = true;

    let sum = numString.length > 0 ? Number(numString[0]) : 0;

    for (let index = 0; index < numString.length - 1; index++) {
        const element = Number(numString[index]);
        const nextElement = Number(numString[index + 1]);
        if(element !== nextElement){
            isSame = false;
        }
        sum = sum + nextElement;
    }

    console.log(isSame);
    console.log(sum);
}

solve(1234);