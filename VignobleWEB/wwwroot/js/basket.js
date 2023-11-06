transportSelect = document.getElementById("idTransport");

// URL de l'API
const urlSiteApi = 'http://82.165.237.163:5000/api/transport/';


function appelAPI(param1) {
    // URL de base de l'API
    const apiUrl = 'https://exemple.com/api/endpoint';

    // Construire l'URL en incluant les paramètres
    const urlAvecParametres = `${apiUrl}${param1}`;

    // Effectuer la requête GET à l'URL avec les paramètres
    return fetch(urlAvecParametres)
        .then(response => {
            if (!response.ok) {
                throw new Error('Réponse de l\'API non valide');
            }
            return response.json();
        })
        .then(data => {
            return data; // Retournez les données de l'API
        })
        .catch(error => {
            throw new Error('Une erreur s\'est produite :', error);
        });
}


transportSelect.addEventListener("change", function() {
    transportChoisi = transportSelect.selectedIndex;
    valTransport = transportSelect.options[transportChoisi].value;

    appelAPI(valTransport).then(data => {

        console.log(data);
    })
    .catch(error => {
        console.error(error);
    });

});