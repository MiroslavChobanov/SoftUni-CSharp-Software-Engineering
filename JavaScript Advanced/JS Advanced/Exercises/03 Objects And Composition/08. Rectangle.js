function rectangle(width, height, color) {
    color = color.charAt(0).toUpperCase() + color.slice(1);
    let rect = {
        width: width,
        height: height,
        color: color,
        calcArea: function() {
            return this.height * this.width;
        }
    }
    return rect;
}

let rect = rectangle(4, 5, 'red');
console.log(rect.width);
console.log(rect.height);
console.log(rect.color);
console.log(rect.calcArea());