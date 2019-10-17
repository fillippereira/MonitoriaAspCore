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


$('#carregar-ficha').click(function () {
    let dados_editable = $('#editable-irregularidades-ficha').text();

    if (dados_editable != '') {
        let dados_linha = dados_editable.split(';');
        dados_linha.forEach(function (e) {
            let dados_col = e.split('. ');
            if (dados_col.length > 1) {
                let id_number = dados_col[0];
                let valor_ocorrencia = dados_col[1];
                let ids_nivel_ocorrencia = id_number.split('.');
                let id_text = 'ocorrencia_'+id_number.replace(/./g, '_');
                let class_element = '';

                for (let index = 0; index < ids_nivel_ocorrencia.length-1; index++) {
                    class_element += ids_nivel_ocorrencia[index] + '_';
                }
                if (class_element.length > 0) {
                    class_element = 'ocorrencia_'+class_element.substring(0, class_element.length - 1);
                }

                $('#editable-irregularidades-ficha').addClass('d-none');
                
                if($('#irregularidades-ficha div:eq(0) #bloco_' + ids_nivel_ocorrencia.length).length == 0){
                    $('#irregularidades-ficha div:eq(0)').append(`
                            <div class="col" id="bloco_${ids_nivel_ocorrencia.length}"></div>
                     `)
                }

                let elemento_oculto = 'd-none';
                if (ids_nivel_ocorrencia.length == 1) {
                    elemento_oculto = '';
                }

                $('#irregularidades-ficha div:eq(0) #bloco_' + ids_nivel_ocorrencia.length).append(`
                    <div id="${id_text}" class="input-group my-2 ocorrencia_ficha ${elemento_oculto} ${class_element}">
                        <input class="form-control form-control-sm" name="texto_${id_text}" value="${valor_ocorrencia}">
                        <input class="form-control form-control-sm" name="valor_${id_text}" value="" placeholder="Peso da Ocorrência">
                        <select class="form-control form-control-sm" name="tipo_ocorrencia">
                            <option value="">[Tipo Ocorrência]</option>
                            <option value="marcacao">Marcação</option>
                            <option value="multimarcacao">Multi Marcação</option>
                            <option value="texto">Texto</option>
                        </select>
                    </div>
                `)
                
            }
            else {
               console.log('Dados inserido em formato diferente.');
               return false;
            }
        })
    }
})

$('#irregularidades-ficha').on('click','.ocorrencia_ficha',function () {
    let idAtual = this.id;
    let blocoAtual = $(this).parent().attr('id');
    blocoAtual = blocoAtual.replace('bloco_','');
    let proxBloco = 'bloco_' + (parseInt(blocoAtual) + 1);

    $(this).parent().parent().find('#' + proxBloco + ' .ocorrencia_ativa').addClass('d-none').removeClass('ocorrencia_ativa');
    $(this).parent().parent().find('#' + proxBloco + ' .' + idAtual).removeClass('d-none').addClass('ocorrencia_ativa');
})