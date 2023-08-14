// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//bikin looping ke animals, 2 fungsi :
//1. jika spesies 'cat' maka ambil lalu pindahkan ke variabel onlycat
//2. jika spesies 'fish' maka ganti class -> menjadi 'non mamalia'
//array of object
const animals = [
    { name: "dory", species: "fish", class: { name: "vertebrata" } },
    { name: "tom", species: "cat", class: { name: "mamalia" } },
    { name: "nemo", species: "fish", class: { name: "vertebrata" } },
    { name: "umar", species: "cat", class: { name: "mamalia" } },
    { name: "gary", species: "fish", class: { name: "human" } },
];

//console.log(animals[1].class.name);

let onlycat = [];
animals.forEach((animal) => {
    if (animal.species === "cat") {
        onlycat.push(animal);
    }

    if (animal.species === "fish") {
        animal.class.name = "non mamalia";
    }
});

console.log(onlycat);
console.log(animals);