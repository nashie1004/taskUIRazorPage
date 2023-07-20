﻿let count = 1;

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

let array = [];
let releaseName
let releaseDate
let shortDescription
let longDescription

if (window.location.href.toUpperCase() != baseURL.toUpperCase()) {

    document.querySelector("#submitBtn").onclick = function (e) {
        if (e.target.dataset.btntype === "createBtnSubmit") {
            getInputValues()
            submitFunction(submitHeaderURL) 
        } else if (e.target.dataset.btntype == "submitTableView") {
            tableViewSubmit()
        }
    }
}


function getInputValues() {
    // main header
    releaseName = document.querySelector("#ReleaseName").value;
    releaseDate = document.querySelector("#ReleaseDate").value;
    shortDescription = document.querySelector("#ShortDescription").value;
    longDescription = document.querySelector("#LongDescription").value;

    // details

    const length = document.querySelectorAll(".RTE-container").length
    let i = 0;

    document.querySelectorAll(".RTE-container").forEach(item => {

        const rte = document.getElementById(`default-${i++}`).ej2_instances[0]
        let content = rte.value;

        var images = rte.element.querySelectorAll("img");

        if (images.length == 0) {
            const detailType = parseInt(item.querySelector(".selectedTypeSelect").value)
            const detailName = item.querySelector(".detailNameInput").value
            const detailDescription = content.toString()

            array.push({
                type: detailType,
                name: detailName,
                description: detailDescription
            })
        } else {
            Array.from(images).map(async function (image) {
                var base64 = await getBase64FromImageUrl(image.src);
                content = content.replace(image.src, base64);

                const detailType = parseInt(item.querySelector(".selectedTypeSelect").value)
                const detailName = item.querySelector(".detailNameInput").value
                const detailDescription = content.toString()

                array.push({
                    type: detailType,
                    name: detailName,
                    description: detailDescription
                })
            })
        }

        function getBase64FromImageUrl(url) {
            return new Promise(function (resolve, reject) {
                var img = new Image();
                img.crossOrigin = "Anonymous";
                img.onload = function () {
                    var canvas = document.createElement("canvas");
                    canvas.width = img.width;
                    canvas.height = img.height;
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(img, 0, 0, img.width, img.height);
                    var base64 = canvas.toDataURL("image/png"); // You can also use "image/jpeg" for JPEG format
                    resolve(base64);
                };
                img.onerror = function () {
                    reject(new Error("Failed to load image"));
                };
                img.src = url;
            });
        }

        /*const dType = item.querySelector(".detailType")
        const dName = item.querySelector(".detailName")
        const dDescription = item.querySelector(".detailDescription")

        */
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
    console.log(dataBody)
    
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

// for edit page
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

