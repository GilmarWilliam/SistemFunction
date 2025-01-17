﻿
$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #CPF').val(obj.CPF);
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        var form = $(this);
        // Criação da lista de beneficiários a partir do grid
        var beneficiarios = [];
        $('#grid tbody tr').each(function () {
            var cpf = $(this).find('td').eq(0).text();
            var nome = $(this).find('td').eq(1).text();

            // Verifica se os valores estão preenchidos antes de adicionar
            if (cpf.trim() !== "" && nome.trim() !== "") {
                beneficiarios.push({ CPF: cpf, Nome: nome});
            }
        });

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": form.find("#Nome").val(),
                "CEP": form.find("#CEP").val(),
                "Email": form.find("#Email").val(),
                "Sobrenome": form.find("#Sobrenome").val(),
                "Nacionalidade": form.find("#Nacionalidade").val(),
                "Estado": form.find("#Estado").val(),
                "Cidade": form.find("#Cidade").val(),
                "Logradouro": form.find("#Logradouro").val(),
                "Telefone": form.find("#Telefone").val(),
                "CPF": form.find("#CPF").val(),
                "Beneficiarios": beneficiarios // Envia a lista de beneficiários como JSON
            },
            error:
                function (r) {
                    if (r.status == 400)
                        ModalDialog("Ocorreu um erro", r.responseJSON);
                    else if (r.status == 500)
                        ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    redirecionarAposSubmit();
                }
        });
    })
})

var urlVoltar = document.getElementById("linkVoltar").getAttribute("href");
function redirecionarAposSubmit() {
    window.location.href = urlVoltar;
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
