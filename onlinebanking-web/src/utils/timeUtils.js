export const dateToExpireCard = date => {
    const dateSplited = new Date(date).toDateString().split(' ');

    return `${dateSplited[2]}/${dateSplited[3]}`
}