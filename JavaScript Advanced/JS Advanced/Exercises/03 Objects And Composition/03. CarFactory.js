function solve(input) {
    let engines = {
        "Small engine": { power: 90, volume: 1800 },
        "Normal engine": { power: 120, volume: 2400 },
        "Monster engine": { power: 200, volume: 3500 },
    };
    let carriage = {
        Hatchback: {
            type: 'hatchback', color: input.color
        },
        Coupe: {
            type: 'coupe', color: input.color
        }
    }
    if (input.wheelsize % 2 == 0) {
        input.wheelsize--;
    }
    let wheels = [, , , ,];
    wheels.fill(input.wheelsize, 0, 4);

    let wantedEngine = Object.values(engines).filter(x => x.power >= input.power)[0];
    let wantedCarriage = Object.values(carriage).filter(x => x.type === input.carriage)[0];

    return {
        model: input.model,
        engine: wantedEngine,
        carriage: wantedCarriage,
        wheels: wheels
    };
}

console.log(solve({
    model: 'VW Golf II',
    power: 90,
    color: 'blue',
    carriage: 'hatchback',
    wheelsize: 14
}
));
console.log();
console.log(solve({
    model: 'Opel Vectra',
    power: 110,
    color: 'grey',
    carriage: 'coupe',
    wheelsize: 17
}
));