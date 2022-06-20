function createSortedList() {
    let arr = [];

    return {
        add(element) {
            arr.push(element);
            this.size++;
            arr.sort((a, b) => a - b);
        },
        remove(index) {
            if (index < 0 || index >= arr.length) {
                throw new RangeError('Index out of range');
            }
            this.size--;
            arr.splice(index, 1);
        },
        get(index) {
            if (index < 0 || index >= arr.length) {
                throw new RangeError('Index out of range');
            }
            return arr[index];
        },
        get size() { return arr.length; }
    }
}

let list = createSortedList();
list.add(5);
list.add(6);
list.add(7);
console.log(list.get(1));
list.remove(1);
console.log(list.get(1));
console.log(list.size);