// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//function changeText() {
//    document.getElementById("p1").innerHTML = "Hello World!";
//}

//function changeColor() {
//    document.getElementById("p2").style.backgroundColor = "green";
//}

//function changeTextColor() {
//    document.getElementById("p3").style.color = "red";
//}

//------------------------------------------------------------------
////asynchronous javascript
//$.ajax({
//    url: "https://swapi.dev/api/people/"
//}).done((result) => {
//    let temp = "";
//    $.each(result.results, (key, val) => {
//        temp += "<li>" + val.name + "</li>";
//    })
//    $("#listSW").html(temp);
//});
//------------------------------------------------------------------

//$.ajax({
//    url: "https://swapi.dev/api/people/"
//}).done((result) => {
//    let temp = "";
//    $.each(result.results, (index, val) => {
//        temp += "<tr>";
//        temp += "<td>" + (index + 1) + "</td>";
//        temp += "<td>" + val.name + "</td>";
//        temp += "<td>" + val.height + "</td>";
//        temp += "<td>" + val.gender + "</td>";
//        temp += "<td>" + val.birth_year + "</td>";
//        temp += "</tr>";
//    });
//    $("#tbodySW").html(temp);

//    console.log(result);
//});


//------------------------------------------------------------------
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    let temp = "";
    //$.each(result.results, (indeks, val) => {
    //    temp += `
    //            <tr>
    //                <td>${indeks + 1}</td>
    //                <td>${val.name}</td>
    //                <td><button onclick="detailPokemon('${val.url}')" data-bs-toggle="modal" data-bs-target="#exampleModal" class="btn btn-primary">Detail</button></td>
    //            </tr>
    //        `;
    //})
    $("#tbodySW").html(temp);
    $.each(result.results, (indeks, val) => {
        temp += `
                    <div class="pokemon-card">
                        <h4 class="pokemon-name">${val.name}</h4>
                        <button onclick="detailPokemon('${val.url}')" data-bs-toggle="modal" data-bs-target="#exampleModal" class="btn btn-primary">Detail</button>
                    </div>
                `;
    })
    $("#pokemonContainer").html(temp);
});


function detailPokemon(stringURL) {
    $.ajax({
        url: stringURL,
        success: (result) => {


            $('.modal-title').html(result.name);
            $('#gambar').attr('src', result.sprites.other['official-artwork'].front_default);
            $('#name').text(result.name),
            $('#weight').text(result.weight),
            $('#height').text(result.height),
            $('#types').text(result.types);

            const abilitiesList = result.abilities.map((ability) => {
                return `<li>${ability.ability.name}</li>`;
            });
            $('#abilities').html(abilitiesList.join(''));

          
            const baseExperiencePercentage = Math.min((result.base_experience / 255) * 100, 100);
            $('#experience-progress').attr('style', `width: ${baseExperiencePercentage}%`);
            $('#experience-progress .progress-text').text(`${baseExperiencePercentage.toFixed(2)}%`);

            const hpPercentage = Math.min((result.stats[0].base_stat / 255) * 100, 100);
            $('#hp-progress').attr('style', `width: ${hpPercentage}%`);
            $('#hp-progress .progress-text').text(`${hpPercentage.toFixed(2)}%`);

            const attackPercentage = Math.min((result.stats[1].base_stat / 255) * 100, 100);
            $('#attack-progress').attr('style', `width: ${attackPercentage}%`);
            $('#attack-progress .progress-text').text(`${attackPercentage.toFixed(2)}%`);

            const defensePercentage = Math.min((result.stats[2].base_stat / 255) * 100, 100);
            $('#defense-progress').attr('style', `width: ${defensePercentage}%`);
            $('#defense-progress .progress-text').text(`${defensePercentage.toFixed(2)}%`);

            const types = result.types.slice(0, 2).map((typeData) => typeData.type.name);
            document.getElementById("types").innerText = types.join(" ");

            const typesList = result.types.map((type) => {
                return `<span class="badge type-badge ${type.type.name}">${type.type.name}</span>`;
            });
            $('#types').html(typesList.join(' '));
        }
    });
}

// Fungsi untuk mendapatkan URL sprite gambar Pokemon
function getPokemonSprite(pokemonURL) {
    const pokemonID = pokemonURL.split("/").slice(-2, -1)[0];
    return `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/${pokemonID}.png`;
}

$(document).ready(() => {
    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon/"
    }).done((result) => {
        let temp = "";
        $.each(result.results, (indeks, val) => {
            temp += `
                        <div class="pokemon-card">
                            <img class="pokemon-image" src="${getPokemonSprite(val.url)}" alt="${val.name}">
                            <p class="pokemon-name">${val.name}</p>
                            <button onclick="detailPokemon('${val.url}')" data-bs-toggle="modal" data-bs-target="#exampleModal" class="btn btn-primary">Detail</button>
                        </div>
                    `;
        });
        $("#pokemonContainer").html(temp);
    });
});

