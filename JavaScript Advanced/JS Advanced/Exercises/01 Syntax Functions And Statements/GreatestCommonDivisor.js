function func(a, b) {
    while (b > 0) {
        let temp = b;
        b = a % b;
        a = temp;
    }

    return a;
}

console.log(func(15, 5));
console.log(func(2154, 458));