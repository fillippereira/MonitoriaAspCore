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


$("input[type=file]").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});


function ToggleCustomItem(button) {
    var card = $(button).parent().parent().parent().parent();
    var divToggle = $(card).children(".collapse");
  
    if ($(divToggle).hasClass("show")){
        $(divToggle).removeClass("show");
        $(button).html('<i class="fa fa-angle-right fa-lg"></i>');
       
    } else
    {
        $(divToggle).addClass("show");
        $(button).html('<i class="fa fa-angle-down fa-lg"></i>');
    }
   
    
}

function AddCustomItem() {
    var divNoItem = $("#DivCustomItens").children();
    if ($(divNoItem).attr("id") == "DivNoItem") {
        $(divNoItem).remove();

    } 
    $("#DivCustomItens").append(`
     <div class="col-12 mt-1"> 
        <div class="card">
            <div class="card-header bg-white" >
                <div class="row">
                    <div class="col-1 text-left">
                        <button class="btn btn-sm" type="button" onclick="ToggleCustomItem(this)"><i class="fa fa-angle-right fa-lg"></i></button>
                    </div>
                    <div class="col-9 pl-0">
                        <input type="text" class="form-control form-control-sm" />
                    </div>
                    <div class="col-2 text-right">
                        <button class="btn btn-sm btn-light" type="button" onclick="RemoveCustomItem(this)"><i class="fa fa-trash text-danger"></i></button>
                    </div>
                </div>
            </div>
                                     
            <div class="card-body collapse">
                <div class="row">
                    <div class="col-3">
                        <label>Formato</label>
                        <select class="form-control form-control-sm">
                            <option>Texto</option>
                            <option>Inteiro</option>
                            <option>Double</option>
                            <option>Data</option>
                            <option>Data e hora</option>
                        </select>
                    </div>

                    <div class="col-3">
                        <label>Caracteres</label>
                        <input type="number" class="form-control form-control-sm" />
                    </div>
                    <div class="col-3">
                        <label>Colunas</label>
                        <select class="form-control form-control-sm">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                            <option>5</option>
                            <option>6</option>
                            <option>7</option>
                            <option>8</option>
                            <option>9</option>
                            <option>10</option>
                            <option>11</option>
                            <option>12</option>
                        </select>
                    </div>
                    <div class="col-3">
                        <label>Obrigatório</label>
                        <select class="form-control form-control-sm">
                            <option>Sim</option>
                            <option>Não</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </div>`);
}

function RemoveCustomItem(button) {
    var customItem = $(button).parent().parent().parent().parent().parent();

    $(customItem).remove();
   
    if ($("#DivCustomItens").is(":empty")) {
        console.log("asdasda");
        $("#DivCustomItens").append(`<div id="DivNoItem" class="col-12 text-center mt-5">
            <label>Nenhum item foi adicionado</label>
        </div>`);
    }
}