//--------------------------------------------------Employee
$(document).ready(function () {
    let table = new DataTable('#tabelEmp', {
        dom: 'Blfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ],
       
        ajax: {
            url: "https://localhost:7280/api/employees",
            dataSrc: "data",
            dataType: "JSON"
        },

        columns: [
            /* { data: "guid" },*/
            {
                data: null,
                render: function (data, type, row, num) {
                    return num.row + 1;
                }
            },
            { data: "nik" },
            {
                data: null,
                render: function (data, type, row) {
                    return row.firstName + ' ' + row.lastName;
                }
            },
            /*{ data: "birth_Date" },*/
            {
                data: "birthDate",
                render: function (data) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },
            {
                data: 'gender',
                render: function (data, type, row) {
                    return data === 0 ? "Female" : "Male";
                }
            },
            /*{ data: "hiring_Date" },*/
            {
                data: "hiringDate",
                render: function (data) {
                    return moment(data).format('DD-MM-YYYY');
                }
            },
            { data: "email" },
            { data: "phoneNumber" },

            {
                data: '',
                render: function (data, type, row) {
                    return `<button onclick="updateEmp('${row.guid}')" 
                    data-bs-toggle="modal" data-bs-target="#modalUpdateEmployee" 
                    class="btn btn-primary"><i class="fa-regular fa-pen-to-square"></i></button>

                    <button onclick="deleteEmp('${row.guid}')" 
                    data-bs-toggle="modal" data-bs-target="#modalSW" 
                    class="btn btn-primary"><i class="fa-solid fa-trash"></i></button>`;
                }
            }
        ]
    }).fail((error)=>{
        console.log(error)
    });
});

$(document).ready(() => {
    $.ajax({
        url: "https://localhost:7280/api/employees"
    }).done((result) => {
        let female = 0;
        let male = 0;

        result.data.forEach(employee => {
            if (employee.gender === 0) { female++ }
            if (employee.gender === 1) { male++ }
        });

        var xValues = ["Female", "Male"];
        var yValues = [female, male];
        var barColors = [
            "#b91d47",
            "#00aba9",
        ];

        new Chart("myChart", {
            type: "pie",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                title: {
                    display: true,
                    text: "Gender of Employees"
                }
            }
        });
    });
});

function Insert() {
    var obj = new Object();
    obj.First_Name = $("#firstName").val();
    obj.Last_Name = $("#lastName").val();
    obj.Birth_Date = $("#birthDate").val();
    /*obj.Gender = $("#gender").val() === "female" ? 0 : 1;*/
    obj.Gender = parseInt($("#gender").val());
    obj.Hiring_Date = $("#hiringDate").val();
    obj.Email = $("#email").val();
    obj.Phone_Number = $("#phone").val();

    $.ajax({
        url: "https://localhost:7280/api/employees",
        type: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json",
    }).done((result) => {
        console.log(result);
        alert("Data inserted successfully!");
    }).fail((xhr, status, error) => {
        console.log(xhr);
        alert("Failed to insert data. Error: " + xhr.responseText);
    });
}


$(document).ready(() => {
    $.ajax({
        url: "https://localhost:7280/api/univerities"
    }).done((result) => {
        let unila = 0;
        let ub = 0;
        let itb = 0;
        let unj = 0;
        let others = 0;

        result.data.forEach(university => {
            if (university.code === 'UNILA') { unila++}
            else if (university.code === 'UB') { ub++ }
            else if (university.code === 'ITB') { itb++ }
            else if (university.code === 'UNJ') { unj++ }
            else { others++ }
        })
        const xValues = ["UNILA", "UB", "ITB", "UNJ", "OTHERS"];
        const yValues = [unila, ub, itb, unj, others];
        const barColors = ["red", "green", "blue", "orange", "brown"];

        new Chart("myBar", {
            type: "bar",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: "University of Employees"
                }
            }
        });
    });
});

function deleteEmp(guid) {
    if (confirm("Apakah Anda yakin ingin menghapus data ini?")) {
        $.ajax({
            url: "https://localhost:7280/api/employees/?guid=" + guid,
            type: 'DELETE',
            success: function (response) {
                console.log('Data berhasil dihapus:', response)
                location.reload()
            },
            error: function (xhr, status, error) {

                console.log('Terjadi kesalahan:', error);
            }
        });
    }
}


$(document).ready(function () {
    $('#razoremp').DataTable({
        dom: 'Blfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ],
    });
});

