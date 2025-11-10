export const generateId = (): number => {
    const min = 1;
    const max = 10000;
    return (Date.now() + Math.floor(Math.random() * (max - min + 1)) + min);
}
