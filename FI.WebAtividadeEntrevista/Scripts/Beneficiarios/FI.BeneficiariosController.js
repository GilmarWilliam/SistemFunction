$(document).ready(function () {
    let isEditing = false; // Flag para identificar se estamos no modo de edição
    let editingRow; // Linha atual sendo editada

    // Função para adicionar um novo item ao grid
    $('#btnIncluir').click(function () {
        var cpf = $("#cpfModal").val();
        var nome = $("#NomeModal").val();
        var cpfEncontrado = false;

        if (cpf && nome) {
            if (isEditing) {
                // Modo de edição: Atualiza a linha existente
                editingRow.find('td:eq(0)').text(cpf);
                editingRow.find('td:eq(1)').text(nome);

                // Sai do modo de edição
                isEditing = false;
                editingRow = null;

                // Altera o texto do botão de volta para "Incluir"
                $('#btnIncluir').text('Incluir');
            }
            else {
                $.ajax({
                    url: '/Beneficiarios/VerificarExistenciaCPF',
                    type: 'POST',
                    data: { CPF: cpf },
                    success: function (response) {
                        if (response === "CPF ja existente no banco de dados") {
                            $('#mensagemErroBeneficiario').text(response);
                        } else {
                            $('#mensagemErroBeneficiario').text('');
                            // Percorre todas as linhas da tabela para verificar se o CPF já existe
                            $('#grid tbody tr').each(function () {
                                var cpfNaLinha = $(this).find('td').eq(0).text();

                                if (cpfNaLinha === cpf) {
                                    cpfEncontrado = true;
                                    return $('#mensagemErroBeneficiario').text('CPF ja existente no grid');; // Interrompe o loop se o CPF for encontrado
                                }
                            });
                            if (!cpfEncontrado) {
                                // Modo de inclusão: Adiciona uma nova linha ao grid
                                var newRow = `
                                            <tr>
                                                <td>${cpf}</td>
                                                <td>${nome}</td>
                                                <td>
                                                    <button class="btn btn-primary btn-sm btnAlterar">Alterar</button>
                                                    <button class="btn btn-primary btn-sm btnExcluir">Excluir</button>
                                                </td>
                                            </tr>
                                        `;
                                $('#grid tbody').append(newRow);

                                // Mostra o grid se ele estiver oculto
                                $('#gridContainer').show();
                            }
                        }
                    },
                    error: function () {
                        alert("Erro na verificação do CPF.");
                    }
                });
            }

            // Limpa os campos do formulário
            $('#formModal')[0].reset();
        } else {
            alert("Preencha todos os campos.");
        }
    });

    // Evento para entrar no modo de edição
    $('#grid').on('click', '.btnAlterar', function () {
        editingRow = $(this).closest('tr'); // Linha atual sendo editada
        var cpf = editingRow.find('td:eq(0)').text();
        var nome = editingRow.find('td:eq(1)').text();

        // Carrega os valores nos campos de entrada
        $('#cpfModal').val(cpf);
        $('#NomeModal').val(nome);

        // Altera o texto do botão para "Salvar"
        $('#btnIncluir').text('Salvar');

        // Define a flag de edição como verdadeira
        isEditing = true;
    });

    // Função para excluir um item do grid
    $('#grid').on('click', '.btnExcluir', function () {
        $(this).closest('tr').remove();

        // Oculta o grid se não houver mais linhas na tabela
        if ($('#grid tbody tr').length === 0) {
            $('#gridContainer').hide();
        }
    });
});

function abrirModal() {
    $('#BeneficiariosModal').modal({
        backdrop: 'static', // Não permite fechar ao clicar fora
        keyboard: false  // Também impede de fechar com a tecla ESC
    });

    if (obj !== null) {
        // Limpa o grid antes de adicionar novos dados
        $('#grid tbody').empty();

        // Verifica se há beneficiários para mostrar
        if (obj.Beneficiarios.length > 0) {
            $('#gridContainer').show(); // Mostra o grid

            // Percorre a lista de beneficiários e cria as linhas da tabela
            obj.Beneficiarios.forEach(function (beneficiario) {
                var novaLinha = `<tr>
                                <td>${beneficiario.CPF}</td>
                                <td>${beneficiario.Nome}</td>
                                <td>
                                    <button class="btn btn-primary btn-sm btnAlterar" data-id="${beneficiario.Id}">Alterar</button>
                                    <button class="btn btn-primary btn-sm btnExcluir" data-id="${beneficiario.Id}">Excluir</button>
                                </td>
                             </tr>`;
                $('#grid tbody').append(novaLinha);
            });
        } else {
            $('#gridContainer').hide(); // Esconde o grid se não houver dados
        }
    }

    $('#BeneficiariosModal').modal('show');
}