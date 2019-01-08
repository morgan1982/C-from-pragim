

let Employees = [
    { name: "Max", experience: 5 },
    { name: "Tom", experience: 4 },
    { name: "Lisa", experience: 6 },
    { name: "Maria", experience: 3 },
    { name: "Michael", experience: 2 },
]


// the delegate
const Selector = (empl) => {
    if ( empl.experience >= 5) {
        return true
    }else {
        return false
    }
}




function DeclarePromotion(Employees, callback) {


    for (let employee of Employees) {

        if (callback(employee)) {
            console.log(`${employee} is promoted`);
        }else {
            console.log(`${employee} is fired`);
        }
    }
}

DeclarePromotion(Employees, Selector);
