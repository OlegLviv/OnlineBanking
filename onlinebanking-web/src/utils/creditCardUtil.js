String.prototype.splice = function (start, delCount, newSubStr) {
    return this.slice(0, start) + newSubStr + this.slice(start + Math.abs(delCount));
};

export const normalizeCardNumber = number => {
    if (!number.length)
        return '';

    for (let i = 4; i < number.length; i += 5) {
        number = number.splice(i, 0, '-');
    }

    return number;
}