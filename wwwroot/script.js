const baseUrl = window.location.origin;

function createTeam() {
    const form = document.getElementById("createTeamForm");
    const formData = new FormData(form);

    fetch(`${baseUrl}/createteam`, {
        method: "POST",
        body: formData
    })
    .then(async (response) => {
        if(!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
    })
    .catch((error) => {
        console.log("Error", error);
    })
}

function addPlayer() {
    const formData = getFormBody("addPlayerForm");

    fetch(`${baseUrl}/addplayer`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
          },
        body: JSON.stringify(formData),
    })
    .then(async (response) => {
        // If response is not ok (status 4xx or 5xx), handle the error
        if (!response.ok) {
            // Parse the error response (because it's JSON)
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();  // If successful, return the response as JSON
    })
    .then((data) => {
        console.log(data);
    })
    .catch((error) => {
        // Log the error message and optionally display it in the UI
        console.log("Error:", error.message);
    });

}

function deletePlayer() {
    const form = document.getElementById("deletePlayerForm");
    const formData = new FormData(form);

    fetch(`${baseUrl}/deleteplayer`, {
        method: "DELETE",
        body: formData
    })
    .then(async (response) => {
        if(!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
    })
    .catch((error) => {
        console.log("Error", error);
    })
}

function updatePlayer() {
    const formData = getFormBody("updatePlayerForm");

    fetch(`${baseUrl}/updateplayer/${formData.playerid}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
          },
        body: JSON.stringify(formData),
    })
    .then(async (response) => {
        if(!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
    })
    .catch((error) => {
        console.log("Error", error);
    })
}

function loadPlayers() {
    fetch(`${baseUrl}/getplayers`, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
          }
    })
    .then(async (response) => {
        if(!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
        displayPlayers(data);
    })
    .catch((error) => {
        console.log("Error", error);
    })


}

function findRanking() {
    const form = document.getElementById("findrankings");
    const formData = new FormData(form);

    fetch(`${baseUrl}/findranking`, {
        method: "POST",
        body: formData
    })
    .then(async (response) => {
        if(!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
        displayPlayers(data);
    })
    .catch((error) => {
        console.log("Error", error);
    })
}

function findOldest() {
    fetch(`${baseUrl}/findoldest`, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
          }
    })
    .then(async (response) => {
        if(!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || "An error occurred");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
        displayPlayers(data);
    })
    .catch((error) => {
        console.log("Error", error);
    })
}

function getFormBody(formid) {
    const form = document.getElementById(formid);
    const formData = {};

    for(let element of form.elements) {
        if(element.name && element.value) {
            formData[element.name] = element.value;
        }
    }
    return formData;
}

function displayPlayers(players) {
    const playersTable = document.getElementById("playersTable").getElementsByTagName("tbody")[0];
    playersTable.innerHTML = '';  // Clear the table before inserting new rows

    players.forEach(player => {
        const row = playersTable.insertRow();
        row.innerHTML = `
            <td>${player.name}</td>
            <td>${player.age}</td>
            <td>${player.position}</td>
            <td>${player.ranking}</td>
        `;
    });
}