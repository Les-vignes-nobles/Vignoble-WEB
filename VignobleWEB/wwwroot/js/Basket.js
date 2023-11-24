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
                    
                    setCookie('CardItem', JSON.stringify(cartItems));

                    location.reload();
                }
            }
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

$(document).ready(function () {
    let productTotalJs = parseFloat('@productTotal');
    let tokenJs = '@token';

    $('select#dropdown-transports').change(function (event) {
        let selectedOption = $(this).find(":selected");
        let idTransport = parseInt(selectedOption.val());
        $.ajax({
            url: `http://82.165.237.163:5000/api/transport/${$(this).val()}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + tokenJs
            },
            dataType: 'json',
            contentType: 'application/json',
            success: function (transportData) {
                let transportPrice = parseFloat(transportData.price);

                if (!isNaN(transportPrice) && !isNaN(productTotalJs)) {
                    const totalPrice = productTotalJs + transportPrice;
                    $('#total-price').text(totalPrice.toFixed(2) + ' €');
                } else {
                    console.error('One of the values is NaN', { productTotalJs, transportPrice });
                }
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });

    $('button#btn-discount').click(function (event) {
        $.ajax({
            url: `http://82.165.237.163:5000/api/discount/${document.getElementById("discountValue").value}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + tokenJs
            },
            dataType: 'json',
            contentType: 'application/json',
            success: function (discountData) {
                let discountPrice = parseFloat(discountData.value);

                if (!isNaN(discountPrice) && !isNaN(productTotalJs)) {
                    const totalPrice = productTotalJs - discountPrice;
                    $('#total-price').text(totalPrice.toFixed(2) + ' €');
                } else {
                    console.error('One of the values is NaN', { productTotalJs, discountPrice });
                }
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });
});