function solve(data) {
    let heroesArray = [];

    data.forEach(element => {
        let [name, level, items] = element.split(" / ");
        level = Number(level);

        if (items) {
            items = items.split(", ");
        }
        else {
            items = [];
        }

        let hero = { name, level, items };

        heroesArray.push(hero);
    });
    let output = JSON.stringify(heroesArray);

    return output;
}

console.log(solve(['Jake / 1000 / Gauss, HolidayGrenade']
));