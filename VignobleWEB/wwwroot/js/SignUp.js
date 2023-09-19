function validate() {
    var a = document.getElementById("password").value;
    var b = document.getElementById("passwordVerified").value;

    if (a != b) {
        alert("Les mots de passe ne correspondent pas.");
    }
}