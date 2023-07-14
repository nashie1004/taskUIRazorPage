// VALUES (TO ADD)

let count = 1;

document.querySelector("#addDetailBtn").onclick = () => {
    document.querySelector("#addDetailsContainer").innerHTML += `
       <tr>
            <th>${count++}</th>
            <td>
                <input class="detailType form-control form-control-sm" type="number" placeholder="Type..." aria-label="Type...S">
            </td>
            <td>
                <input class="detailName form-control form-control-sm" type="text" placeholder="Name..." aria-label="Name...S">
            </td>
            <td>
                <input class="detailDescription form-control form-control-sm" type="text" placeholder="Description..." aria-label="Description...S">
            </td>
        </tr>
    `
}

let array = [];
let releaseName
let releaseDate
let shortDescription
let longDescription

document.querySelector("#submitBtn").onclick = function () {
    // main header
    releaseName = document.querySelector("#ReleaseName").value;
    releaseDate = document.querySelector("#ReleaseDate").value;
    shortDescription = document.querySelector("#ShortDescription").value;
    longDescription = document.querySelector("#LongDescription").value;
    console.log(longDescription)
    // details
    let checkArray = []
    document.querySelectorAll(".detailType").forEach((dType, i) => {

        document.querySelectorAll(".detailName").forEach((dName) => {

            document.querySelectorAll(".detailDescription").forEach(dDescription => {

                if (!checkArray.includes(dDescription)) {

                    checkArray.push(dDescription)

                    array.push({
                        type: dType.value,
                        name: dName.value,
                        description: dDescription.value
                    })
                } 
            })
        })
    })

    submitFunction()
}

async function submitFunction() {
    const res = await fetch("https://localhost:7249/SubmitHeader", {

        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            ReleaseName: releaseName,
            ReleaseDate: releaseDate,
            ShortDescription: shortDescription,
            LongDescription: longDescription,
            Details: array
        })
        
    })
    const data = await res.json();
    console.log(data);
    window.location.href = "/";
}