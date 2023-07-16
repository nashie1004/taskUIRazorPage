// VALUES (TO ADD)

let count = 1;

document.querySelector("#addDetailBtn").onclick = () => {
    let div = document.createElement("tr");
    div.dataset.trid = count;

    div.innerHTML = `
        <th>${count++}</th>
        <td>
            <select class="detailType form-select" aria-label="Default select example">
                <option selected value="1">1 - Enhancements</option>
                <option value="2">2 - Bugs/Fixes</option>
            </select>
        </td>
        <td>
            <input value="Name here #${count - 1}" class="detailName 
            form-control form-control-sm" type="text" placeholder="Name..." aria-label="Name...S">
        </td>
        <td>
            <input value="Description here #${count - 1}" 
            class="detailDescription form-control form-control-sm" type="text" placeholder="Description..." aria-label="Description...S">
        </td>
        <td>
            <button type="button" class="deleteDetailBtnOnTable btn btn-danger">Delete</button>
        </td>
    `;

    document.querySelector("#addDetailsContainer").append(div); 

    document.querySelectorAll(".deleteDetailBtnOnTable").forEach(item => {
        item.addEventListener("click", (e) => {
            e.target.parentElement.parentElement.remove();
        })
    })
}

let array = [];
let releaseName
let releaseDate
let shortDescription
let longDescription

document.querySelector("#submitBtn").onclick = function (e) {
    getInputValues()

    if (e.target.dataset.btntype === "createBtnSubmit") {
        submitFunction("https://localhost:7249/SubmitHeader")
    } else {
        submitFunction("https://localhost:7249/SubmitHeader/editAPage")
    }
}

function getInputValues() {
    // main header
    releaseName = document.querySelector("#ReleaseName").value;
    releaseDate = document.querySelector("#ReleaseDate").value;
    shortDescription = document.querySelector("#ShortDescription").value;
    longDescription = document.querySelector("#LongDescription").value;

    // details
    document.querySelectorAll("[data-trid]").forEach(item => {
        const dType = item.querySelector(".detailType")
        const dName = item.querySelector(".detailName")
        const dDescription = item.querySelector(".detailDescription")

        array.push({
            // convert dType aka select to number
            type: parseInt(dType.options[dType.selectedIndex].value),
            name: dName.value,
            description: dDescription.value
        })
    })
}

async function submitFunction(URL) {
    const res = await fetch(URL, {
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
    window.location.href = "/";
}