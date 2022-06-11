function calc(fruit, grams, pricePerKg) {
    let weightInKg = grams / 1000;
    let moneyAll = weightInKg * pricePerKg;
    return `I need $${moneyAll.toFixed(2)} to buy ${weightInKg.toFixed(2)} kilograms ${fruit}.`;
}

console.log(solve('orange', 2500, 1.80));
console.log(solve('apple', 1563, 2.35));