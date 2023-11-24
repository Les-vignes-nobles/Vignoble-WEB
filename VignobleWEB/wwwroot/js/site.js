function getCookie(name) {
    // Recherchez le nom du cookie dans la chaîne document.cookie
    var matches = document.cookie.match(new RegExp('(?:^|; )' + name.replace(/([.$?*|{}()\[\]\\\/+^])/g, '\\$1') + '=([^;]*)'));

    // Si le cookie est trouvé, retournez sa valeur ; sinon, retournez null
    return matches ? decodeURIComponent(matches[1]) : null;
}

var valeurDuCookie = getCookie('CardItem');

// Faire quelque chose avec la valeur du cookie...
if (valeurDuCookie !== null) {
    if (valeurDuCookie.length > 0) {
        var cartItems = JSON.parse(valeurDuCookie);

        document.getElementById("nbProducts").innerHTML = cartItems.length;
    }
    else {
        var cartItems = null;

        document.getElementById("nbProducts").innerHTML = 0;
    }
}
 