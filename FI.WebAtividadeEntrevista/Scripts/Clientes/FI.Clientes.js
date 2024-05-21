
$(document).ready(function () {

    // Array para armazenar beneficiários
    const beneficiarios = [];
    // Aplica a máscara de CPF ao campo de input
    const cpfInput = document.getElementById("Cpf");
    cpfInput.addEventListener('input', function () {
        cpfInput.value = aplicarMascaraCPF(cpfInput.value);
    });


    const cpfBenInput = document.getElementById("CpfBeneficiario");
    cpfBenInput.addEventListener('input', function () {
        cpfBenInput.value = aplicarMascaraCPF(cpfBenInput.value);
    });

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "Cpf": $(this).find("#Cpf").val(),
                "Beneficiarios": beneficiarios
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
            }
        });
    })

  
    // Adiciona um novo beneficiário à tabela
    btnIncluirBeneficiario.onclick = function () {
        const nome = document.getElementById("NomeBeneficiario").value;
        const cpf = document.getElementById("CpfBeneficiario").value;

        if (beneficiarioForm.checkValidity()) {
            //// Adiciona uma nova linha à tabela
            const newRow = beneficiariosTable.insertRow();
            const cellCpf = newRow.insertCell(0);
            const cellNome = newRow.insertCell(1);
            const cellAcoes = newRow.insertCell(2);
            const alterarBtn = document.createElement('button');
            alterarBtn.textContent = 'Alterar';
            alterarBtn.classList.add('btn');
            alterarBtn.classList.add('btn-primary');
            alterarBtn.onclick = function () {
                editarBeneficiario(index);
            };
            cellAcoes.appendChild(alterarBtn);

            const excluirBtn = document.createElement('button');
            excluirBtn.classList.add('btn');
            excluirBtn.classList.add('btn-primary');
            excluirBtn.textContent = 'Excluir';
            excluirBtn.onclick = function () {
                excluirBeneficiario(index);
            };
            cellAcoes.appendChild(excluirBtn);

            cellNome.innerHTML = nome;
            cellCpf.innerHTML = cpf;
            beneficiarios.push({ nome, cpf });
            beneficiarioForm.reset();
        } else {
            beneficiarioForm.reportValidity();
        }
    }
})

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



// Função para aplicar máscara de CPF
function aplicarMascaraCPF(cpf) {
    cpf = cpf.replace(/\D/g, ""); // Remove caracteres não numéricos
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2");
    cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    return cpf;
}