function setCookie(name, value) {
    var cookieValue = encodeURIComponent(value);



    document.cookie = name + '=' + cookieValue;
}

function deleteCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
}

//for (let y = 0; y < cartItems.length; y++) {
//    document.getElementById("btnSupp[" + y + "]").addEventListener("click", function () {

//        if (cartItems !== null) {

//            for (var i = 0; i < cartItems.length; i++) {
//                var item = cartItems[i];


//                if (item.IdProduct == document.getElementById("listProducts[" + y + "].Id").value) {
//                    //cartItems = cartItems.filter(function (element) {
//                    //    return element !== item.IdProduct;
//                    //});
//                    delete cartItems[i];
//                    setCookie('CardItem', cartItems);
//                }
//            }

//            if (cartItems.length === 0) {
//                console.log("vide");
//            }
//        }
//    });



//}



document.addEventListener("DOMContentLoaded", function () {
    // Récupérer tous les éléments avec la classe 'inputClass'
    var inputs = document.querySelectorAll('.inputClass');

    // Ajouter un écouteur d'événements à chaque input
    inputs.forEach(function (input) {
        input.addEventListener('change', function () {
            // Récupérer l'ID de l'input
            var idInput = input.id;

            var valLigne = idInput.split("_");

            for (var i = 0; i < cartItems.length; i++) {
                var item = cartItems[i];

                if (item.IdProduct == document.getElementById("listProducts[" + valLigne[1] + "].Id").value) {

                    item.Quantity = input.value;

                    if (item.Quantity == 0) {
                        delete cartItems[i];

                        cartItems = cartItems.filter(function (element) {
                            return element !== null;
                        });
                    }
                    
                    setCookie('CardItem', JSON.stringify(cartItems));

                    location.reload();
                }
            }
        });
    });

    var imgsDelete = document.querySelectorAll('.imgDeleteClass');

    imgsDelete.forEach(function (event) {
        event.addEventListener('click', function () {
            var valLigne = event.id.split("_");

            delete cartItems[valLigne[1]];

            cartItems = cartItems.filter(function (element) {
                return element !== null;
            });

            setCookie('CardItem', JSON.stringify(cartItems));

            location.reload();
        });
    });
});

var totalPriceWithOutTransports = 0;

for (var i = 0; i < cartItems.length; i++) {

    if (document.getElementById("totalPriceProduct_" + i).value.includes(",") === true) {
        var valueToAdd = document.getElementById("totalPriceProduct_" + i).value.replace(",", ".");
    }
    else {
        var valueToAdd = document.getElementById("totalPriceProduct_" + i).value;
    }

    totalPriceWithOutTransports = parseFloat(totalPriceWithOutTransports) + parseFloat(valueToAdd);
}

document.getElementById("totalPriceWithOutTransports").innerHTML = "Total : " + totalPriceWithOutTransports.toString().replace(".", ",") + " €";

