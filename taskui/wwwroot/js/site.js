let count = 1;

// urls
const createPageURL = "https://localhost:7249/Create";
const submitHeaderURL = "https://localhost:7249/SubmitHeader";
const baseURL = "https://localhost:7249/";
const submitTableViewURL = "https://localhost:7249/SubmitHeader/submitTableView";

// edit page not showing this so add now
document.querySelectorAll(".deleteDetailBtnOnTable").forEach(item => {
    item.addEventListener("click", (e) => {
        e.target.parentElement.parentElement.remove();
    })
})

if (window.location.href.toUpperCase() == createPageURL.toUpperCase()) {
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
}

let array = [];
let releaseName
let releaseDate
let shortDescription
let longDescription

document.querySelector("#submitBtn").onclick = function (e) {
    if (e.target.dataset.btntype === "createBtnSubmit") {
        getInputValues()
        submitFunction(submitHeaderURL) 
    } else if (e.target.dataset.btntype == "submitTableView") {
        tableViewSubmit()
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

    let dataBody = {
        ReleaseName: releaseName,
        ReleaseDate: releaseDate,
        ShortDescription: shortDescription,
        LongDescription: longDescription,
        Details: array
    }

    const res = await fetch(URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(dataBody)

    })

    try {

        const receivedData = await res.json()

        if (
            receivedData
        ) {
            console.log(receivedData)
            alert("Successfully created")
            window.location.href = baseURL;
        } else {
            alert("Error in creating")
        }

    } catch (err) {
        alert(err)
    }
}

function tableViewSubmit() {

    const submitArray = []
    document.querySelectorAll(".headerTr").forEach(tr => {
        let headerId = tr.querySelector(".trHeaderId").textContent
        let releaseNameTable = document.querySelector(`.trReleaseNameMain${headerId}`).value
        let releaseDateTable = document.querySelector(`.trReleaseDateMain${headerId}`).value
        let shortDescription = document.querySelector(`.trShortDescriptionMain${headerId}`).value
        let longDescription = document.querySelector(`.trLongDescriptionMain${headerId}`).value

        let detailId = tr.querySelector(".trDetailId").textContent
        let detailTypeTable = tr.querySelector(".trType").value
        let detailNameTable = tr.querySelector(".trName").value
        let detailDescriptionTable = tr.querySelector(".trDesc").value

        submitArray.push({
            HeaderId: headerId,
            ReleaseNameTable: releaseNameTable,
            ReleaseDateTable: releaseDateTable,
            ShortDescription: shortDescription,
            LongDescription: longDescription,
            DetailId: detailId,
            DetailTypeTable: Number(parseInt(detailTypeTable)),
            DetailNameTable: detailNameTable,
            DetailDescriptionTable: detailDescriptionTable
        })
    })

    submitTable(submitTableViewURL);
    async function submitTable(URL) {
        const res = await fetch(URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                BodyData: submitArray
            })
        })

        window.location.href = "/";
    }
    alert('submitted')
}

