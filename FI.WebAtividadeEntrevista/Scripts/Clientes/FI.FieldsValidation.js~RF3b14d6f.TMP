function mascaraCPF(input) {
    // Remove caracteres não numéricos
    let valor = input.value.replace(/\D/g, '');

    // Adiciona a máscara
    valor = valor.replace(/(\d{3})(\d)/, '$1.$2'); // Primeiro ponto
    valor = valor.replace(/(\d{3})(\d)/, '$1.$2'); // Segundo ponto
    valor = valor.replace(/(\d{3})(\d{1,2})$/, '$1-$2'); // Hífen

    input.value = valor; // Atualiza o valor do input
}
