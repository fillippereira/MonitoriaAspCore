// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#DivAlterarFoto").on("click", function () {

    $("input[type=file]").click();
})


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#FotoPerfil').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("input[type=file]").change(function () {
    readURL(this);
});


function Logout() {

    $("#LogoutForm").submit();
}


$('.multiSelect').multiselect();